using OfficeOpenXml;
using System;
using System.Windows.Forms;

namespace TestTool
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            OfficeOpenXml.ExcelPackage.License.SetNonCommercialPersonal("TestTool");

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm()); // 一定是 MainForm
        }
    }
}
