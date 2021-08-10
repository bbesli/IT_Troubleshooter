using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TroubleshooterUI.Business.Abstract;
using TroubleshooterUI.Business.Concrete;

namespace TroubleshooterUI
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
            ICommands _cmd = new Commands();
            Application.Run(new Form1(_cmd));
        }
    }
}
