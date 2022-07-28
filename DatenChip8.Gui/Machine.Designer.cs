namespace DatenChip8.Gui {
    partial class Machine {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.picBoxGameDisplay = new System.Windows.Forms.PictureBox();
            this.btnPausePlay = new System.Windows.Forms.Button();
            this.btnShowCpuInfoForm = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGameDisplay)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBoxGameDisplay
            // 
            this.picBoxGameDisplay.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.picBoxGameDisplay.BackColor = System.Drawing.Color.Black;
            this.picBoxGameDisplay.Location = new System.Drawing.Point(12, 32);
            this.picBoxGameDisplay.Name = "picBoxGameDisplay";
            this.picBoxGameDisplay.Size = new System.Drawing.Size(640, 321);
            this.picBoxGameDisplay.TabIndex = 0;
            this.picBoxGameDisplay.TabStop = false;
            // 
            // btnPausePlay
            // 
            this.btnPausePlay.Location = new System.Drawing.Point(93, 358);
            this.btnPausePlay.Name = "btnPausePlay";
            this.btnPausePlay.Size = new System.Drawing.Size(75, 24);
            this.btnPausePlay.TabIndex = 1;
            this.btnPausePlay.Text = "Pause";
            this.btnPausePlay.UseVisualStyleBackColor = true;
            this.btnPausePlay.Click += new System.EventHandler(this.btnPausePlay_Click);
            // 
            // btnShowCpuInfoForm
            // 
            this.btnShowCpuInfoForm.Location = new System.Drawing.Point(174, 358);
            this.btnShowCpuInfoForm.Name = "btnShowCpuInfoForm";
            this.btnShowCpuInfoForm.Size = new System.Drawing.Size(110, 24);
            this.btnShowCpuInfoForm.TabIndex = 2;
            this.btnShowCpuInfoForm.Text = "Show CPU Info";
            this.btnShowCpuInfoForm.UseVisualStyleBackColor = true;
            this.btnShowCpuInfoForm.Click += new System.EventHandler(this.btnShowCpuInfoForm_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(12, 358);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(75, 24);
            this.btnRestart.TabIndex = 3;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(664, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectROMToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // selectROMToolStripMenuItem
            // 
            this.selectROMToolStripMenuItem.Name = "selectROMToolStripMenuItem";
            this.selectROMToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.selectROMToolStripMenuItem.Text = "Select ROM";
            this.selectROMToolStripMenuItem.Click += new System.EventHandler(this.selectROMToolStripMenuItem_Click);
            // 
            // Machine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(664, 394);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnShowCpuInfoForm);
            this.Controls.Add(this.btnPausePlay);
            this.Controls.Add(this.picBoxGameDisplay);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Machine";
            this.Text = "DatenChip8";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Machine_FormClosing);
            this.Load += new System.EventHandler(this.Machine_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Machine_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Machine_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGameDisplay)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public PictureBox picBoxGameDisplay;
        private Button btnPausePlay;
        private Button btnShowCpuInfoForm;
        private Button btnRestart;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem selectROMToolStripMenuItem;
    }
}