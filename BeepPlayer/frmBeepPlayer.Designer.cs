namespace BeepPlayer
{
    partial class frmBeepPlayer
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblDuration = new System.Windows.Forms.Label();
            this.trkDuration = new System.Windows.Forms.TrackBar();
            this.lblDurationVal = new System.Windows.Forms.Label();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnReplay = new System.Windows.Forms.Button();
            this.btnDemo = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblKeyHint = new System.Windows.Forms.Label();
            this.palMain = new System.Windows.Forms.Panel();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkDuration)).BeginInit();
            this.palMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblDuration);
            this.pnlTop.Controls.Add(this.trkDuration);
            this.pnlTop.Controls.Add(this.lblDurationVal);
            this.pnlTop.Controls.Add(this.btnRecord);
            this.pnlTop.Controls.Add(this.btnReplay);
            this.pnlTop.Controls.Add(this.btnDemo);
            this.pnlTop.Controls.Add(this.btnPause);
            this.pnlTop.Controls.Add(this.lblStatus);
            this.pnlTop.Controls.Add(this.lblKeyHint);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1329, 144);
            this.pnlTop.TabIndex = 1;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("微軟正黑體", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDuration.Location = new System.Drawing.Point(781, 22);
            this.lblDuration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(145, 33);
            this.lblDuration.TabIndex = 0;
            this.lblDuration.Text = "音符時長：";
            // 
            // trkDuration
            // 
            this.trkDuration.Location = new System.Drawing.Point(934, 22);
            this.trkDuration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trkDuration.Name = "trkDuration";
            this.trkDuration.Size = new System.Drawing.Size(255, 90);
            this.trkDuration.TabIndex = 10;
            this.trkDuration.Scroll += new System.EventHandler(this.trkDuration_Scroll);
            // 
            // lblDurationVal
            // 
            this.lblDurationVal.AutoSize = true;
            this.lblDurationVal.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblDurationVal.Location = new System.Drawing.Point(1197, 22);
            this.lblDurationVal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDurationVal.Name = "lblDurationVal";
            this.lblDurationVal.Size = new System.Drawing.Size(94, 30);
            this.lblDurationVal.TabIndex = 11;
            this.lblDurationVal.Text = "300 ms";
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(16, 17);
            this.btnRecord.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(170, 45);
            this.btnRecord.TabIndex = 11;
            this.btnRecord.Text = "🔴 錄音";
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnReplay
            // 
            this.btnReplay.Enabled = false;
            this.btnReplay.Location = new System.Drawing.Point(194, 17);
            this.btnReplay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReplay.Name = "btnReplay";
            this.btnReplay.Size = new System.Drawing.Size(159, 45);
            this.btnReplay.TabIndex = 12;
            this.btnReplay.Text = "▶ 重播";
            this.btnReplay.UseVisualStyleBackColor = false;
            this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
            // 
            // btnDemo
            // 
            this.btnDemo.Location = new System.Drawing.Point(362, 18);
            this.btnDemo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDemo.Name = "btnDemo";
            this.btnDemo.Size = new System.Drawing.Size(194, 45);
            this.btnDemo.TabIndex = 13;
            this.btnDemo.Text = "🌟 示範";
            this.btnDemo.UseVisualStyleBackColor = false;
            this.btnDemo.Click += new System.EventHandler(this.btnDemo_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(564, 17);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(144, 45);
            this.btnPause.TabIndex = 14;
            this.btnPause.Text = "⏸ 暫停";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Visible = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblStatus.Location = new System.Drawing.Point(20, 71);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1271, 33);
            this.lblStatus.TabIndex = 15;
            // 
            // lblKeyHint
            // 
            this.lblKeyHint.Font = new System.Drawing.Font("微軟正黑體", 8.5F);
            this.lblKeyHint.Location = new System.Drawing.Point(20, 106);
            this.lblKeyHint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKeyHint.Name = "lblKeyHint";
            this.lblKeyHint.Size = new System.Drawing.Size(1271, 30);
            this.lblKeyHint.TabIndex = 16;
            // 
            // palMain
            // 
            this.palMain.Controls.Add(this.btn8);
            this.palMain.Controls.Add(this.btn7);
            this.palMain.Controls.Add(this.btn6);
            this.palMain.Controls.Add(this.btn5);
            this.palMain.Controls.Add(this.btn4);
            this.palMain.Controls.Add(this.btn3);
            this.palMain.Controls.Add(this.btn2);
            this.palMain.Controls.Add(this.btn1);
            this.palMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palMain.Location = new System.Drawing.Point(0, 144);
            this.palMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.palMain.Name = "palMain";
            this.palMain.Size = new System.Drawing.Size(1329, 265);
            this.palMain.TabIndex = 0;
            // 
            // btn8
            // 
            this.btn8.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.btn8.Location = new System.Drawing.Point(1158, 25);
            this.btn8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(147, 215);
            this.btn8.TabIndex = 7;
            this.btn8.Text = "Do";
            this.btn8.UseVisualStyleBackColor = false;
            this.btn8.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn7
            // 
            this.btn7.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.btn7.Location = new System.Drawing.Point(995, 25);
            this.btn7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(147, 215);
            this.btn7.TabIndex = 6;
            this.btn7.Text = "Si";
            this.btn7.UseVisualStyleBackColor = false;
            this.btn7.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn6
            // 
            this.btn6.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.btn6.Location = new System.Drawing.Point(832, 25);
            this.btn6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(147, 215);
            this.btn6.TabIndex = 5;
            this.btn6.Text = "La";
            this.btn6.UseVisualStyleBackColor = false;
            this.btn6.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn5
            // 
            this.btn5.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.btn5.Location = new System.Drawing.Point(669, 25);
            this.btn5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(147, 215);
            this.btn5.TabIndex = 4;
            this.btn5.Text = "Sol";
            this.btn5.UseVisualStyleBackColor = false;
            this.btn5.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn4
            // 
            this.btn4.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.btn4.Location = new System.Drawing.Point(506, 25);
            this.btn4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(147, 215);
            this.btn4.TabIndex = 3;
            this.btn4.Text = "Fa";
            this.btn4.UseVisualStyleBackColor = false;
            this.btn4.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn3
            // 
            this.btn3.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.btn3.Location = new System.Drawing.Point(342, 25);
            this.btn3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(147, 215);
            this.btn3.TabIndex = 2;
            this.btn3.Text = "Mi";
            this.btn3.UseVisualStyleBackColor = false;
            this.btn3.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn2
            // 
            this.btn2.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.btn2.Location = new System.Drawing.Point(179, 25);
            this.btn2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(147, 215);
            this.btn2.TabIndex = 1;
            this.btn2.Text = "Re";
            this.btn2.UseVisualStyleBackColor = false;
            this.btn2.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn1
            // 
            this.btn1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.btn1.Location = new System.Drawing.Point(16, 25);
            this.btn1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(147, 215);
            this.btn1.TabIndex = 0;
            this.btn1.Text = "Do";
            this.btn1.UseVisualStyleBackColor = false;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // frmBeepPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1329, 409);
            this.Controls.Add(this.palMain);
            this.Controls.Add(this.pnlTop);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1317, 469);
            this.Name = "frmBeepPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "🎹 簡易電子琴";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBeepPlayer_FormClosing);
            this.Load += new System.EventHandler(this.frmBeepPlayer_Load);
            this.SizeChanged += new System.EventHandler(this.frmBeepPlayer_SizeChanged);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkDuration)).EndInit();
            this.palMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel    pnlTop;
        private System.Windows.Forms.Label    lblDuration;
        private System.Windows.Forms.TrackBar trkDuration;
        private System.Windows.Forms.Label    lblDurationVal;
        private System.Windows.Forms.Button   btnRecord;
        private System.Windows.Forms.Button   btnReplay;
        private System.Windows.Forms.Button   btnDemo;
        private System.Windows.Forms.Button   btnPause;
        private System.Windows.Forms.Label    lblStatus;
        private System.Windows.Forms.Label    lblKeyHint;

        private System.Windows.Forms.Panel  palMain;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn8;
    }
}
