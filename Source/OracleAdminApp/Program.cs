using System;
using System.Windows.Forms;

namespace OracleSecurityAdmin
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Chạy Form Đăng nhập đầu tiên
            Application.Run(new LoginForm());
        }
    }
}