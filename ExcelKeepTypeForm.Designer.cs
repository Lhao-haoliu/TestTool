using DocumentFormat.OpenXml.Spreadsheet;
using System.Windows.Forms;

namespace TestTool
{
    partial class ExcelScenarioCleanerConfigForm
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtExcelPath;
        private Button btnChooseExcel;

        private ComboBox cmbSheets;
        private Label labelSheet;

        private NumericUpDown numStartRow;
        private Label labelStartRow;

        private Button btnReloadTypes;

        private CheckedListBox checkedFunctions;
        private Label lblFuncCount;
        private Button btnFuncAll;
        private Button btnFuncNone;

        private CheckedListBox checkedTypes;
        private Label lblTypeCount;
        private Button btnTypeAll;
        private Button btnTypeNone;

        private DataGridView gridMappings;
        private Button btnLoadDefault;
        private Button btnSyncFuncList;

        private Button btnProcess;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtExcelPath = new TextBox();
            btnChooseExcel = new Button();
            cmbSheets = new ComboBox();
            labelSheet = new Label();
            numStartRow = new NumericUpDown();
            labelStartRow = new Label();
            btnReloadTypes = new Button();
            checkedFunctions = new CheckedListBox();
            lblFuncCount = new Label();
            btnFuncAll = new Button();
            btnFuncNone = new Button();
            checkedTypes = new CheckedListBox();
            lblTypeCount = new Label();
            btnTypeAll = new Button();
            btnTypeNone = new Button();
            gridMappings = new DataGridView();
            btnLoadDefault = new Button();
            btnSyncFuncList = new Button();
            btnProcess = new Button();
            ((System.ComponentModel.ISupportInitialize)numStartRow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridMappings).BeginInit();
            SuspendLayout();
            // 
            // txtExcelPath
            // 
            txtExcelPath.Location = new Point(12, 12);
            txtExcelPath.Name = "txtExcelPath";
            txtExcelPath.ReadOnly = true;
            txtExcelPath.Size = new Size(640, 23);
            txtExcelPath.TabIndex = 0;
            // 
            // btnChooseExcel
            // 
            btnChooseExcel.Location = new Point(660, 12);
            btnChooseExcel.Name = "btnChooseExcel";
            btnChooseExcel.Size = new Size(110, 23);
            btnChooseExcel.TabIndex = 1;
            btnChooseExcel.Text = "选择Excel";
            btnChooseExcel.Click += btnChooseExcel_Click;
            // 
            // cmbSheets
            // 
            cmbSheets.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSheets.Location = new Point(70, 44);
            cmbSheets.Name = "cmbSheets";
            cmbSheets.Size = new Size(180, 25);
            cmbSheets.TabIndex = 3;
            cmbSheets.SelectedIndexChanged += cmbSheets_SelectedIndexChanged;
            // 
            // labelSheet
            // 
            labelSheet.Location = new Point(12, 46);
            labelSheet.Name = "labelSheet";
            labelSheet.Size = new Size(60, 23);
            labelSheet.TabIndex = 2;
            labelSheet.Text = "Sheet：";
            // 
            // numStartRow
            // 
            numStartRow.Location = new Point(336, 41);
            numStartRow.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numStartRow.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numStartRow.Name = "numStartRow";
            numStartRow.Size = new Size(120, 23);
            numStartRow.TabIndex = 5;
            numStartRow.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // labelStartRow
            // 
            labelStartRow.Location = new Point(260, 46);
            labelStartRow.Name = "labelStartRow";
            labelStartRow.Size = new Size(70, 23);
            labelStartRow.TabIndex = 4;
            labelStartRow.Text = "起始行：";
            // 
            // btnReloadTypes
            // 
            btnReloadTypes.Location = new Point(660, 44);
            btnReloadTypes.Name = "btnReloadTypes";
            btnReloadTypes.Size = new Size(110, 23);
            btnReloadTypes.TabIndex = 6;
            btnReloadTypes.Text = "刷新类型";
            btnReloadTypes.Click += btnReloadTypes_Click;
            // 
            // checkedFunctions
            // 
            checkedFunctions.CheckOnClick = true;
            checkedFunctions.Location = new Point(12, 92);
            checkedFunctions.Name = "checkedFunctions";
            checkedFunctions.Size = new Size(360, 166);
            checkedFunctions.TabIndex = 10;
            // 
            // lblFuncCount
            // 
            lblFuncCount.Location = new Point(12, 72);
            lblFuncCount.Name = "lblFuncCount";
            lblFuncCount.Size = new Size(120, 16);
            lblFuncCount.TabIndex = 7;
            lblFuncCount.Text = "功能：0";
            // 
            // btnFuncAll
            // 
            btnFuncAll.Location = new Point(140, 68);
            btnFuncAll.Name = "btnFuncAll";
            btnFuncAll.Size = new Size(80, 22);
            btnFuncAll.TabIndex = 8;
            btnFuncAll.Text = "功能全选";
            btnFuncAll.Click += btnFuncAll_Click;
            // 
            // btnFuncNone
            // 
            btnFuncNone.Location = new Point(230, 68);
            btnFuncNone.Name = "btnFuncNone";
            btnFuncNone.Size = new Size(90, 22);
            btnFuncNone.TabIndex = 9;
            btnFuncNone.Text = "功能全不选";
            btnFuncNone.Click += btnFuncNone_Click;
            // 
            // checkedTypes
            // 
            checkedTypes.CheckOnClick = true;
            checkedTypes.Location = new Point(410, 92);
            checkedTypes.Name = "checkedTypes";
            checkedTypes.Size = new Size(360, 166);
            checkedTypes.TabIndex = 14;
            // 
            // lblTypeCount
            // 
            lblTypeCount.Location = new Point(410, 72);
            lblTypeCount.Name = "lblTypeCount";
            lblTypeCount.Size = new Size(120, 16);
            lblTypeCount.TabIndex = 11;
            lblTypeCount.Text = "类型：0";
            // 
            // btnTypeAll
            // 
            btnTypeAll.Location = new Point(540, 68);
            btnTypeAll.Name = "btnTypeAll";
            btnTypeAll.Size = new Size(80, 22);
            btnTypeAll.TabIndex = 12;
            btnTypeAll.Text = "类型全选";
            btnTypeAll.Click += btnTypeAll_Click;
            // 
            // btnTypeNone
            // 
            btnTypeNone.Location = new Point(630, 68);
            btnTypeNone.Name = "btnTypeNone";
            btnTypeNone.Size = new Size(90, 22);
            btnTypeNone.TabIndex = 13;
            btnTypeNone.Text = "类型全不选";
            btnTypeNone.Click += btnTypeNone_Click;
            // 
            // gridMappings
            // 
            gridMappings.Location = new Point(12, 310);
            gridMappings.Name = "gridMappings";
            gridMappings.Size = new Size(758, 170);
            gridMappings.TabIndex = 17;
            // 
            // btnLoadDefault
            // 
            btnLoadDefault.Location = new Point(12, 280);
            btnLoadDefault.Name = "btnLoadDefault";
            btnLoadDefault.Size = new Size(120, 24);
            btnLoadDefault.TabIndex = 15;
            btnLoadDefault.Text = "加载默认映射";
            btnLoadDefault.Click += btnLoadDefault_Click;
            // 
            // btnSyncFuncList
            // 
            btnSyncFuncList.Location = new Point(140, 280);
            btnSyncFuncList.Name = "btnSyncFuncList";
            btnSyncFuncList.Size = new Size(140, 24);
            btnSyncFuncList.TabIndex = 16;
            btnSyncFuncList.Text = "同步功能列表";
            btnSyncFuncList.Click += btnSyncFuncList_Click;
            // 
            // btnProcess
            // 
            btnProcess.Location = new Point(685, 486);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(85, 28);
            btnProcess.TabIndex = 18;
            btnProcess.Text = "执行";
            btnProcess.Click += btnProcess_Click;
            // 
            // ExcelScenarioCleanerConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 526);
            Controls.Add(txtExcelPath);
            Controls.Add(btnChooseExcel);
            Controls.Add(labelSheet);
            Controls.Add(cmbSheets);
            Controls.Add(labelStartRow);
            Controls.Add(numStartRow);
            Controls.Add(btnReloadTypes);
            Controls.Add(lblFuncCount);
            Controls.Add(btnFuncAll);
            Controls.Add(btnFuncNone);
            Controls.Add(checkedFunctions);
            Controls.Add(lblTypeCount);
            Controls.Add(btnTypeAll);
            Controls.Add(btnTypeNone);
            Controls.Add(checkedTypes);
            Controls.Add(btnLoadDefault);
            Controls.Add(btnSyncFuncList);
            Controls.Add(gridMappings);
            Controls.Add(btnProcess);
            Name = "ExcelScenarioCleanerConfigForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Excel 场景筛选清空（功能多选 + 类型多选 + 下方配置列范围）";
            ((System.ComponentModel.ISupportInitialize)numStartRow).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridMappings).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
