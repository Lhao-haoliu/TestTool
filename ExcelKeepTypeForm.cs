using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TestTool
{
    public partial class ExcelScenarioCleanerConfigForm : Form
    {
        private string? _excelPath;

        public ExcelScenarioCleanerConfigForm()
        {
            InitializeComponent();

            numStartRow.Value = 4;
            btnProcess.Enabled = false;

            InitMappingGrid();
            LoadDefaultMappings(); // 默认先填一份，你也可删掉这行改成按钮触发
        }

        private void InitMappingGrid()
        {
            gridMappings.AutoGenerateColumns = false;
            gridMappings.AllowUserToAddRows = true;
            gridMappings.AllowUserToDeleteRows = true;
            gridMappings.RowHeadersVisible = false;
            gridMappings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            gridMappings.Columns.Clear();

            var colFunc = new DataGridViewTextBoxColumn
            {
                Name = "FunctionName",
                HeaderText = "功能名",
                Width = 220
            };
            var colFrom = new DataGridViewTextBoxColumn
            {
                Name = "FromCol",
                HeaderText = "起始列(如 D/4)",
                Width = 140
            };
            var colTo = new DataGridViewTextBoxColumn
            {
                Name = "ToCol",
                HeaderText = "结束列(如 J/10)",
                Width = 140
            };

            gridMappings.Columns.Add(colFunc);
            gridMappings.Columns.Add(colFrom);
            gridMappings.Columns.Add(colTo);
        }

        private void LoadDefaultMappings()
        {
            gridMappings.Rows.Clear();

            // 默认模板映射（你后续可在界面编辑）
            AddMapRow("OCR-Layer比对", "G", "M");
            AddMapRow("OCR-Name比对", "N", "W");
            AddMapRow("公版图比对", "X", "AF");
            AddMapRow("量测", "AG", "AO");
            AddMapRow("平滑度检测", "AP", "AT");

            RefreshFunctionChecklistFromGrid();
        }

        private void AddMapRow(string name, string from, string to)
        {
            gridMappings.Rows.Add(name, from, to);
        }

        private void btnLoadDefault_Click(object sender, EventArgs e)
        {
            LoadDefaultMappings();
        }

        private void btnSyncFuncList_Click(object sender, EventArgs e)
        {
            RefreshFunctionChecklistFromGrid();
        }

        private void RefreshFunctionChecklistFromGrid()
        {
            var names = ReadFunctionNamesFromGrid();

            checkedFunctions.Items.Clear();
            foreach (var n in names)
                checkedFunctions.Items.Add(n, false);

            lblFuncCount.Text = $"功能：{checkedFunctions.Items.Count}";
        }

        private List<string> ReadFunctionNamesFromGrid()
        {
            var list = new List<string>();

            foreach (DataGridViewRow row in gridMappings.Rows)
            {
                if (row.IsNewRow) continue;

                var name = (row.Cells["FunctionName"].Value?.ToString() ?? "").Trim();
                if (string.IsNullOrWhiteSpace(name)) continue;

                list.Add(name);
            }

            // 去重（保持顺序）
            var dedup = new List<string>();
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var n in list)
            {
                if (set.Add(n)) dedup.Add(n);
            }
            return dedup;
        }

        private void btnChooseExcel_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                Title = "选择Excel文件"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            _excelPath = ofd.FileName;
            txtExcelPath.Text = _excelPath;

            LoadSheets();
        }

        private void LoadSheets()
        {
            cmbSheets.Items.Clear();
            checkedTypes.Items.Clear();
            lblTypeCount.Text = "类型：0";
            btnProcess.Enabled = false;

            if (string.IsNullOrWhiteSpace(_excelPath) || !File.Exists(_excelPath))
            {
                MessageBox.Show("Excel路径无效。");
                return;
            }

            try
            {
                using var package = new ExcelPackage(new FileInfo(_excelPath));
                foreach (var ws in package.Workbook.Worksheets)
                    cmbSheets.Items.Add(ws.Name);

                if (cmbSheets.Items.Count == 0)
                {
                    MessageBox.Show("没有Sheet。");
                    return;
                }

                int idx = cmbSheets.Items.IndexOf("新格式");
                cmbSheets.SelectedIndex = idx >= 0 ? idx : 0;

                btnProcess.Enabled = true;
                LoadTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取Excel失败：" + ex.Message);
            }
        }

        private void cmbSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTypes();
        }

        private void btnReloadTypes_Click(object sender, EventArgs e)
        {
            LoadTypes();
        }

        private void LoadTypes()
        {
            checkedTypes.Items.Clear();
            lblTypeCount.Text = "类型：0";

            if (string.IsNullOrWhiteSpace(_excelPath) || cmbSheets.SelectedItem == null) return;

            string sheetName = cmbSheets.SelectedItem.ToString()!;
            int startRow = (int)numStartRow.Value;

            try
            {
                using var package = new ExcelPackage(new FileInfo(_excelPath));
                var ws = package.Workbook.Worksheets[sheetName];
                if (ws == null) return;

                int lastRow = ws.Dimension?.End.Row ?? 0;
                if (lastRow < startRow) return;

                // B列=类型
                var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                for (int r = startRow; r <= lastRow; r++)
                {
                    var t = ws.Cells[r, 2].Text?.Trim();
                    if (!string.IsNullOrWhiteSpace(t))
                        set.Add(t);
                }

                var list = set.OrderBy(x => x).ToList();
                foreach (var t in list) checkedTypes.Items.Add(t, false);
                lblTypeCount.Text = $"类型：{list.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取类型失败：" + ex.Message);
            }
        }

        private void btnFuncAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedFunctions.Items.Count; i++)
                checkedFunctions.SetItemChecked(i, true);
        }

        private void btnFuncNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedFunctions.Items.Count; i++)
                checkedFunctions.SetItemChecked(i, false);
        }

        private void btnTypeAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedTypes.Items.Count; i++)
                checkedTypes.SetItemChecked(i, true);
        }

        private void btnTypeNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedTypes.Items.Count; i++)
                checkedTypes.SetItemChecked(i, false);
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_excelPath) || !File.Exists(_excelPath))
            {
                MessageBox.Show("请先选择Excel文件。");
                return;
            }
            if (cmbSheets.SelectedItem == null)
            {
                MessageBox.Show("请选择Sheet。");
                return;
            }

            var keepTypes = checkedTypes.CheckedItems.Cast<object>()
                .Select(x => x.ToString()!)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (keepTypes.Count == 0)
            {
                MessageBox.Show("请至少勾选一种类型。");
                return;
            }

            var selectedFuncNames = checkedFunctions.CheckedItems.Cast<object>()
                .Select(x => x.ToString()!)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            if (selectedFuncNames.Count == 0)
            {
                MessageBox.Show("请至少勾选一个功能。");
                return;
            }

            // 从Grid读取映射并校验
            if (!TryReadMappings(out var mapping, out var err))
            {
                MessageBox.Show(err);
                return;
            }

            // 只取用户勾选的功能映射
            var keepRanges = new List<(int From, int To)>();
            foreach (var fn in selectedFuncNames)
            {
                if (!mapping.TryGetValue(fn, out var range))
                {
                    MessageBox.Show($"功能映射表中找不到：{fn}\n请点击“同步功能列表”或检查映射表。");
                    return;
                }
                keepRanges.Add(range);
            }

            string sheetName = cmbSheets.SelectedItem.ToString()!;
            int startRow = (int)numStartRow.Value;

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                Title = "另存为",
                FileName = Path.GetFileNameWithoutExtension(_excelPath) + "_filtered.xlsx"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            string savePath = sfd.FileName;

            try
            {
                using var package = new ExcelPackage(new FileInfo(_excelPath));
                var ws = package.Workbook.Worksheets[sheetName];
                if (ws == null)
                {
                    MessageBox.Show($"找不到Sheet：{sheetName}");
                    return;
                }

                int lastRow = ws.Dimension?.End.Row ?? 0;
                int lastCol = ws.Dimension?.End.Column ?? 0;
                if (lastRow < startRow || lastCol <= 0)
                {
                    MessageBox.Show("没有可处理的数据。");
                    return;
                }

                const int LAYER_COL = 1; // A 不清
                const int TYPE_COL = 2;  // B 默认不清

                bool IsColKept(int col)
                {
                    foreach (var (From, To) in keepRanges)
                        if (col >= From && col <= To) return true;
                    return false;
                }

                void ClearCell(int r, int c)
                {
                    var cell = ws.Cells[r, c];
                    cell.Value = null;
                    cell.Formula = string.Empty;
                    if (cell.RichText != null && cell.RichText.Count > 0) cell.RichText.Clear();
                    cell.Hyperlink = null;
                    if (cell.Comment != null && ws.Comments != null)
                        ws.Comments.Remove(cell.Comment);
                }

                var selectedTypeRows = new HashSet<int>();
                var otherRows = new HashSet<int>();

                for (int r = startRow; r <= lastRow; r++)
                {
                    var t = ws.Cells[r, TYPE_COL].Text?.Trim() ?? "";
                    if (!string.IsNullOrWhiteSpace(t) && keepTypes.Contains(t))
                        selectedTypeRows.Add(r);
                    else
                        otherRows.Add(r);
                }

                // 清单元格：只从C列开始（不动A/B）
                for (int r = startRow; r <= lastRow; r++)
                {
                    bool isSelectedType = selectedTypeRows.Contains(r);

                    for (int c = 3; c <= lastCol; c++)
                    {
                        if (isSelectedType && IsColKept(c)) continue;
                        ClearCell(r, c);
                    }

                    ws.Row(r).Height = ws.DefaultRowHeight;
                }

                // 图片删除：只要覆盖到“被清空区域”就删
                bool PicShouldDelete(int fromRow, int toRow, int fromCol, int toCol)
                {
                    for (int rr = fromRow; rr <= toRow; rr++)
                    {
                        if (rr < startRow || rr > lastRow) continue;

                        bool isSelectedType = selectedTypeRows.Contains(rr);

                        if (!isSelectedType)
                        {
                            if (RangesIntersect(fromCol, toCol, 3, lastCol)) return true;
                        }
                        else
                        {
                            if (!RangesIntersect(fromCol, toCol, 3, lastCol)) continue;

                            for (int cc = fromCol; cc <= toCol; cc++)
                            {
                                if (cc < 3 || cc > lastCol) continue;
                                if (!IsColKept(cc)) return true;
                            }
                        }
                    }
                    return false;
                }

                var drawings = ws.Drawings.ToList();
                foreach (var d in drawings)
                {
                    if (d is ExcelPicture pic)
                    {
                        int pr1 = pic.From.Row + 1, pr2 = pic.To.Row + 1;
                        if (pr1 > pr2) (pr1, pr2) = (pr2, pr1);

                        int pc1 = pic.From.Column + 1, pc2 = pic.To.Column + 1;
                        if (pc1 > pc2) (pc1, pc2) = (pc2, pc1);

                        if (PicShouldDelete(pr1, pr2, pc1, pc2))
                            ws.Drawings.Remove(d);
                    }
                }

                // merge 解除：只解除与“被清空区域”相交的 merge
                if (ws.MergedCells != null && ws.MergedCells.Count > 0)
                {
                    var mergeAddrs = ws.MergedCells.Where(a => !string.IsNullOrWhiteSpace(a)).ToList();

                    foreach (var addr in mergeAddrs)
                    {
                        ExcelAddressBase m;
                        try { m = new ExcelAddressBase(addr); }
                        catch { continue; }

                        bool needUnmerge = false;

                        for (int rr = m.Start.Row; rr <= m.End.Row; rr++)
                        {
                            if (rr < startRow || rr > lastRow) continue;

                            bool isSelectedType = selectedTypeRows.Contains(rr);

                            if (!isSelectedType)
                            {
                                if (RangesIntersect(m.Start.Column, m.End.Column, 3, lastCol))
                                {
                                    needUnmerge = true;
                                    break;
                                }
                            }
                            else
                            {
                                for (int cc = m.Start.Column; cc <= m.End.Column; cc++)
                                {
                                    if (cc < 3 || cc > lastCol) continue;
                                    if (!IsColKept(cc))
                                    {
                                        needUnmerge = true;
                                        break;
                                    }
                                }
                                if (needUnmerge) break;
                            }
                        }

                        if (needUnmerge)
                        {
                            try { ws.Cells[addr].Merge = false; } catch { }
                        }
                    }
                }

                package.SaveAs(new FileInfo(savePath));
                MessageBox.Show($"处理完成：\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("处理失败：" + ex.Message);
            }
        }

        private bool TryReadMappings(out Dictionary<string, (int From, int To)> map, out string error)
        {
            map = new Dictionary<string, (int From, int To)>(StringComparer.OrdinalIgnoreCase);
            error = "";

            foreach (DataGridViewRow row in gridMappings.Rows)
            {
                if (row.IsNewRow) continue;

                var name = (row.Cells["FunctionName"].Value?.ToString() ?? "").Trim();
                var fromRaw = (row.Cells["FromCol"].Value?.ToString() ?? "").Trim();
                var toRaw = (row.Cells["ToCol"].Value?.ToString() ?? "").Trim();

                if (string.IsNullOrWhiteSpace(name))
                    continue; // 空行跳过

                int from = ParseColumn(fromRaw);
                int to = ParseColumn(toRaw);

                if (from <= 0 || to <= 0)
                {
                    error = $"功能映射列输入不合法：{name}\n起始列={fromRaw}, 结束列={toRaw}\n支持：D/AA 或 4/27";
                    return false;
                }

                if (from > to) (from, to) = (to, from);

                map[name] = (from, to);
            }

            if (map.Count == 0)
            {
                error = "功能映射表为空：请在下方配置功能名/起始列/结束列。";
                return false;
            }

            return true;
        }

        private static int ParseColumn(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return -1;
            input = input.Trim();

            if (int.TryParse(input, out int n))
                return n;

            input = input.ToUpperInvariant();
            int col = 0;
            foreach (char ch in input)
            {
                if (ch < 'A' || ch > 'Z') return -1;
                col = col * 26 + (ch - 'A' + 1);
            }
            return col;
        }

        private static bool RangesIntersect(int aFrom, int aTo, int bFrom, int bTo)
        {
            if (aFrom > aTo) (aFrom, aTo) = (aTo, aFrom);
            if (bFrom > bTo) (bFrom, bTo) = (bTo, bFrom);
            return aFrom <= bTo && bFrom <= aTo;
        }
    }
}
