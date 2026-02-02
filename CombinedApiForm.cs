using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestTool
{
    public sealed class CombinedApiForm : Form
    {
        private readonly TabControl _tabControl = new TabControl();
        private readonly List<Form> _childForms = new List<Form>();

        public CombinedApiForm()
        {
            Text = "接口合集";
            Width = 1400;
            Height = 900;
            StartPosition = FormStartPosition.CenterScreen;

            _tabControl.Dock = DockStyle.Fill;
            _tabControl.Multiline = true;

            Controls.Add(_tabControl);

            AddTab("基础 / Detect", new DetectForm());
            AddTab("基础 / Detect Smooth", new DetectSmoothForm());
            AddTab("基础 / Measure", new MeasureForm());
            AddTab("基础 / Color", new ColorForm());
            AddTab("基础 / 单图 + Expect", new ExpectSingleForm());

            AddTab("Mirror / Detect", new MirrorDetectForm());
            AddTab("Mirror / Detect + OCR", new MirrorDetectOcrForm());

            AddTab("OCR / OCR", new OcrForm());
            AddTab("OCR / OCR-VL", new OcrVlForm());

            AddTab("黄光 / Detect + OCR", new HgDetectOcrForm());
            AddTab("黄光 / Similarity Check", new HgSimilarityCheckForm());

            AddTab("TSK / Detect", new TskDetectForm());
            AddTab("TSK / Etch", new TskEtchForm());
            AddTab("TSK / Similarity", new TskSimilarityForm());

            AddTab("整合 / ZH Detect + OCR", new ZhDetectOcrForm());
            AddTab("整合 / ZH Similarity Check", new ZhSimilarityCheckForm());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var child in _childForms)
                {
                    child.Dispose();
                }
                _childForms.Clear();
                _tabControl.Dispose();
            }
            base.Dispose(disposing);
        }

        private void AddTab(string title, Form form)
        {
            var page = new TabPage(title);
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Visible = true;
            page.Controls.Add(form);
            _tabControl.TabPages.Add(page);
            _childForms.Add(form);
        }
    }
}
