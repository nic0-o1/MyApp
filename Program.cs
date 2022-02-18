using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Squirrel;

namespace MyApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static bool ShowTheWelcomeWizard;
        [STAThread]
        static async Task Main()
        {
            using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/nic0-o1/MyApp"))
            {
                await mgr.Result.UpdateApp();
            }

            using (var mgr = new UpdateManager("https://github.com/nic0-o1/MyApp"))
            {
                SquirrelAwareApp.HandleEvents(
                  onInitialInstall: v => mgr.CreateShortcutForThisExe(),
                  onAppUpdate: v => mgr.CreateShortcutForThisExe(),
                  onAppUninstall: v => mgr.RemoveShortcutForThisExe(),
                  onFirstRun: () => ShowTheWelcomeWizard = true);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
