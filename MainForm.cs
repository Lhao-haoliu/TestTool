using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TestTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            BuildMenu();
        }

        /// <summary>
        /// 在这里集中维护主页入口：只要加一行即可新增功能按钮。
        /// </summary>
        private void BuildMenu()
        {
            // 你可以按需要继续分组/调整顺序
            var sections = new List<MenuSection>
            {
                new MenuSection("接口合集", new List<MenuItem>
                {
                    new MenuItem("接口合集", () => new CombinedApiForm()),
                }),

                new MenuSection("参数自定义", new List<MenuItem>
                {
                    new MenuItem("参数自定义", () => new DynamicParamForm()),
                }),

                new MenuSection("其他", new List<MenuItem>
                {
                    // 如果你的 FormatJson.cs 是一个 Form（不是工具类），可以放开这一行：
                    // new MenuItem("Format Json", () => new FormatJson()),
                    new MenuItem("长截图Splice", () => new SpliceForm()),
                }),

                new MenuSection("批量处理", new List<MenuItem>
                {
                    new MenuItem("批量处理", () => new BatchRunForm()),
                }),

                // ✅ 新增：Excel处理（不改变原有界面格局，只是多一个分组）
                new MenuSection("Excel处理", new List<MenuItem>
                {
                    new MenuItem("Excel 清理工具", () => new ExcelScenarioCleanerConfigForm()),
                }),
            };

            // 清空并重建
            pnlMenuHost.Controls.Clear();

            foreach (var sec in sections)
            {
                pnlMenuHost.Controls.Add(CreateSection(sec));
            }
        }

        private Control CreateSection(MenuSection section)
        {
            var group = new GroupBox();
            group.Text = section.Title;
            group.AutoSize = true;
            group.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            group.Padding = new Padding(10);
            group.Margin = new Padding(8);

            var flow = new FlowLayoutPanel();
            flow.Dock = DockStyle.Fill;
            flow.WrapContents = true;
            flow.AutoSize = true;
            flow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flow.Margin = new Padding(0);
            flow.Padding = new Padding(0);

            foreach (var item in section.Items)
            {
                var btn = CreateMenuButton(item.Text);
                btn.Click += (s, e) => OpenForm(item.Factory);
                flow.Controls.Add(btn);
            }

            group.Controls.Add(flow);
            return group;
        }

        private Button CreateMenuButton(string text)
        {
            var btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 10F);
            btn.Size = new Size(200, 42);
            btn.Margin = new Padding(6);
            btn.UseVisualStyleBackColor = true;
            return btn;
        }

        private void OpenForm(Func<Form> factory)
        {
            try
            {
                var f = factory();
                // 作为非模态窗体打开
                f.StartPosition = FormStartPosition.CenterScreen;
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开窗体失败：" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private sealed class MenuSection
        {
            public string Title { get; private set; }
            public List<MenuItem> Items { get; private set; }

            public MenuSection(string title, List<MenuItem> items)
            {
                Title = title;
                Items = items ?? new List<MenuItem>();
            }
        }

        private sealed class MenuItem
        {
            public string Text { get; private set; }
            public Func<Form> Factory { get; private set; }

            public MenuItem(string text, Func<Form> factory)
            {
                Text = text;
                Factory = factory;
            }
        }
    }
}
