using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YazlabII_Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
        }

        public static void OnApplicationExit(object sender, EventArgs e)
        {
            MySocketClient.Instance.Disconnect();
        }
    }
}
