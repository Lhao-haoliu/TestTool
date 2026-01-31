using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TestTool
{
    public partial class ExcelCleanerForm : Form
    {
        private string _excelPath = "";

        public ExcelCleanerForm()
        {
            InitializeComponent();
            txtStartRow.Text = "4";
            txtColFrom.Text = "A";
            txtColTo.Text = "Z";
        }

        private void btnChooseExcel_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel (*.xlsx)|*.xlsx";
                ofd.Title = "选择要处理的 Excel 文件";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                _excelPath = ofd.FileName;
                txtExcelPath.Text = _excelPath;

                LoadSheets(_excelPath);
            }
        }

        private void LoadSheets(string path)
        {
            cmbSheets.Items.Clear();

            try
            {
                using (var wb = new XLWorkbook(path))
                {
                    foreach (var ws in wb.Worksheets)
                        cmbSheets.Items.Add(ws.Name);
                }

                if (cmbSheets.Items.Count > 0)
                    cmbSheets.SelectedIndex = 0;

                lblStatus.Text = $"已加载：{cmbSheets.Items.Count} 个Sheet";
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取Excel失败：" + ex.Message);
            }
        }

        // ✅ 添加列段到表格
        private void btnAddRange_Click(object sender, EventArgs e)
        {
            try
            {
                var fromText = (txtColFrom.Text ?? "").Trim();
                var toText = (txtColTo.Text ?? "").Trim();

                int from = ParseExcelColumn(fromText);
                int to = ParseExcelColumn(toText);

                if (from <= 0 || to <= 0)
                {
                    MessageBox.Show("列输入无效，请输入如 A / Z / AA / AD 这样的列名");
                    return;
                }

                if (from > to)
                {
                    int t = from; from = to; to = t;
                }

                string show = $"{ToExcelColumn(from)}-{ToExcelColumn(to)}";

                // 去重：同一段不重复加
                foreach (DataGridViewRow r in dgvRanges.Rows)
                {
                    if ((r.Cells[2].Value ?? "").ToString().Equals(show, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("该列段已存在");
                        return;
                    }
                }

                dgvRanges.Rows.Add(ToExcelColumn(from), ToExcelColumn(to), show);
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败：" + ex.Message);
            }
        }

        // ✅ 删除选中行
        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (dgvRanges.SelectedRows == null || dgvRanges.SelectedRows.Count == 0) return;

            foreach (DataGridViewRow r in dgvRanges.SelectedRows)
            {
                if (!r.IsNewRow)
                    dgvRanges.Rows.Remove(r);
            }
        }

        // ✅ 清空表格
        private void btnClearRanges_Click(object sender, EventArgs e)
        {
            dgvRanges.Rows.Clear();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_excelPath) || !File.Exists(_excelPath))
            {
                MessageBox.Show("请先选择 Excel 文件");
                return;
            }

            if (cmbSheets.SelectedItem == null)
            {
                MessageBox.Show("请选择 Sheet");
                return;
            }

            int startRow;
            if (!int.TryParse((txtStartRow.Text ?? "").Trim(), out startRow) || startRow < 1)
            {
                MessageBox.Show("起始行必须是 >= 1 的整数");
                return;
            }

            // ✅ 从表格取所有列段（没有就退回到单段输入）
            List<ColRange> ranges;
            try
            {
                ranges = GetRangesFromGridOrFallback();
            }
            catch (Exception ex)
            {
                MessageBox.Show("列段无效：" + ex.Message);
                return;
            }

            if (ranges.Count == 0)
            {
                MessageBox.Show("请先添加要删除的列段（点“添加”加入表格）");
                return;
            }

            ranges = MergeRanges(ranges);

            var sheetName = cmbSheets.SelectedItem.ToString();

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = Path.GetFileNameWithoutExtension(_excelPath) + "_cleaned.xlsx";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    CleanExcel(_excelPath, sfd.FileName, sheetName, startRow, ranges);
                    MessageBox.Show("处理完成，已生成新文件！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("处理失败：" + ex.Message);
                }
            }
        }

        private List<ColRange> GetRangesFromGridOrFallback()
        {
            var list = new List<ColRange>();

            foreach (DataGridViewRow r in dgvRanges.Rows)
            {
                var fromText = (r.Cells[0].Value ?? "").ToString().Trim();
                var toText = (r.Cells[1].Value ?? "").ToString().Trim();

                int from = ParseExcelColumn(fromText);
                int to = ParseExcelColumn(toText);
                if (from <= 0 || to <= 0) continue;

                if (from > to)
                {
                    int t = from; from = to; to = t;
                }

                list.Add(new ColRange(from, to));
            }

            // 如果表格没填，则使用单段输入作为兼容
            if (list.Count == 0)
            {
                var colFromText = (txtColFrom.Text ?? "").Trim();
                var colToText = (txtColTo.Text ?? "").Trim();

                int colFrom = ParseExcelColumn(colFromText);
                int colTo = ParseExcelColumn(colToText);
                if (colFrom <= 0 || colTo <= 0)
                    return new List<ColRange>(); // 交给上层提示

                if (colFrom > colTo)
                {
                    int t = colFrom; colFrom = colTo; colTo = t;
                }

                list.Add(new ColRange(colFrom, colTo));
            }

            return list;
        }

        /// <summary>
        /// 删除指定 sheet 内，从 startRow 开始、多个列段范围的文字和图片（浮动图片）。
        /// </summary>
        private void CleanExcel(string inputPath, string outputPath, string sheetName, int startRow, List<ColRange> ranges)
        {
            using (var wb = new XLWorkbook(inputPath))
            {
                var ws = wb.Worksheet(sheetName);

                int lastRowUsed = ws.LastRowUsed()?.RowNumber() ?? startRow;
                if (lastRowUsed < startRow) lastRowUsed = startRow;

                // 1) 删除图片：图片左上角单元格落在任何列段内 + 行>=startRow 即删
                var toDeletePics = new List<IXLPicture>();

                foreach (var pic in ws.Pictures)
                {
                    var anchor = pic.TopLeftCell;
                    if (anchor == null) continue;

                    int r = anchor.Address.RowNumber;
                    int c = anchor.Address.ColumnNumber;

                    if (r < startRow) continue;

                    if (IsInAnyRange(c, ranges))
                        toDeletePics.Add(pic);
                }

                foreach (var pic in toDeletePics)
                    pic.Delete();

                // 2) 清内容（保留样式）
                foreach (var rg in ranges)
                {
                    var rng = ws.Range(startRow, rg.From, lastRowUsed, rg.To);
                    rng.Clear(XLClearOptions.Contents);
                }

                wb.SaveAs(outputPath);
            }
        }

        private static bool IsInAnyRange(int col, List<ColRange> ranges)
        {
            for (int i = 0; i < ranges.Count; i++)
            {
                if (col >= ranges[i].From && col <= ranges[i].To) return true;
            }
            return false;
        }

        private struct ColRange
        {
            public int From;
            public int To;
            public ColRange(int from, int to) { From = from; To = to; }
        }

        private static List<ColRange> MergeRanges(List<ColRange> ranges)
        {
            if (ranges == null || ranges.Count == 0) return new List<ColRange>();
            var ordered = ranges.OrderBy(r => r.From).ThenBy(r => r.To).ToList();

            var merged = new List<ColRange>();
            ColRange cur = ordered[0];

            for (int i = 1; i < ordered.Count; i++)
            {
                var next = ordered[i];

                if (next.From <= cur.To + 1)
                {
                    cur = new ColRange(cur.From, Math.Max(cur.To, next.To));
                }
                else
                {
                    merged.Add(cur);
                    cur = next;
                }
            }

            merged.Add(cur);
            return merged;
        }

        /// <summary>
        /// Excel列名转数字：A->1, Z->26, AA->27, AD->30
        /// </summary>
        private static int ParseExcelColumn(string col)
        {
            if (string.IsNullOrWhiteSpace(col)) return -1;
            col = col.Trim().ToUpperInvariant();

            int sum = 0;
            for (int i = 0; i < col.Length; i++)
            {
                char ch = col[i];
                if (ch < 'A' || ch > 'Z') return -1;
                sum = sum * 26 + (ch - 'A' + 1);
            }
            return sum;
        }

        /// <summary>
        /// 数字转Excel列名：1->A, 26->Z, 27->AA
        /// </summary>
        private static string ToExcelColumn(int col)
        {
            if (col <= 0) return "";
            string s = "";
            int n = col;
            while (n > 0)
            {
                int rem = (n - 1) % 26;
                s = (char)('A' + rem) + s;
                n = (n - 1) / 26;
            }
            return s;
        }
    }
}
