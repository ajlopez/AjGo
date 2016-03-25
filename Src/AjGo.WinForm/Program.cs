using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AjGo.WinForm
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
            Matches.LoadMatches();
            Application.Run(new AjGoForm());
        }
    }
}