using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeepPlayer
{
    public partial class frmBeepPlayer : Form
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);

        // ── 音符資料 ────────────────────────────────────────────
        private readonly int[]    freq      = { 523, 587, 659, 698, 784, 880, 988, 1046 };
        private readonly string[] noteNames = { "Do", "Re", "Mi", "Fa", "Sol", "La", "Si", "Do" };
        private readonly Keys[]   keyMap    = { Keys.A, Keys.S, Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K };
        private readonly string[] keyLabels = { "A", "S", "D", "F", "G", "H", "J", "K" };

        // ── 播放狀態 ─────────────────────────────────────────────
        private int    noteDuration  = 300;
        private bool   isRecording   = false;
        private Button[] pianoButtons;
        private List<(int freqIdx, int gap)> recordedNotes = new List<(int, int)>();
        private DateTime lastNoteTime;

        private CancellationTokenSource _cts       = null;
        private ManualResetEventSlim    _pauseGate = new ManualResetEventSlim(true);
        private bool _isPaused      = false;
        private bool _isPlayingDemo = false;
        private bool _isReplaying   = false;

        // ── 縮放 ─────────────────────────────────────────────────
        private int    initWidth  = 0;
        private int    initHeight = 0;
        private Dictionary<string, System.Drawing.Rectangle> initControl = new Dictionary<string, System.Drawing.Rectangle>();

        // ── 配色 ─────────────────────────────────────────────────
        private readonly Color _bg        = Color.FromArgb(30,  30,  46);
        private readonly Color _accent    = Color.FromArgb(137, 180, 250);
        private readonly Color _textMain  = Color.FromArgb(205, 214, 244);
        private readonly Color _subtle    = Color.FromArgb(108, 112, 134);
        private readonly Color _keyNormal = Color.White;
        private readonly Color _keyPress  = Color.FromArgb(173, 216, 230);
        private readonly Color _keyHighDo = Color.FromArgb(255, 220, 220);

        // ── 小星星旋律（音符索引, ms）───────────────────────────
        private readonly (int idx, int dur)[] twinkleMelody =
        {
            (0,350),(0,350),(4,350),(4,350),(5,350),(5,350),(4,700),
            (3,350),(3,350),(2,350),(2,350),(1,350),(1,350),(0,700),
            (4,350),(4,350),(3,350),(3,350),(2,350),(2,350),(1,700),
            (4,350),(4,350),(3,350),(3,350),(2,350),(2,350),(1,700),
            (0,350),(0,350),(4,350),(4,350),(5,350),(5,350),(4,700),
            (3,350),(3,350),(2,350),(2,350),(1,350),(1,350),(0,700),
        };

        // ════════════════════════════════════════════════════════
        public frmBeepPlayer()
        {
            InitializeComponent();
            ApplyPianoStyle();
            RegisterKeyEvents();
        }

        // ── 外觀套用 ─────────────────────────────────────────────
        private void ApplyPianoStyle()
        {
            pianoButtons = new Button[] { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8 };

            this.BackColor    = _bg;
            pnlTop.BackColor  = _bg;
            palMain.BackColor = Color.FromArgb(22, 22, 38);

            lblDuration.ForeColor    = _accent;
            lblDurationVal.ForeColor = _textMain;
            lblStatus.ForeColor      = Color.FromArgb(148, 226, 213);
            lblKeyHint.ForeColor     = _subtle;
            trkDuration.BackColor    = _bg;

            StyleCtrlBtn(btnRecord, Color.FromArgb(243, 139, 168));
            StyleCtrlBtn(btnReplay, Color.FromArgb(166, 227, 161));
            StyleCtrlBtn(btnDemo,   Color.FromArgb(250, 179, 135));
            StyleCtrlBtn(btnPause,  Color.FromArgb(180, 190, 254));
            btnPause.Visible = false;

            for (int i = 0; i < pianoButtons.Length; i++)
            {
                Button btn  = pianoButtons[i];
                bool highDo = (i == 7);

                btn.BackColor = highDo ? _keyHighDo : _keyNormal;
                btn.ForeColor = Color.FromArgb(40, 40, 60);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor       = Color.FromArgb(90, 90, 120);
                btn.FlatAppearance.BorderSize         = 2;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(210, 235, 255);
                btn.Font      = new Font("微軟正黑體", 9F, FontStyle.Bold);
                btn.TextAlign = ContentAlignment.BottomCenter;
                btn.Padding   = new Padding(0, 0, 0, 10);
                btn.Text      = $"{noteNames[i]}\n[{keyLabels[i]}]\n{freq[i]} Hz";
                btn.Cursor    = Cursors.Hand;
                btn.UseVisualStyleBackColor = false;

                int idx = i;
                btn.MouseDown += (s, e2) => {
                    if (e2.Button == MouseButtons.Left)
                        ((Button)s).BackColor = _keyPress;
                };
                btn.MouseUp += (s, e2) => {
                    ((Button)s).BackColor = (idx == 7) ? _keyHighDo : _keyNormal;
                };
            }
        }

        private void StyleCtrlBtn(Button btn, Color back)
        {
            btn.BackColor  = back;
            btn.ForeColor  = Color.FromArgb(30, 30, 46);
            btn.FlatStyle  = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font       = new Font("微軟正黑體", 9F, FontStyle.Bold);
            btn.Cursor     = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        // ── 鍵盤對應 ─────────────────────────────────────────────
        private void RegisterKeyEvents()
        {
            this.KeyPreview = true;
            this.KeyDown   += frmBeepPlayer_KeyDown;
        }

        private void frmBeepPlayer_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isPlayingDemo || _isReplaying) return;
            for (int i = 0; i < keyMap.Length; i++)
            {
                if (e.KeyCode == keyMap[i])
                {
                    PlayNote(i);
                    pianoButtons[i].BackColor = _keyPress;
                    int cap = i;
                    Task.Delay(200).ContinueWith(_ =>
                        this.Invoke(new Action(() =>
                            pianoButtons[cap].BackColor = (cap == 7) ? _keyHighDo : _keyNormal)));
                    e.Handled = true;
                    break;
                }
            }
        }

        // ── 播放單音 ─────────────────────────────────────────────
        private void PlayNote(int idx)
        {
            if (isRecording)
            {
                int gap = recordedNotes.Count == 0
                    ? 0
                    : (int)(DateTime.Now - lastNoteTime).TotalMilliseconds;
                recordedNotes.Add((idx, gap));
                lastNoteTime   = DateTime.Now;
                lblStatus.Text = $"🔴 錄音中... 已錄 {recordedNotes.Count} 個音符";
            }
            int d = noteDuration;
            Task.Run(() => Beep(freq[idx], d));
        }

        private void btn1_Click(object sender, EventArgs e)
            => PlayNote(((Button)sender).TabIndex);

        // ── TrackBar ─────────────────────────────────────────────
        private void trkDuration_Scroll(object sender, EventArgs e)
        {
            noteDuration        = trkDuration.Value;
            lblDurationVal.Text = $"{noteDuration} ms";
        }

        // ── 錄音 ─────────────────────────────────────────────────
        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (!isRecording)
            {
                recordedNotes.Clear();
                lastNoteTime        = DateTime.Now;
                isRecording         = true;
                btnRecord.Text      = "⏹ 停止錄音";
                btnRecord.BackColor = Color.FromArgb(255, 80, 100);
                lblStatus.Text      = "🔴 錄音中... 請彈奏琴鍵";
                btnReplay.Enabled   = false;
                btnDemo.Enabled     = false;
            }
            else
            {
                isRecording         = false;
                btnRecord.Text      = "🔴 錄音";
                btnRecord.BackColor = Color.FromArgb(243, 139, 168);
                lblStatus.Text      = $"✅ 錄音完成，共 {recordedNotes.Count} 個音符";
                btnReplay.Enabled   = recordedNotes.Count > 0;
                btnDemo.Enabled     = true;
            }
        }

        // ── 重播 ─────────────────────────────────────────────────
        private void btnReplay_Click(object sender, EventArgs e)
        {
            if (_isReplaying) { _cts?.Cancel(); return; }
            if (recordedNotes.Count == 0) return;

            _cts        = new CancellationTokenSource();
            _isReplaying = true;
            SetPianoEnabled(false);
            btnRecord.Enabled = false;
            btnDemo.Enabled   = false;
            btnReplay.Text    = "⏹ 停止重播";
            lblStatus.Text    = "▶ 重播中...";

            var token = _cts.Token;
            Task.Run(() =>
            {
                try
                {
                    foreach (var (noteIdx, gap) in recordedNotes)
                    {
                        token.ThrowIfCancellationRequested();
                        if (gap > 0) Thread.Sleep(Math.Min(gap, 2000));
                        Beep(freq[noteIdx], noteDuration);
                        this.Invoke(new Action(() => pianoButtons[noteIdx].BackColor = _keyPress));
                        Thread.Sleep(noteDuration + 40);
                        this.Invoke(new Action(() =>
                            pianoButtons[noteIdx].BackColor = (noteIdx == 7) ? _keyHighDo : _keyNormal));
                    }
                    this.Invoke(new Action(() => lblStatus.Text = "✅ 重播完畢"));
                }
                catch (OperationCanceledException)
                {
                    this.Invoke(new Action(() => lblStatus.Text = "⏹ 重播已停止"));
                }
                finally
                {
                    this.Invoke(new Action(() =>
                    {
                        _isReplaying      = false;
                        btnReplay.Text    = "▶ 重播";
                        ResetAllKeyColors();
                        SetAllEnabled(true);
                    }));
                }
            }, token);
        }

        // ── 示範 ─────────────────────────────────────────────────
        private void btnDemo_Click(object sender, EventArgs e)
        {
            if (_isPlayingDemo)
            {
                _cts?.Cancel();
                _pauseGate.Set(); // 確保不卡在暫停
                return;
            }
            StartDemo();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!_isPaused)
            {
                _pauseGate.Reset();
                _isPaused      = true;
                btnPause.Text  = "▶ 繼續";
                lblStatus.Text = "⏸ 示範暫停中";
            }
            else
            {
                _pauseGate.Set();
                _isPaused      = false;
                btnPause.Text  = "⏸ 暫停";
                lblStatus.Text = "🌟 示範播放中";
            }
        }

        private void StartDemo()
        {
            _cts           = new CancellationTokenSource();
            _isPaused      = false;
            _isPlayingDemo = true;
            _pauseGate.Set();

            SetPianoEnabled(false);
            btnRecord.Enabled = false;
            btnReplay.Enabled = false;
            btnDemo.Text      = "⏹ 停止示範";
            btnDemo.BackColor = Color.FromArgb(255, 140, 100);
            btnPause.Visible  = true;
            btnPause.Text     = "⏸ 暫停";
            lblStatus.Text    = "🌟 示範播放中";

            var token = _cts.Token;
            Task.Run(() =>
            {
                try
                {
                    foreach (var (noteIdx, dur) in twinkleMelody)
                    {
                        token.ThrowIfCancellationRequested();
                        _pauseGate.Wait(token);         // 暫停時在此等待

                        Beep(freq[noteIdx], dur);
                        this.Invoke(new Action(() => pianoButtons[noteIdx].BackColor = _keyPress));
                        Thread.Sleep(dur + 60);
                        this.Invoke(new Action(() =>
                            pianoButtons[noteIdx].BackColor = (noteIdx == 7) ? _keyHighDo : _keyNormal));
                    }
                    this.Invoke(new Action(() => lblStatus.Text = "✅ 示範播放完畢"));
                }
                catch (OperationCanceledException)
                {
                    this.Invoke(new Action(() => lblStatus.Text = "⏹ 示範已停止"));
                }
                finally
                {
                    this.Invoke(new Action(() =>
                    {
                        _isPlayingDemo    = false;
                        _isPaused         = false;
                        btnDemo.Text      = "🌟 示範";
                        btnDemo.BackColor = Color.FromArgb(250, 179, 135);
                        btnPause.Visible  = false;
                        ResetAllKeyColors();
                        SetAllEnabled(true);
                    }));
                }
            }, token);
        }

        // ── 工具函式 ─────────────────────────────────────────────
        private void SetPianoEnabled(bool v)
        {
            foreach (var b in pianoButtons) b.Enabled = v;
        }

        private void SetAllEnabled(bool v)
        {
            btnRecord.Enabled   = v;
            btnReplay.Enabled   = v && recordedNotes.Count > 0;
            btnDemo.Enabled     = v;
            trkDuration.Enabled = v;
            SetPianoEnabled(v);
        }

        private void ResetAllKeyColors()
        {
            for (int i = 0; i < pianoButtons.Length; i++)
                pianoButtons[i].BackColor = (i == 7) ? _keyHighDo : _keyNormal;
        }

        // ── Form 事件 ─────────────────────────────────────────────
        private void frmBeepPlayer_Load(object sender, EventArgs e)
        {
            initWidth  = palMain.Width;
            initHeight = palMain.Height;
            foreach (Control ctl in palMain.Controls)
                initControl.Add(ctl.Name, new System.Drawing.Rectangle(ctl.Left, ctl.Top, ctl.Width, ctl.Height));

            trkDuration.Minimum       = 100;
            trkDuration.Maximum       = 1000;
            trkDuration.Value         = 300;
            trkDuration.TickFrequency = 100;
            lblDurationVal.Text       = "300 ms";
            lblStatus.Text            = "請彈奏琴鍵，或用鍵盤 A S D F G H J K 演奏";
            lblKeyHint.Text           = "鍵盤對應：A=Do  S=Re  D=Mi  F=Fa  G=Sol  H=La  J=Si  K=Do↑";
        }

        private void frmBeepPlayer_SizeChanged(object sender, EventArgs e)
        {
            if (initWidth == 0 || initHeight == 0 || initControl.Count == 0) return;
            double rw = (double)palMain.Width  / initWidth;
            double rh = (double)palMain.Height / initHeight;
            foreach (Control ctl in palMain.Controls)
            {
                if (!initControl.ContainsKey(ctl.Name)) continue;
                var r  = initControl[ctl.Name];
                ctl.Left   = (int)(r.Left   * rw);
                ctl.Top    = (int)(r.Top    * rh);
                ctl.Width  = (int)(r.Width  * rw);
                ctl.Height = (int)(r.Height * rh);
            }
        }

        private void frmBeepPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cts?.Cancel();
            _pauseGate.Set();
            var result = MessageBox.Show("確定要關閉應用程式嗎？", "關閉確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                e.Cancel = true;
        }
    }
}
