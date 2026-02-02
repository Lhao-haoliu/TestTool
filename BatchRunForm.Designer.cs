namespace TestTool
{
    partial class BatchRunForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtPublicFolder = new System.Windows.Forms.TextBox();
            btnPickPublicFolder = new System.Windows.Forms.Button();
            txtTargetFolder = new System.Windows.Forms.TextBox();
            btnPickTargetFolder = new System.Windows.Forms.Button();
            labelPublicFolder = new System.Windows.Forms.Label();
            labelTargetFolder = new System.Windows.Forms.Label();

            txtUrl = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();

            labelMode = new System.Windows.Forms.Label();
            rbSingle = new System.Windows.Forms.RadioButton();
            rbCompare = new System.Windows.Forms.RadioButton();

            label2 = new System.Windows.Forms.Label();
            rbOnlyImage = new System.Windows.Forms.RadioButton();
            rbImageModel = new System.Windows.Forms.RadioButton();

            txtModelKey = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            txtExpect = new System.Windows.Forms.TextBox();
            labelExpect = new System.Windows.Forms.Label();

            btnRun = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            btnExport = new System.Windows.Forms.Button();

            dgv = new System.Windows.Forms.DataGridView();
            colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colMs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colOk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            colErr = new System.Windows.Forms.DataGridViewTextBoxColumn();

            progress = new System.Windows.Forms.ProgressBar();
            lblStatus = new System.Windows.Forms.Label();

            txtDetail = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(dgv)).BeginInit();
            SuspendLayout();

            // 公版文件夹
            labelPublicFolder.AutoSize = true;
            labelPublicFolder.Location = new System.Drawing.Point(12, 15);
            labelPublicFolder.Name = "labelPublicFolder";
            labelPublicFolder.Size = new System.Drawing.Size(80, 17);
            labelPublicFolder.TabIndex = 0;
            labelPublicFolder.Text = "公版文件夹：";

            txtPublicFolder.Location = new System.Drawing.Point(92, 12);
            txtPublicFolder.Name = "txtPublicFolder";
            txtPublicFolder.Size = new System.Drawing.Size(560, 23);
            txtPublicFolder.TabIndex = 1;

            btnPickPublicFolder.Location = new System.Drawing.Point(658, 10);
            btnPickPublicFolder.Name = "btnPickPublicFolder";
            btnPickPublicFolder.Size = new System.Drawing.Size(90, 25);
            btnPickPublicFolder.TabIndex = 2;
            btnPickPublicFolder.Text = "选择公版";
            btnPickPublicFolder.UseVisualStyleBackColor = true;
            btnPickPublicFolder.Click += btnPickPublicFolder_Click;

            // 目标文件夹
            labelTargetFolder.AutoSize = true;
            labelTargetFolder.Location = new System.Drawing.Point(12, 44);
            labelTargetFolder.Name = "labelTargetFolder";
            labelTargetFolder.Size = new System.Drawing.Size(80, 17);
            labelTargetFolder.TabIndex = 3;
            labelTargetFolder.Text = "目标文件夹：";

            txtTargetFolder.Location = new System.Drawing.Point(92, 41);
            txtTargetFolder.Name = "txtTargetFolder";
            txtTargetFolder.Size = new System.Drawing.Size(560, 23);
            txtTargetFolder.TabIndex = 4;

            btnPickTargetFolder.Location = new System.Drawing.Point(658, 39);
            btnPickTargetFolder.Name = "btnPickTargetFolder";
            btnPickTargetFolder.Size = new System.Drawing.Size(90, 25);
            btnPickTargetFolder.TabIndex = 5;
            btnPickTargetFolder.Text = "选择目标";
            btnPickTargetFolder.UseVisualStyleBackColor = true;
            btnPickTargetFolder.Click += btnPickTargetFolder_Click;

            // 接口URL
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 74);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(67, 17);
            label1.TabIndex = 6;
            label1.Text = "接口URL：";

            txtUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            txtUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            txtUrl.FormattingEnabled = true;
            txtUrl.Location = new System.Drawing.Point(77, 71);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new System.Drawing.Size(671, 25);
            txtUrl.TabIndex = 7;

            // 接口类型
            labelMode.AutoSize = true;
            labelMode.Location = new System.Drawing.Point(12, 104);
            labelMode.Name = "labelMode";
            labelMode.Size = new System.Drawing.Size(68, 17);
            labelMode.TabIndex = 8;
            labelMode.Text = "接口类型：";

            rbSingle.AutoSize = true;
            rbSingle.Location = new System.Drawing.Point(77, 102);
            rbSingle.Name = "rbSingle";
            rbSingle.Size = new System.Drawing.Size(75, 21);
            rbSingle.TabIndex = 9;
            rbSingle.TabStop = true;
            rbSingle.Text = "单图接口";
            rbSingle.UseVisualStyleBackColor = true;
            rbSingle.CheckedChanged += rbSingle_CheckedChanged;

            rbCompare.AutoSize = true;
            rbCompare.Location = new System.Drawing.Point(160, 102);
            rbCompare.Name = "rbCompare";
            rbCompare.Size = new System.Drawing.Size(123, 21);
            rbCompare.TabIndex = 10;
            rbCompare.TabStop = true;
            rbCompare.Text = "双图比对(similarity)";
            rbCompare.UseVisualStyleBackColor = true;
            rbCompare.CheckedChanged += rbCompare_CheckedChanged;

            // 请求模式（仅单图启用）
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 133);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(68, 17);
            label2.TabIndex = 11;
            label2.Text = "请求模式：";

            rbOnlyImage.AutoSize = true;
            rbOnlyImage.Location = new System.Drawing.Point(77, 131);
            rbOnlyImage.Name = "rbOnlyImage";
            rbOnlyImage.Size = new System.Drawing.Size(125, 21);
            rbOnlyImage.TabIndex = 12;
            rbOnlyImage.TabStop = true;
            rbOnlyImage.Text = "仅 image_base64";
            rbOnlyImage.UseVisualStyleBackColor = true;

            rbImageModel.AutoSize = true;
            rbImageModel.Location = new System.Drawing.Point(210, 131);
            rbImageModel.Name = "rbImageModel";
            rbImageModel.Size = new System.Drawing.Size(188, 21);
            rbImageModel.TabIndex = 13;
            rbImageModel.TabStop = true;
            rbImageModel.Text = "image_base64 + model_key";
            rbImageModel.UseVisualStyleBackColor = true;

            // model_key
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(400, 133);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(82, 17);
            label3.TabIndex = 14;
            label3.Text = "model_key：";

            txtModelKey.Location = new System.Drawing.Point(471, 129);
            txtModelKey.Name = "txtModelKey";
            txtModelKey.Size = new System.Drawing.Size(100, 23);
            txtModelKey.TabIndex = 15;

            // expect
            labelExpect.AutoSize = true;
            labelExpect.Location = new System.Drawing.Point(580, 133);
            labelExpect.Name = "labelExpect";
            labelExpect.Size = new System.Drawing.Size(56, 17);
            labelExpect.TabIndex = 16;
            labelExpect.Text = "expect：";

            txtExpect.Location = new System.Drawing.Point(640, 129);
            txtExpect.Name = "txtExpect";
            txtExpect.Size = new System.Drawing.Size(120, 23);
            txtExpect.TabIndex = 17;

            // 按钮
            btnRun.Location = new System.Drawing.Point(12, 160);
            btnRun.Name = "btnRun";
            btnRun.Size = new System.Drawing.Size(90, 28);
            btnRun.TabIndex = 18;
            btnRun.Text = "开始执行";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;

            btnCancel.Enabled = false;
            btnCancel.Location = new System.Drawing.Point(108, 160);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(90, 28);
            btnCancel.TabIndex = 19;
            btnCancel.Text = "取消";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;

            btnExport.Enabled = false;
            btnExport.Location = new System.Drawing.Point(204, 160);
            btnExport.Name = "btnExport";
            btnExport.Size = new System.Drawing.Size(140, 28);
            btnExport.TabIndex = 20;
            btnExport.Text = "导出Excel(含图)";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;

            progress.Location = new System.Drawing.Point(360, 164);
            progress.Name = "progress";
            progress.Size = new System.Drawing.Size(310, 18);
            progress.TabIndex = 21;

            lblStatus.AutoSize = true;
            lblStatus.Location = new System.Drawing.Point(680, 166);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(35, 17);
            lblStatus.TabIndex = 22;
            lblStatus.Text = "0 / 0";

            // 表格
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                colIndex, colFile, colCode, colMs, colOk, colErr
            });
            dgv.Location = new System.Drawing.Point(12, 194);
            dgv.MultiSelect = false;
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new System.Drawing.Size(736, 260);
            dgv.TabIndex = 21;
            dgv.CellDoubleClick += dgv_CellDoubleClick;

            colIndex.DataPropertyName = "Index";
            colIndex.HeaderText = "#";
            colIndex.Name = "colIndex";
            colIndex.ReadOnly = true;
            colIndex.Width = 55;

            // 表格主列（仍显示 TargetFileName；双图时你可以看详情或导出看 Public/Target）
            colFile.DataPropertyName = "TargetFileName";
            colFile.HeaderText = "目标文件";
            colFile.Name = "colFile";
            colFile.ReadOnly = true;
            colFile.Width = 260;

            colCode.DataPropertyName = "StatusCode";
            colCode.HeaderText = "HTTP";
            colCode.Name = "colCode";
            colCode.ReadOnly = true;
            colCode.Width = 60;

            colMs.DataPropertyName = "ElapsedMs";
            colMs.HeaderText = "耗时(ms)";
            colMs.Name = "colMs";
            colMs.ReadOnly = true;
            colMs.Width = 80;

            colOk.DataPropertyName = "Ok";
            colOk.HeaderText = "OK";
            colOk.Name = "colOk";
            colOk.ReadOnly = true;
            colOk.Width = 50;

            colErr.DataPropertyName = "Error";
            colErr.HeaderText = "错误/备注";
            colErr.Name = "colErr";
            colErr.ReadOnly = true;
            colErr.Width = 260;

            // 详情
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(12, 465);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(152, 17);
            label4.TabIndex = 22;
            label4.Text = "双击表格行查看完整响应：";

            txtDetail.Location = new System.Drawing.Point(12, 484);
            txtDetail.Multiline = true;
            txtDetail.Name = "txtDetail";
            txtDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtDetail.Size = new System.Drawing.Size(736, 180);
            txtDetail.TabIndex = 23;
            txtDetail.WordWrap = false;

            // Form
            ClientSize = new System.Drawing.Size(760, 680);
            Controls.Add(txtDetail);
            Controls.Add(label4);
            Controls.Add(lblStatus);
            Controls.Add(progress);
            Controls.Add(dgv);
            Controls.Add(btnExport);
            Controls.Add(btnCancel);
            Controls.Add(btnRun);
            Controls.Add(txtExpect);
            Controls.Add(labelExpect);
            Controls.Add(txtModelKey);
            Controls.Add(label3);
            Controls.Add(rbImageModel);
            Controls.Add(rbOnlyImage);
            Controls.Add(label2);
            Controls.Add(rbCompare);
            Controls.Add(rbSingle);
            Controls.Add(labelMode);
            Controls.Add(txtUrl);
            Controls.Add(label1);
            Controls.Add(btnPickTargetFolder);
            Controls.Add(txtTargetFolder);
            Controls.Add(labelTargetFolder);
            Controls.Add(btnPickPublicFolder);
            Controls.Add(txtPublicFolder);
            Controls.Add(labelPublicFolder);

            Name = "BatchRunForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Batch Runner - 批量图片接口测试";

            ((System.ComponentModel.ISupportInitialize)(dgv)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtPublicFolder;
        private System.Windows.Forms.Button btnPickPublicFolder;
        private System.Windows.Forms.TextBox txtTargetFolder;
        private System.Windows.Forms.Button btnPickTargetFolder;
        private System.Windows.Forms.Label labelPublicFolder;
        private System.Windows.Forms.Label labelTargetFolder;

        private System.Windows.Forms.ComboBox txtUrl;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.RadioButton rbSingle;
        private System.Windows.Forms.RadioButton rbCompare;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbOnlyImage;
        private System.Windows.Forms.RadioButton rbImageModel;

        private System.Windows.Forms.TextBox txtModelKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtExpect;
        private System.Windows.Forms.Label labelExpect;

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnExport;

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colOk;
        private System.Windows.Forms.DataGridViewTextBoxColumn colErr;
    }
}
