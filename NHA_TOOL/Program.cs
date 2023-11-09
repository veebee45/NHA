using NHA_TOOL.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NHA_TOOL
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
            Application.Run(new Form4());
            //Application.Run(new NHA_24UPS(@"A:\ColorPlast\Demo\NHAFiles\NHA_GeneratedFiles\04.10.2023\Balrampur\",1));
        }
    }
}
