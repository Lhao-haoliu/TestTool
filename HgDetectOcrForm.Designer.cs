namespace TestTool
{
    partial class HgDetectOcrForm
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
            this.txtModelKey = new System.Windows.Forms.TextBox();
            this.lblModel1Conf = new System.Windows.Forms.Label();
            this.txtModel1Conf = new System.Windows.Forms.TextBox();
            this.lblModel2Conf = new System.Windows.Forms.Label();
            this.txtModel2Conf = new System.Windows.Forms.TextBox();
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
            // txtModelKey
            // 
            this.txtModelKey.Location = new System.Drawing.Point(325, 45);
            this.txtModelKey.Name = "txtModelKey";
            this.txtModelKey.Size = new System.Drawing.Size(200, 23);
            this.txtModelKey.TabIndex = 4;
            // 
            // lblModel1Conf
            // 
            this.lblModel1Conf.AutoSize = true;
            this.lblModel1Conf.Location = new System.Drawing.Point(535, 49);
            this.lblModel1Conf.Name = "lblModel1Conf";
            this.lblModel1Conf.Size = new System.Drawing.Size(77, 17);
            this.lblModel1Conf.TabIndex = 5;
            this.lblModel1Conf.Text = "model1_conf:";
            // 
            // txtModel1Conf
            // 
            this.txtModel1Conf.Location = new System.Drawing.Point(615, 45);
            this.txtModel1Conf.Name = "txtModel1Conf";
            this.txtModel1Conf.Size = new System.Drawing.Size(60, 23);
            this.txtModel1Conf.TabIndex = 6;
            // 
            // lblModel2Conf
            // 
            this.lblModel2Conf.AutoSize = true;
            this.lblModel2Conf.Location = new System.Drawing.Point(680, 49);
            this.lblModel2Conf.Name = "lblModel2Conf";
            this.lblModel2Conf.Size = new System.Drawing.Size(77, 17);
            this.lblModel2Conf.TabIndex = 7;
            this.lblModel2Conf.Text = "model2_conf:";
            // 
            // txtModel2Conf
            // 
            this.txtModel2Conf.Location = new System.Drawing.Point(760, 45);
            this.txtModel2Conf.Name = "txtModel2Conf";
            this.txtModel2Conf.Size = new System.Drawing.Size(60, 23);
            this.txtModel2Conf.TabIndex = 8;
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Location = new System.Drawing.Point(12, 80);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(300, 240);
            this.pictureBoxImage.TabIndex = 9;
            this.pictureBoxImage.TabStop = false;
            // 
            // txtRequest
            // 
            this.txtRequest.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtRequest.Location = new System.Drawing.Point(330, 80);
            this.txtRequest.Multiline = true;
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequest.Size = new System.Drawing.Size(490, 240);
            this.txtRequest.TabIndex = 10;
            this.txtRequest.WordWrap = false;
            // 
            // txtBase64
            // 
            this.txtBase64.Location = new System.Drawing.Point(12, 330);
            this.txtBase64.Multiline = true;
            this.txtBase64.Name = "txtBase64";
            this.txtBase64.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBase64.Size = new System.Drawing.Size(808, 100);
            this.txtBase64.TabIndex = 11;
            this.txtBase64.WordWrap = false;
            // 
            // txtResponse
            // 
            this.txtResponse.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtResponse.Location = new System.Drawing.Point(12, 440);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponse.Size = new System.Drawing.Size(808, 200);
            this.txtResponse.TabIndex = 12;
            this.txtResponse.WordWrap = false;
            // 
            // HgDetectOcrForm
            // 
            this.ClientSize = new System.Drawing.Size(832, 661);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.txtBase64);
            this.Controls.Add(this.txtRequest);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.txtModel2Conf);
            this.Controls.Add(this.lblModel2Conf);
            this.Controls.Add(this.txtModel1Conf);
            this.Controls.Add(this.lblModel1Conf);
            this.Controls.Add(this.txtModelKey);
            this.Controls.Add(this.lblModelKey);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.txtUrl);
            this.Name = "HgDetectOcrForm";
            this.Text = "HG Detect + OCR API Tester";
            this.Load += new System.EventHandler(this.HgDetectOcrForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblModelKey;
        private System.Windows.Forms.TextBox txtModelKey;
        private System.Windows.Forms.Label lblModel1Conf;
        private System.Windows.Forms.TextBox txtModel1Conf;
        private System.Windows.Forms.Label lblModel2Conf;
        private System.Windows.Forms.TextBox txtModel2Conf;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.TextBox txtBase64;
        private System.Windows.Forms.TextBox txtResponse;
    }
}
