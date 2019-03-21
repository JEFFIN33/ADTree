using System;
using System.Windows.Forms;

namespace ADTree_TestForm
{
    public static class ADT_Root
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ADT_TestForm());
        }
    }
}