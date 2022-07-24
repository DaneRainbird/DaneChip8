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
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGameDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxGameDisplay
            // 
            this.picBoxGameDisplay.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.picBoxGameDisplay.BackColor = System.Drawing.Color.Black;
            this.picBoxGameDisplay.Location = new System.Drawing.Point(12, 12);
            this.picBoxGameDisplay.Name = "picBoxGameDisplay";
            this.picBoxGameDisplay.Size = new System.Drawing.Size(640, 320);
            this.picBoxGameDisplay.TabIndex = 0;
            this.picBoxGameDisplay.TabStop = false;
            // 
            // btnPausePlay
            // 
            this.btnPausePlay.Location = new System.Drawing.Point(12, 338);
            this.btnPausePlay.Name = "btnPausePlay";
            this.btnPausePlay.Size = new System.Drawing.Size(75, 23);
            this.btnPausePlay.TabIndex = 1;
            this.btnPausePlay.Text = "Pause";
            this.btnPausePlay.UseVisualStyleBackColor = true;
            this.btnPausePlay.Click += new System.EventHandler(this.btnPausePlay_Click);
            // 
            // Machine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(664, 371);
            this.Controls.Add(this.btnPausePlay);
            this.Controls.Add(this.picBoxGameDisplay);
            this.Name = "Machine";
            this.Text = "DatenChip8";
            this.Load += new System.EventHandler(this.Machine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxGameDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public PictureBox picBoxGameDisplay;
        private Button btnPausePlay;
    }
}