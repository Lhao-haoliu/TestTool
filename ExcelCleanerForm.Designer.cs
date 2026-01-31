namespace TestTool
{
    partial class ExcelCleanerForm
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
            this.txtExcelPath = new System.Windows.Forms.TextBox();
            this.btnChooseExcel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSheets = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStartRow = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtColFrom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtColTo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();

            // ✅ 新增：列段表格 + 操作按钮
            this.dgvRanges = new System.Windows.Forms.DataGridView();
            this.colFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddRange = new System.Windows.Forms.Button();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnClearRanges = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvRanges)).BeginInit();
            this.SuspendLayout();
            // 
            // txtExcelPath
            // 
            this.txtExcelPath.Location = new System.Drawing.Point(12, 12);
            this.txtExcelPath.Name = "txtExcelPath";
            this.txtExcelPath.ReadOnly = true;
            this.txtExcelPath.Size = new System.Drawing.Size(560, 23);
            this.txtExcelPath.TabIndex = 0;
            // 
            // btnChooseExcel
            // 
            this.btnChooseExcel.Location = new System.Drawing.Point(578, 11);
            this.btnChooseExcel.Name = "btnChooseExcel";
            this.btnChooseExcel.Size = new System.Drawing.Size(120, 25);
            this.btnChooseExcel.TabIndex = 1;
            this.btnChooseExcel.Text = "选择Excel";
            this.btnChooseExcel.UseVisualStyleBackColor = true;
            this.btnChooseExcel.Click += new System.EventHandler(this.btnChooseExcel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sheet：";
            // 
            // cmbSheets
            // 
            this.cmbSheets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSheets.FormattingEnabled = true;
            this.cmbSheets.Location = new System.Drawing.Point(68, 49);
            this.cmbSheets.Name = "cmbSheets";
            this.cmbSheets.Size = new System.Drawing.Size(258, 25);
            this.cmbSheets.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(352, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "起始行：";
            // 
            // txtStartRow
            // 
            this.txtStartRow.Location = new System.Drawing.Point(414, 49);
            this.txtStartRow.Name = "txtStartRow";
            this.txtStartRow.Size = new System.Drawing.Size(60, 23);
            this.txtStartRow.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "从列：";
            // 
            // txtColFrom
            // 
            this.txtColFrom.Location = new System.Drawing.Point(68, 83);
            this.txtColFrom.Name = "txtColFrom";
            this.txtColFrom.Size = new System.Drawing.Size(60, 23);
            this.txtColFrom.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(140, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "到列：";
            // 
            // txtColTo
            // 
            this.txtColTo.Location = new System.Drawing.Point(190, 83);
            this.txtColTo.Name = "txtColTo";
            this.txtColTo.Size = new System.Drawing.Size(60, 23);
            this.txtColTo.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(266, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "（例：A 到 AD，先填好再点“添加”）";
            // 
            // btnAddRange
            // 
            this.btnAddRange.Location = new System.Drawing.Point(498, 81);
            this.btnAddRange.Name = "btnAddRange";
            this.btnAddRange.Size = new System.Drawing.Size(70, 28);
            this.btnAddRange.TabIndex = 11;
            this.btnAddRange.Text = "添加";
            this.btnAddRange.UseVisualStyleBackColor = true;
            this.btnAddRange.Click += new System.EventHandler(this.btnAddRange_Click);
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.Location = new System.Drawing.Point(574, 81);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(60, 28);
            this.btnRemoveSelected.TabIndex = 12;
            this.btnRemoveSelected.Text = "删除";
            this.btnRemoveSelected.UseVisualStyleBackColor = true;
            this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
            // 
            // btnClearRanges
            // 
            this.btnClearRanges.Location = new System.Drawing.Point(638, 81);
            this.btnClearRanges.Name = "btnClearRanges";
            this.btnClearRanges.Size = new System.Drawing.Size(60, 28);
            this.btnClearRanges.TabIndex = 13;
            this.btnClearRanges.Text = "清空";
            this.btnClearRanges.UseVisualStyleBackColor = true;
            this.btnClearRanges.Click += new System.EventHandler(this.btnClearRanges_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(200, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "要删除的列段（可添加多条）：";
            // 
            // dgvRanges
            // 
            this.dgvRanges.AllowUserToAddRows = false;
            this.dgvRanges.AllowUserToDeleteRows = false;
            this.dgvRanges.ReadOnly = true;
            this.dgvRanges.MultiSelect = true;
            this.dgvRanges.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRanges.Location = new System.Drawing.Point(12, 140);
            this.dgvRanges.Name = "dgvRanges";
            this.dgvRanges.Size = new System.Drawing.Size(686, 180);
            this.dgvRanges.TabIndex = 15;

            this.dgvRanges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colFrom, this.colTo, this.colText
            });

            // 
            // colFrom
            // 
            this.colFrom.HeaderText = "从列";
            this.colFrom.Name = "colFrom";
            this.colFrom.Width = 80;
            // 
            // colTo
            // 
            this.colTo.HeaderText = "到列";
            this.colTo.Name = "colTo";
            this.colTo.Width = 80;
            // 
            // colText
            // 
            this.colText.HeaderText = "列段";
            this.colText.Name = "colText";
            this.colText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(12, 330);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(150, 30);
            this.btnRun.TabIndex = 16;
            this.btnRun.Text = "执行删除并另存";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(180, 337);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 17);
            this.lblStatus.TabIndex = 17;
            this.lblStatus.Text = "未加载";
            // 
            // ExcelCleanerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 372);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.dgvRanges);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnClearRanges);
            this.Controls.Add(this.btnRemoveSelected);
            this.Controls.Add(this.btnAddRange);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtColTo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtColFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtStartRow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSheets);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChooseExcel);
            this.Controls.Add(this.txtExcelPath);
            this.Name = "ExcelCleanerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel 清理工具（删除文字+图片）";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRanges)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtExcelPath;
        private System.Windows.Forms.Button btnChooseExcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSheets;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStartRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtColFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtColTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label lblStatus;

        // ✅ 新增控件
        private System.Windows.Forms.DataGridView dgvRanges;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colText;
        private System.Windows.Forms.Button btnAddRange;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnClearRanges;
        private System.Windows.Forms.Label label6;
    }
}
