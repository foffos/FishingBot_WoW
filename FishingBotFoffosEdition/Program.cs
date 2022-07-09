using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FishingBotFoffosEdition
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                File.WriteAllText($"CrashReport_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt", ex.StackTrace);
            }

        }
    }
}
