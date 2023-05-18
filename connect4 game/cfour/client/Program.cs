using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form2());

            Form2 login = new Form2();
            DialogResult result = login.ShowDialog();
            if (result == DialogResult.OK)
                Application.Run(new Form3(login.Username, login.NetworkStream));
        }
    }
}
