namespace TestTool
{
    partial class TskSimilarityForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            if (disposing)
            {
                if (pictureBoxImage?.Image != null) { pictureBoxImage.Image.Dispose(); pictureBoxImage.Image = null; }
                if (pictureBoxPublic?.Image != null) { pictureBoxPublic.Image.Dispose(); pictureBoxPublic.Image = null; }
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnSelectPublic = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.pictureBoxPublic = new System.Windows.Forms.PictureBox();
            this.lblImage = new System.Windows.Forms.Label();
            this.lblPublic = new System.Windows.Forms.Label();
            this.txtImageBase64 = new System.Windows.Forms.TextBox();
            this.txtPublicBase64 = new System.Windows.Forms.TextBox();
            this.txtRequest = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPublic)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(12, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(960, 23);
            this.txtUrl.TabIndex = 0;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(12, 45);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(120, 25);
            this.btnSelectImage.TabIndex = 1;
            this.btnSelectImage.Text = "上传实际图片";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // btnSelectPublic
            // 
            this.btnSelectPublic.Location = new System.Drawing.Point(148, 45);
            this.btnSelectPublic.Name = "btnSelectPublic";
            this.btnSelectPublic.Size = new System.Drawing.Size(120, 25);
            this.btnSelectPublic.TabIndex = 2;
            this.btnSelectPublic.Text = "上传公版图片";
            this.btnSelectPublic.UseVisualStyleBackColor = true;
            this.btnSelectPublic.Click += new System.EventHandler(this.btnSelectPublic_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(286, 45);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(120, 25);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "发送接口";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Location = new System.Drawing.Point(12, 105);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(300, 220);
            this.pictureBoxImage.TabIndex = 4;
            this.pictureBoxImage.TabStop = false;
            // 
            // pictureBoxPublic
            // 
            this.pictureBoxPublic.Location = new System.Drawing.Point(330, 105);
            this.pictureBoxPublic.Name = "pictureBoxPublic";
            this.pictureBoxPublic.Size = new System.Drawing.Size(300, 220);
            this.pictureBoxPublic.TabIndex = 5;
            this.pictureBoxPublic.TabStop = false;
            // 
            // lblImage
            // 
            this.lblImage.AutoSize = true;
            this.lblImage.Location = new System.Drawing.Point(12, 80);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(128, 17);
            this.lblImage.TabIndex = 6;
            this.lblImage.Text = "实际图片（image_base64）";
            // 
            // lblPublic
            // 
            this.lblPublic.AutoSize = true;
            this.lblPublic.Location = new System.Drawing.Point(330, 80);
            this.lblPublic.Name = "lblPublic";
            this.lblPublic.Size = new System.Drawing.Size(128, 17);
            this.lblPublic.TabIndex = 7;
            this.lblPublic.Text = "公版图片（public_base64）";
            // 
            // txtImageBase64
            // 
            this.txtImageBase64.Location = new System.Drawing.Point(12, 335);
            this.txtImageBase64.Multiline = true;
            this.txtImageBase64.Name = "txtImageBase64";
            this.txtImageBase64.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtImageBase64.Size = new System.Drawing.Size(300, 120);
            this.txtImageBase64.TabIndex = 8;
            this.txtImageBase64.WordWrap = false;
            // 
            // txtPublicBase64
            // 
            this.txtPublicBase64.Location = new System.Drawing.Point(330, 335);
            this.txtPublicBase64.Multiline = true;
            this.txtPublicBase64.Name = "txtPublicBase64";
            this.txtPublicBase64.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPublicBase64.Size = new System.Drawing.Size(300, 120);
            this.txtPublicBase64.TabIndex = 9;
            this.txtPublicBase64.WordWrap = false;
            // 
            // txtRequest
            // 
            this.txtRequest.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtRequest.Location = new System.Drawing.Point(648, 105);
            this.txtRequest.Multiline = true;
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequest.Size = new System.Drawing.Size(324, 350);
            this.txtRequest.TabIndex = 10;
            this.txtRequest.WordWrap = false;
            // 
            // txtResponse
            // 
            this.txtResponse.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtResponse.Location = new System.Drawing.Point(12, 470);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponse.Size = new System.Drawing.Size(960, 190);
            this.txtResponse.TabIndex = 11;
            this.txtResponse.WordWrap = false;
            // 
            // TskSimilarityForm
            // 
            this.ClientSize = new System.Drawing.Size(984, 681);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.txtRequest);
            this.Controls.Add(this.txtPublicBase64);
            this.Controls.Add(this.txtImageBase64);
            this.Controls.Add(this.lblPublic);
            this.Controls.Add(this.lblImage);
            this.Controls.Add(this.pictureBoxPublic);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnSelectPublic);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.txtUrl);
            this.Name = "TskSimilarityForm";
            this.Text = "TSK Similarity (Compare) API Tester";
            this.Load += new System.EventHandler(this.TskSimilarityForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPublic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnSelectPublic;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.PictureBox pictureBoxPublic;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.Label lblPublic;
        private System.Windows.Forms.TextBox txtImageBase64;
        private System.Windows.Forms.TextBox txtPublicBase64;
        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.TextBox txtResponse;
    }
}
