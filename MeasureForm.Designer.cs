namespace TestTool
{
    partial class MeasureForm
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
            txtUrl = new TextBox();
            lblModelKey = new Label();
            txtModelKey = new TextBox();
            btnSelectImage = new Button();
            pictureBoxImage = new PictureBox();
            txtBase64 = new TextBox();
            btnSend = new Button();
            txtRequest = new TextBox();
            txtResponse = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).BeginInit();
            SuspendLayout();
            // 
            // txtUrl
            // 
            txtUrl.Location = new Point(12, 12);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(760, 23);
            txtUrl.TabIndex = 0;
            // 
            // lblModelKey
            // 
            lblModelKey.AutoSize = true;
            lblModelKey.Location = new Point(12, 47);
            lblModelKey.Name = "lblModelKey";
            lblModelKey.Size = new Size(82, 17);
            lblModelKey.TabIndex = 1;
            lblModelKey.Text = "model_key：";
            // 
            // txtModelKey
            // 
            txtModelKey.Location = new Point(89, 44);
            txtModelKey.Name = "txtModelKey";
            txtModelKey.Size = new Size(200, 23);
            txtModelKey.TabIndex = 2;
            // 
            // btnSelectImage
            // 
            btnSelectImage.Location = new Point(310, 43);
            btnSelectImage.Name = "btnSelectImage";
            btnSelectImage.Size = new Size(100, 25);
            btnSelectImage.TabIndex = 3;
            btnSelectImage.Text = "上传图片";
            btnSelectImage.UseVisualStyleBackColor = true;
            btnSelectImage.Click += btnSelectImage_Click;
            // 
            // pictureBoxImage
            // 
            pictureBoxImage.Location = new Point(12, 80);
            pictureBoxImage.Name = "pictureBoxImage";
            pictureBoxImage.Size = new Size(300, 240);
            pictureBoxImage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxImage.TabIndex = 4;
            pictureBoxImage.TabStop = false;
            // 
            // txtBase64
            // 
            txtBase64.Location = new Point(12, 330);
            txtBase64.Multiline = true;
            txtBase64.Name = "txtBase64";
            txtBase64.ScrollBars = ScrollBars.Both;
            txtBase64.Size = new Size(760, 100);
            txtBase64.TabIndex = 5;
            txtBase64.WordWrap = false;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(430, 43);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(100, 25);
            btnSend.TabIndex = 6;
            btnSend.Text = "发送接口";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // txtRequest
            // 
            txtRequest.Font = new Font("Consolas", 9F);
            txtRequest.Location = new Point(330, 80);
            txtRequest.Multiline = true;
            txtRequest.Name = "txtRequest";
            txtRequest.ScrollBars = ScrollBars.Both;
            txtRequest.Size = new Size(442, 240);
            txtRequest.TabIndex = 7;
            txtRequest.WordWrap = false;
            // 
            // txtResponse
            // 
            txtResponse.Font = new Font("Consolas", 9F);
            txtResponse.Location = new Point(12, 440);
            txtResponse.Multiline = true;
            txtResponse.Name = "txtResponse";
            txtResponse.ScrollBars = ScrollBars.Both;
            txtResponse.Size = new Size(760, 200);
            txtResponse.TabIndex = 8;
            txtResponse.WordWrap = false;
            // 
            // MeasureForm
            // 
            ClientSize = new Size(784, 661);
            Controls.Add(txtResponse);
            Controls.Add(txtRequest);
            Controls.Add(btnSend);
            Controls.Add(txtBase64);
            Controls.Add(pictureBoxImage);
            Controls.Add(btnSelectImage);
            Controls.Add(txtModelKey);
            Controls.Add(lblModelKey);
            Controls.Add(txtUrl);
            Name = "MeasureForm";
            Text = "Measure API Tester";
            Load += MeasureForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label lblModelKey;
        private System.Windows.Forms.TextBox txtModelKey;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.TextBox txtBase64;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.TextBox txtResponse;
    }
}
