namespace TestTool
{
    partial class SpliceForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            if (disposing)
            {
                if (pictureBoxCellPreview?.Image != null) { pictureBoxCellPreview.Image.Dispose(); pictureBoxCellPreview.Image = null; }
                if (pictureBoxResult?.Image != null) { pictureBoxResult.Image.Dispose(); pictureBoxResult.Image = null; }
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.dgvGrid = new System.Windows.Forms.DataGridView();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnRemoveRow = new System.Windows.Forms.Button();
            this.btnAddCol = new System.Windows.Forms.Button();
            this.btnRemoveCol = new System.Windows.Forms.Button();
            this.btnLoadToCell = new System.Windows.Forms.Button();
            this.btnLoadToRow = new System.Windows.Forms.Button();
            this.btnClearCell = new System.Windows.Forms.Button();
            this.btnClearRow = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.chkShortRequest = new System.Windows.Forms.CheckBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pictureBoxCellPreview = new System.Windows.Forms.PictureBox();
            this.pictureBoxResult = new System.Windows.Forms.PictureBox();
            this.txtRequest = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.btnSaveResult = new System.Windows.Forms.Button();
            this.lblResultInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(12, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(1200, 23);
            this.txtUrl.TabIndex = 0;
            // 
            // dgvGrid
            // 
            this.dgvGrid.Location = new System.Drawing.Point(12, 70);
            this.dgvGrid.Name = "dgvGrid";
            this.dgvGrid.Size = new System.Drawing.Size(650, 280);
            this.dgvGrid.TabIndex = 1;
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(12, 41);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(90, 25);
            this.btnAddRow.TabIndex = 2;
            this.btnAddRow.Text = "加组(行)";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnRemoveRow
            // 
            this.btnRemoveRow.Location = new System.Drawing.Point(108, 41);
            this.btnRemoveRow.Name = "btnRemoveRow";
            this.btnRemoveRow.Size = new System.Drawing.Size(90, 25);
            this.btnRemoveRow.TabIndex = 3;
            this.btnRemoveRow.Text = "删组(行)";
            this.btnRemoveRow.UseVisualStyleBackColor = true;
            this.btnRemoveRow.Click += new System.EventHandler(this.btnRemoveRow_Click);
            // 
            // btnAddCol
            // 
            this.btnAddCol.Location = new System.Drawing.Point(204, 41);
            this.btnAddCol.Name = "btnAddCol";
            this.btnAddCol.Size = new System.Drawing.Size(90, 25);
            this.btnAddCol.TabIndex = 4;
            this.btnAddCol.Text = "加列(张)";
            this.btnAddCol.UseVisualStyleBackColor = true;
            this.btnAddCol.Click += new System.EventHandler(this.btnAddCol_Click);
            // 
            // btnRemoveCol
            // 
            this.btnRemoveCol.Location = new System.Drawing.Point(300, 41);
            this.btnRemoveCol.Name = "btnRemoveCol";
            this.btnRemoveCol.Size = new System.Drawing.Size(90, 25);
            this.btnRemoveCol.TabIndex = 5;
            this.btnRemoveCol.Text = "删列(张)";
            this.btnRemoveCol.UseVisualStyleBackColor = true;
            this.btnRemoveCol.Click += new System.EventHandler(this.btnRemoveCol_Click);
            // 
            // btnLoadToCell
            // 
            this.btnLoadToCell.Location = new System.Drawing.Point(396, 41);
            this.btnLoadToCell.Name = "btnLoadToCell";
            this.btnLoadToCell.Size = new System.Drawing.Size(120, 25);
            this.btnLoadToCell.TabIndex = 6;
            this.btnLoadToCell.Text = "填当前格(1张)";
            this.btnLoadToCell.UseVisualStyleBackColor = true;
            this.btnLoadToCell.Click += new System.EventHandler(this.btnLoadToCell_Click);
            // 
            // btnLoadToRow
            // 
            this.btnLoadToRow.Location = new System.Drawing.Point(522, 41);
            this.btnLoadToRow.Name = "btnLoadToRow";
            this.btnLoadToRow.Size = new System.Drawing.Size(140, 25);
            this.btnLoadToRow.TabIndex = 7;
            this.btnLoadToRow.Text = "填当前组(多张)";
            this.btnLoadToRow.UseVisualStyleBackColor = true;
            this.btnLoadToRow.Click += new System.EventHandler(this.btnLoadToRow_Click);
            // 
            // btnClearCell
            // 
            this.btnClearCell.Location = new System.Drawing.Point(668, 41);
            this.btnClearCell.Name = "btnClearCell";
            this.btnClearCell.Size = new System.Drawing.Size(90, 25);
            this.btnClearCell.TabIndex = 8;
            this.btnClearCell.Text = "清空当前格";
            this.btnClearCell.UseVisualStyleBackColor = true;
            this.btnClearCell.Click += new System.EventHandler(this.btnClearCell_Click);
            // 
            // btnClearRow
            // 
            this.btnClearRow.Location = new System.Drawing.Point(764, 41);
            this.btnClearRow.Name = "btnClearRow";
            this.btnClearRow.Size = new System.Drawing.Size(110, 25);
            this.btnClearRow.TabIndex = 9;
            this.btnClearRow.Text = "清空当前组";
            this.btnClearRow.UseVisualStyleBackColor = true;
            this.btnClearRow.Click += new System.EventHandler(this.btnClearRow_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(880, 41);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(90, 25);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "发送拼接";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // chkShortRequest
            // 
            this.chkShortRequest.AutoSize = true;
            this.chkShortRequest.Checked = true;
            this.chkShortRequest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShortRequest.Location = new System.Drawing.Point(976, 45);
            this.chkShortRequest.Name = "chkShortRequest";
            this.chkShortRequest.Size = new System.Drawing.Size(159, 21);
            this.chkShortRequest.TabIndex = 11;
            this.chkShortRequest.Text = "Request短显示(防卡)";
            this.chkShortRequest.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 355);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(158, 17);
            this.lblInfo.TabIndex = 12;
            this.lblInfo.Text = "组数(行): 0    每组最大张数(列): 0";
            // 
            // pictureBoxCellPreview
            // 
            this.pictureBoxCellPreview.Location = new System.Drawing.Point(668, 70);
            this.pictureBoxCellPreview.Name = "pictureBoxCellPreview";
            this.pictureBoxCellPreview.Size = new System.Drawing.Size(260, 280);
            this.pictureBoxCellPreview.TabIndex = 13;
            this.pictureBoxCellPreview.TabStop = false;
            // 
            // pictureBoxResult
            // 
            this.pictureBoxResult.Location = new System.Drawing.Point(934, 70);
            this.pictureBoxResult.Name = "pictureBoxResult";
            this.pictureBoxResult.Size = new System.Drawing.Size(278, 280);
            this.pictureBoxResult.TabIndex = 14;
            this.pictureBoxResult.TabStop = false;
            // 
            // txtRequest
            // 
            this.txtRequest.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtRequest.Location = new System.Drawing.Point(12, 375);
            this.txtRequest.Multiline = true;
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequest.Size = new System.Drawing.Size(600, 180);
            this.txtRequest.TabIndex = 15;
            this.txtRequest.WordWrap = false;
            // 
            // txtResponse
            // 
            this.txtResponse.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtResponse.Location = new System.Drawing.Point(618, 375);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponse.Size = new System.Drawing.Size(594, 180);
            this.txtResponse.TabIndex = 16;
            this.txtResponse.WordWrap = false;
            // 
            // btnSaveResult
            // 
            this.btnSaveResult.Location = new System.Drawing.Point(1122, 352);
            this.btnSaveResult.Name = "btnSaveResult";
            this.btnSaveResult.Size = new System.Drawing.Size(90, 23);
            this.btnSaveResult.TabIndex = 17;
            this.btnSaveResult.Text = "保存结果图";
            this.btnSaveResult.UseVisualStyleBackColor = true;
            this.btnSaveResult.Click += new System.EventHandler(this.btnSaveResult_Click);
            // 
            // lblResultInfo
            // 
            this.lblResultInfo.AutoSize = true;
            this.lblResultInfo.Location = new System.Drawing.Point(934, 355);
            this.lblResultInfo.Name = "lblResultInfo";
            this.lblResultInfo.Size = new System.Drawing.Size(104, 17);
            this.lblResultInfo.TabIndex = 18;
            this.lblResultInfo.Text = "结果尺寸: - x -";
            // 
            // SpliceForm
            // 
            this.ClientSize = new System.Drawing.Size(1224, 571);
            this.Controls.Add(this.lblResultInfo);
            this.Controls.Add(this.btnSaveResult);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.txtRequest);
            this.Controls.Add(this.pictureBoxResult);
            this.Controls.Add(this.pictureBoxCellPreview);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.chkShortRequest);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClearRow);
            this.Controls.Add(this.btnClearCell);
            this.Controls.Add(this.btnLoadToRow);
            this.Controls.Add(this.btnLoadToCell);
            this.Controls.Add(this.btnRemoveCol);
            this.Controls.Add(this.btnAddCol);
            this.Controls.Add(this.btnRemoveRow);
            this.Controls.Add(this.btnAddRow);
            this.Controls.Add(this.dgvGrid);
            this.Controls.Add(this.txtUrl);
            this.Name = "SpliceForm";
            this.Text = "长图拼接 Splice API Tester";
            this.Load += new System.EventHandler(this.SpliceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.DataGridView dgvGrid;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnRemoveRow;
        private System.Windows.Forms.Button btnAddCol;
        private System.Windows.Forms.Button btnRemoveCol;
        private System.Windows.Forms.Button btnLoadToCell;
        private System.Windows.Forms.Button btnLoadToRow;
        private System.Windows.Forms.Button btnClearCell;
        private System.Windows.Forms.Button btnClearRow;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.CheckBox chkShortRequest;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.PictureBox pictureBoxCellPreview;
        private System.Windows.Forms.PictureBox pictureBoxResult;
        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Button btnSaveResult;
        private System.Windows.Forms.Label lblResultInfo;
    }
}
