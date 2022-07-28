namespace DatenChip8.Gui {
    partial class AboutForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblAboutDivider = new System.Windows.Forms.Label();
            this.lblAboutFormText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(7, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(313, 36);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DatenChip8";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAboutDivider
            // 
            this.lblAboutDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAboutDivider.Location = new System.Drawing.Point(15, 44);
            this.lblAboutDivider.Name = "lblAboutDivider";
            this.lblAboutDivider.Size = new System.Drawing.Size(305, 2);
            this.lblAboutDivider.TabIndex = 1;
            // 
            // lblAboutFormText
            // 
            this.lblAboutFormText.Location = new System.Drawing.Point(15, 62);
            this.lblAboutFormText.Name = "lblAboutFormText";
            this.lblAboutFormText.Size = new System.Drawing.Size(305, 80);
            this.lblAboutFormText.TabIndex = 2;
            this.lblAboutFormText.Text = "A simple Chip8 Emulator written by Datenyan\r\n\r\nImplements all instructions, and p" +
    "asses (almost) all quirk tests.";
            this.lblAboutFormText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 151);
            this.Controls.Add(this.lblAboutFormText);
            this.Controls.Add(this.lblAboutDivider);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutForm";
            this.Text = "AboutForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblTitle;
        private Label lblAboutDivider;
        private Label lblAboutFormText;
    }
}