using System;
using System.Windows.Forms;

namespace TestTool
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm()); // 一定是 MainForm
        }
    }
}
