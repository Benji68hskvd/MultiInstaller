using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace WEZD
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            // V�rifie si l'application est ex�cut�e en tant qu'administrateur
            if (!ProgramLauncher.IsRunAsAdmin())
            {
                // Relance l'application en tant qu'administrateur
                ProgramLauncher.LaunchAsAdmin(Application.ExecutablePath);
                return; // Quitte le processus actuel apr�s avoir lanc� l'application avec �l�vation
            }

            // Code principal de l'application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}