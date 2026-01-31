namespace TestTool
{
    partial class TskEtchForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            if (disposing)
            {
                if (pictureBoxCellPreview?.Image != null) { pictureBoxCellPreview.Image.Dispose(); pictureBoxCellPreview.Image = null; }
                if (pictureBoxStitched?.Image != null) { pictureBoxStitched.Image.Dispose(); pictureBoxStitched.Image = null; }
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.dgvImg2D = new System.Windows.Forms.DataGridView();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.btnRemoveGroup = new System.Windows.Forms.Button();
            this.btnAddCol = new System.Windows.Forms.Button();
            this.btnRemoveCol = new System.Windows.Forms.Button();
            this.btnLoadCell = new System.Windows.Forms.Button();
            this.btnLoadGroup = new System.Windows.Forms.Button();
            this.btnClearCell = new System.Windows.Forms.Button();
            this.btnClearGroup = new System.Windows.Forms.Button();
            this.pictureBoxCellPreview = new System.Windows.Forms.PictureBox();
            this.lblModelKey = new System.Windows.Forms.Label();
            this.cmbModelKey = new System.Windows.Forms.ComboBox();
            this.lblImageBase64 = new System.Windows.Forms.Label();
            this.txtImageBase64 = new System.Windows.Forms.TextBox();
            this.btnLoadStitched = new System.Windows.Forms.Button();
            this.pictureBoxStitched = new System.Windows.Forms.PictureBox();
            this.chkShortRequest = new System.Windows.Forms.CheckBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtRequest = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImg2D)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStitched)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(12, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(1160, 23);
            this.txtUrl.TabIndex = 0;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 45);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(158, 17);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "组数(行): 0    每组最大张数(列): 0";
            // 
            // dgvImg2D
            // 
            this.dgvImg2D.Location = new System.Drawing.Point(12, 70);
            this.dgvImg2D.Name = "dgvImg2D";
            this.dgvImg2D.Size = new System.Drawing.Size(650, 260);
            this.dgvImg2D.TabIndex = 2;
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(12, 335);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(90, 25);
            this.btnAddGroup.TabIndex = 3;
            this.btnAddGroup.Text = "加组(行)";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // btnRemoveGroup
            // 
            this.btnRemoveGroup.Location = new System.Drawing.Point(108, 335);
            this.btnRemoveGroup.Name = "btnRemoveGroup";
            this.btnRemoveGroup.Size = new System.Drawing.Size(90, 25);
            this.btnRemoveGroup.TabIndex = 4;
            this.btnRemoveGroup.Text = "删组(行)";
            this.btnRemoveGroup.UseVisualStyleBackColor = true;
            this.btnRemoveGroup.Click += new System.EventHandler(this.btnRemoveGroup_Click);
            // 
            // btnAddCol
            // 
            this.btnAddCol.Location = new System.Drawing.Point(204, 335);
            this.btnAddCol.Name = "btnAddCol";
            this.btnAddCol.Size = new System.Drawing.Size(90, 25);
            this.btnAddCol.TabIndex = 5;
            this.btnAddCol.Text = "加列(张)";
            this.btnAddCol.UseVisualStyleBackColor = true;
            this.btnAddCol.Click += new System.EventHandler(this.btnAddCol_Click);
            // 
            // btnRemoveCol
            // 
            this.btnRemoveCol.Location = new System.Drawing.Point(300, 335);
            this.btnRemoveCol.Name = "btnRemoveCol";
            this.btnRemoveCol.Size = new System.Drawing.Size(90, 25);
            this.btnRemoveCol.TabIndex = 6;
            this.btnRemoveCol.Text = "删列(张)";
            this.btnRemoveCol.UseVisualStyleBackColor = true;
            this.btnRemoveCol.Click += new System.EventHandler(this.btnRemoveCol_Click);
            // 
            // btnLoadCell
            // 
            this.btnLoadCell.Location = new System.Drawing.Point(396, 335);
            this.btnLoadCell.Name = "btnLoadCell";
            this.btnLoadCell.Size = new System.Drawing.Size(120, 25);
            this.btnLoadCell.TabIndex = 7;
            this.btnLoadCell.Text = "填当前格(1张)";
            this.btnLoadCell.UseVisualStyleBackColor = true;
            this.btnLoadCell.Click += new System.EventHandler(this.btnLoadCell_Click);
            // 
            // btnLoadGroup
            // 
            this.btnLoadGroup.Location = new System.Drawing.Point(522, 335);
            this.btnLoadGroup.Name = "btnLoadGroup";
            this.btnLoadGroup.Size = new System.Drawing.Size(140, 25);
            this.btnLoadGroup.TabIndex = 8;
            this.btnLoadGroup.Text = "填当前组(多张)";
            this.btnLoadGroup.UseVisualStyleBackColor = true;
            this.btnLoadGroup.Click += new System.EventHandler(this.btnLoadGroup_Click);
            // 
            // btnClearCell
            // 
            this.btnClearCell.Location = new System.Drawing.Point(12, 365);
            this.btnClearCell.Name = "btnClearCell";
            this.btnClearCell.Size = new System.Drawing.Size(90, 25);
            this.btnClearCell.TabIndex = 9;
            this.btnClearCell.Text = "清当前格";
            this.btnClearCell.UseVisualStyleBackColor = true;
            this.btnClearCell.Click += new System.EventHandler(this.btnClearCell_Click);
            // 
            // btnClearGroup
            // 
            this.btnClearGroup.Location = new System.Drawing.Point(108, 365);
            this.btnClearGroup.Name = "btnClearGroup";
            this.btnClearGroup.Size = new System.Drawing.Size(90, 25);
            this.btnClearGroup.TabIndex = 10;
            this.btnClearGroup.Text = "清当前组";
            this.btnClearGroup.UseVisualStyleBackColor = true;
            this.btnClearGroup.Click += new System.EventHandler(this.btnClearGroup_Click);
            // 
            // pictureBoxCellPreview
            // 
            this.pictureBoxCellPreview.Location = new System.Drawing.Point(668, 70);
            this.pictureBoxCellPreview.Name = "pictureBoxCellPreview";
            this.pictureBoxCellPreview.Size = new System.Drawing.Size(240, 260);
            this.pictureBoxCellPreview.TabIndex = 11;
            this.pictureBoxCellPreview.TabStop = false;
            // 
            // lblModelKey
            // 
            this.lblModelKey.AutoSize = true;
            this.lblModelKey.Location = new System.Drawing.Point(668, 338);
            this.lblModelKey.Name = "lblModelKey";
            this.lblModelKey.Size = new System.Drawing.Size(67, 17);
            this.lblModelKey.TabIndex = 12;
            this.lblModelKey.Text = "model_key:";
            // 
            // cmbModelKey
            // 
            this.cmbModelKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbModelKey.FormattingEnabled = true;
            this.cmbModelKey.Location = new System.Drawing.Point(741, 335);
            this.cmbModelKey.Name = "cmbModelKey";
            this.cmbModelKey.Size = new System.Drawing.Size(167, 25);
            this.cmbModelKey.TabIndex = 13;
            // 
            // lblImageBase64
            // 
            this.lblImageBase64.AutoSize = true;
            this.lblImageBase64.Location = new System.Drawing.Point(12, 403);
            this.lblImageBase64.Name = "lblImageBase64";
            this.lblImageBase64.Size = new System.Drawing.Size(147, 17);
            this.lblImageBase64.TabIndex = 14;
            this.lblImageBase64.Text = "image_base64（拼接后图）:";
            // 
            // txtImageBase64
            // 
            this.txtImageBase64.Location = new System.Drawing.Point(12, 423);
            this.txtImageBase64.Multiline = true;
            this.txtImageBase64.Name = "txtImageBase64";
            this.txtImageBase64.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtImageBase64.Size = new System.Drawing.Size(650, 110);
            this.txtImageBase64.TabIndex = 15;
            this.txtImageBase64.WordWrap = false;
            // 
            // btnLoadStitched
            // 
            this.btnLoadStitched.Location = new System.Drawing.Point(668, 365);
            this.btnLoadStitched.Name = "btnLoadStitched";
            this.btnLoadStitched.Size = new System.Drawing.Size(120, 25);
            this.btnLoadStitched.TabIndex = 16;
            this.btnLoadStitched.Text = "上传拼接后图";
            this.btnLoadStitched.UseVisualStyleBackColor = true;
            this.btnLoadStitched.Click += new System.EventHandler(this.btnLoadStitched_Click);
            // 
            // pictureBoxStitched
            // 
            this.pictureBoxStitched.Location = new System.Drawing.Point(914, 70);
            this.pictureBoxStitched.Name = "pictureBoxStitched";
            this.pictureBoxStitched.Size = new System.Drawing.Size(258, 260);
            this.pictureBoxStitched.TabIndex = 17;
            this.pictureBoxStitched.TabStop = false;
            // 
            // chkShortRequest
            // 
            this.chkShortRequest.AutoSize = true;
            this.chkShortRequest.Location = new System.Drawing.Point(794, 369);
            this.chkShortRequest.Name = "chkShortRequest";
            this.chkShortRequest.Size = new System.Drawing.Size(159, 21);
            this.chkShortRequest.TabIndex = 18;
            this.chkShortRequest.Text = "Request短显示(防卡)";
            this.chkShortRequest.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(959, 338);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(90, 25);
            this.btnSend.TabIndex = 19;
            this.btnSend.Text = "发送检测";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtRequest
            // 
            this.txtRequest.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtRequest.Location = new System.Drawing.Point(668, 396);
            this.txtRequest.Multiline = true;
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequest.Size = new System.Drawing.Size(504, 137);
            this.txtRequest.TabIndex = 20;
            this.txtRequest.WordWrap = false;
            // 
            // txtResponse
            // 
            this.txtResponse.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtResponse.Location = new System.Drawing.Point(12, 540);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponse.Size = new System.Drawing.Size(1160, 180);
            this.txtResponse.TabIndex = 21;
            this.txtResponse.WordWrap = false;
            // 
            // TskEtchForm
            // 
            this.ClientSize = new System.Drawing.Size(1184, 741);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.txtRequest);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.chkShortRequest);
            this.Controls.Add(this.pictureBoxStitched);
            this.Controls.Add(this.btnLoadStitched);
            this.Controls.Add(this.txtImageBase64);
            this.Controls.Add(this.lblImageBase64);
            this.Controls.Add(this.cmbModelKey);
            this.Controls.Add(this.lblModelKey);
            this.Controls.Add(this.pictureBoxCellPreview);
            this.Controls.Add(this.btnClearGroup);
            this.Controls.Add(this.btnClearCell);
            this.Controls.Add(this.btnLoadGroup);
            this.Controls.Add(this.btnLoadCell);
            this.Controls.Add(this.btnRemoveCol);
            this.Controls.Add(this.btnAddCol);
            this.Controls.Add(this.btnRemoveGroup);
            this.Controls.Add(this.btnAddGroup);
            this.Controls.Add(this.dgvImg2D);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtUrl);
            this.Name = "TskEtchForm";
            this.Text = "TSK ETCH Detect API Tester";
            this.Load += new System.EventHandler(this.TskEtchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImg2D)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStitched)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView dgvImg2D;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.Button btnRemoveGroup;
        private System.Windows.Forms.Button btnAddCol;
        private System.Windows.Forms.Button btnRemoveCol;
        private System.Windows.Forms.Button btnLoadCell;
        private System.Windows.Forms.Button btnLoadGroup;
        private System.Windows.Forms.Button btnClearCell;
        private System.Windows.Forms.Button btnClearGroup;
        private System.Windows.Forms.PictureBox pictureBoxCellPreview;
        private System.Windows.Forms.Label lblModelKey;
        private System.Windows.Forms.ComboBox cmbModelKey;
        private System.Windows.Forms.Label lblImageBase64;
        private System.Windows.Forms.TextBox txtImageBase64;
        private System.Windows.Forms.Button btnLoadStitched;
        private System.Windows.Forms.PictureBox pictureBoxStitched;
        private System.Windows.Forms.CheckBox chkShortRequest;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.TextBox txtResponse;
    }
}
