namespace DatenChip8.Gui {
    partial class RegistersForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistersForm));
            this.btnClose = new System.Windows.Forms.Button();
            this.rchTxtBoxCpuInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(244, 500);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // rchTxtBoxCpuInfo
            // 
            this.rchTxtBoxCpuInfo.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rchTxtBoxCpuInfo.Location = new System.Drawing.Point(12, 12);
            this.rchTxtBoxCpuInfo.Name = "rchTxtBoxCpuInfo";
            this.rchTxtBoxCpuInfo.Size = new System.Drawing.Size(307, 482);
            this.rchTxtBoxCpuInfo.TabIndex = 2;
            this.rchTxtBoxCpuInfo.Text = "";
            // 
            // RegistersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 535);
            this.ControlBox = false;
            this.Controls.Add(this.rchTxtBoxCpuInfo);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegistersForm";
            this.Text = "CPU Info";
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnClose;
        private RichTextBox rchTxtBoxCpuInfo;
    }
}