namespace TestTool
{
    partial class HgSimilarityCheckForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            if (disposing)
            {
                if (pictureBoxPublic?.Image != null) { pictureBoxPublic.Image.Dispose(); pictureBoxPublic.Image = null; }
                if (pictureBoxTarget?.Image != null) { pictureBoxTarget.Image.Dispose(); pictureBoxTarget.Image = null; }
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnSelectPublic = new System.Windows.Forms.Button();
            this.btnSelectTarget = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblModelKey = new System.Windows.Forms.Label();
            this.txtModelKey = new System.Windows.Forms.TextBox();
            this.lblPublic = new System.Windows.Forms.Label();
            this.lblTarget = new System.Windows.Forms.Label();
            this.pictureBoxPublic = new System.Windows.Forms.PictureBox();
            this.pictureBoxTarget = new System.Windows.Forms.PictureBox();
            this.txtPublicBase64 = new System.Windows.Forms.TextBox();
            this.txtTargetBase64 = new System.Windows.Forms.TextBox();
            this.txtRequest = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPublic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTarget)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(12, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(960, 23);
            this.txtUrl.TabIndex = 0;
            // 
            // btnSelectPublic
            // 
            this.btnSelectPublic.Location = new System.Drawing.Point(12, 45);
            this.btnSelectPublic.Name = "btnSelectPublic";
            this.btnSelectPublic.Size = new System.Drawing.Size(120, 25);
            this.btnSelectPublic.TabIndex = 1;
            this.btnSelectPublic.Text = "上传公版图片";
            this.btnSelectPublic.UseVisualStyleBackColor = true;
            this.btnSelectPublic.Click += new System.EventHandler(this.btnSelectPublic_Click);
            // 
            // btnSelectTarget
            // 
            this.btnSelectTarget.Location = new System.Drawing.Point(148, 45);
            this.btnSelectTarget.Name = "btnSelectTarget";
            this.btnSelectTarget.Size = new System.Drawing.Size(120, 25);
            this.btnSelectTarget.TabIndex = 2;
            this.btnSelectTarget.Text = "上传目标图片";
            this.btnSelectTarget.UseVisualStyleBackColor = true;
            this.btnSelectTarget.Click += new System.EventHandler(this.btnSelectTarget_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(286, 45);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 25);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "发送接口";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblModelKey
            // 
            this.lblModelKey.AutoSize = true;
            this.lblModelKey.Location = new System.Drawing.Point(405, 49);
            this.lblModelKey.Name = "lblModelKey";
            this.lblModelKey.Size = new System.Drawing.Size(67, 17);
            this.lblModelKey.TabIndex = 4;
            this.lblModelKey.Text = "model_key:";
            // 
            // txtModelKey
            // 
            this.txtModelKey.Location = new System.Drawing.Point(478, 45);
            this.txtModelKey.Name = "txtModelKey";
            this.txtModelKey.Size = new System.Drawing.Size(494, 23);
            this.txtModelKey.TabIndex = 5;
            // 
            // lblPublic
            // 
            this.lblPublic.AutoSize = true;
            this.lblPublic.Location = new System.Drawing.Point(12, 80);
            this.lblPublic.Name = "lblPublic";
            this.lblPublic.Size = new System.Drawing.Size(124, 17);
            this.lblPublic.TabIndex = 6;
            this.lblPublic.Text = "公版图片（public_base64）";
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.Location = new System.Drawing.Point(330, 80);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(124, 17);
            this.lblTarget.TabIndex = 7;
            this.lblTarget.Text = "目标图片（target_base64）";
            // 
            // pictureBoxPublic
            // 
            this.pictureBoxPublic.Location = new System.Drawing.Point(12, 105);
            this.pictureBoxPublic.Name = "pictureBoxPublic";
            this.pictureBoxPublic.Size = new System.Drawing.Size(300, 220);
            this.pictureBoxPublic.TabIndex = 8;
            this.pictureBoxPublic.TabStop = false;
            // 
            // pictureBoxTarget
            // 
            this.pictureBoxTarget.Location = new System.Drawing.Point(330, 105);
            this.pictureBoxTarget.Name = "pictureBoxTarget";
            this.pictureBoxTarget.Size = new System.Drawing.Size(300, 220);
            this.pictureBoxTarget.TabIndex = 9;
            this.pictureBoxTarget.TabStop = false;
            // 
            // txtPublicBase64
            // 
            this.txtPublicBase64.Location = new System.Drawing.Point(12, 335);
            this.txtPublicBase64.Multiline = true;
            this.txtPublicBase64.Name = "txtPublicBase64";
            this.txtPublicBase64.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPublicBase64.Size = new System.Drawing.Size(300, 120);
            this.txtPublicBase64.TabIndex = 10;
            this.txtPublicBase64.WordWrap = false;
            // 
            // txtTargetBase64
            // 
            this.txtTargetBase64.Location = new System.Drawing.Point(330, 335);
            this.txtTargetBase64.Multiline = true;
            this.txtTargetBase64.Name = "txtTargetBase64";
            this.txtTargetBase64.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTargetBase64.Size = new System.Drawing.Size(300, 120);
            this.txtTargetBase64.TabIndex = 11;
            this.txtTargetBase64.WordWrap = false;
            // 
            // txtRequest
            // 
            this.txtRequest.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtRequest.Location = new System.Drawing.Point(648, 105);
            this.txtRequest.Multiline = true;
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequest.Size = new System.Drawing.Size(324, 350);
            this.txtRequest.TabIndex = 12;
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
            this.txtResponse.TabIndex = 13;
            this.txtResponse.WordWrap = false;
            // 
            // HgSimilarityCheckForm
            // 
            this.ClientSize = new System.Drawing.Size(984, 681);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.txtRequest);
            this.Controls.Add(this.txtTargetBase64);
            this.Controls.Add(this.txtPublicBase64);
            this.Controls.Add(this.pictureBoxTarget);
            this.Controls.Add(this.pictureBoxPublic);
            this.Controls.Add(this.lblTarget);
            this.Controls.Add(this.lblPublic);
            this.Controls.Add(this.txtModelKey);
            this.Controls.Add(this.lblModelKey);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnSelectTarget);
            this.Controls.Add(this.btnSelectPublic);
            this.Controls.Add(this.txtUrl);
            this.Name = "HgSimilarityCheckForm";
            this.Text = "HG Similarity Check API Tester";
            this.Load += new System.EventHandler(this.HgSimilarityCheckForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPublic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTarget)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnSelectPublic;
        private System.Windows.Forms.Button btnSelectTarget;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblModelKey;
        private System.Windows.Forms.TextBox txtModelKey;
        private System.Windows.Forms.Label lblPublic;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.PictureBox pictureBoxPublic;
        private System.Windows.Forms.PictureBox pictureBoxTarget;
        private System.Windows.Forms.TextBox txtPublicBase64;
        private System.Windows.Forms.TextBox txtTargetBase64;
        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.TextBox txtResponse;
    }
}
