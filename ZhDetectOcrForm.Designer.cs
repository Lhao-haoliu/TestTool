namespace TestTool
{
    partial class ZhDetectOcrForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            if (disposing && pictureBoxImage?.Image != null)
            {
                pictureBoxImage.Image.Dispose();
                pictureBoxImage.Image = null;
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblModelKey = new System.Windows.Forms.Label();
            this.cmbModelKey = new System.Windows.Forms.ComboBox();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.txtRequest = new System.Windows.Forms.TextBox();
            this.txtBase64 = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(12, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(760, 23);
            this.txtUrl.TabIndex = 0;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(12, 45);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(100, 25);
            this.btnSelectImage.TabIndex = 1;
            this.btnSelectImage.Text = "上传图片";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(130, 45);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 25);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "发送接口";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblModelKey
            // 
            this.lblModelKey.AutoSize = true;
            this.lblModelKey.Location = new System.Drawing.Point(250, 49);
            this.lblModelKey.Name = "lblModelKey";
            this.lblModelKey.Size = new System.Drawing.Size(67, 17);
            this.lblModelKey.TabIndex = 3;
            this.lblModelKey.Text = "model_key:";
            // 
            // cmbModelKey
            // 
            this.cmbModelKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown; // ✅可下拉+可手输
            this.cmbModelKey.FormattingEnabled = true;
            this.cmbModelKey.Location = new System.Drawing.Point(325, 45);
            this.cmbModelKey.Name = "cmbModelKey";
            this.cmbModelKey.Size = new System.Drawing.Size(447, 25);
            this.cmbModelKey.TabIndex = 4;
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Location = new System.Drawing.Point(12, 80);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(300, 240);
            this.pictureBoxImage.TabIndex = 5;
            this.pictureBoxImage.TabStop = false;
            // 
            // txtRequest
            // 
            this.txtRequest.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtRequest.Location = new System.Drawing.Point(330, 80);
            this.txtRequest.Multiline = true;
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequest.Size = new System.Drawing.Size(442, 240);
            this.txtRequest.TabIndex = 6;
            this.txtRequest.WordWrap = false;
            // 
            // txtBase64
            // 
            this.txtBase64.Location = new System.Drawing.Point(12, 330);
            this.txtBase64.Multiline = true;
            this.txtBase64.Name = "txtBase64";
            this.txtBase64.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBase64.Size = new System.Drawing.Size(760, 100);
            this.txtBase64.TabIndex = 7;
            this.txtBase64.WordWrap = false;
            // 
            // txtResponse
            // 
            this.txtResponse.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtResponse.Location = new System.Drawing.Point(12, 440);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponse.Size = new System.Drawing.Size(760, 200);
            this.txtResponse.TabIndex = 8;
            this.txtResponse.WordWrap = false;
            // 
            // ZhDetectOcrForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.txtBase64);
            this.Controls.Add(this.txtRequest);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.cmbModelKey);
            this.Controls.Add(this.lblModelKey);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.txtUrl);
            this.Name = "ZhDetectOcrForm";
            this.Text = "ZH Detect + OCR API Tester";
            this.Load += new System.EventHandler(this.ZhDetectOcrForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblModelKey;
        private System.Windows.Forms.ComboBox cmbModelKey;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.TextBox txtBase64;
        private System.Windows.Forms.TextBox txtResponse;
    }
}
