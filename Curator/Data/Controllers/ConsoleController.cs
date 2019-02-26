using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Data.Controllers
{
    public class ConsoleController
    {
        private CuratorDataSet.ConsoleDataTable Consoles;

        public ConsoleController(CuratorDataSet.ConsoleDataTable consoles)
        {
            Consoles = consoles;
        }

        public void UpdateName(string consoleName)
        {
            Form1.ActiveConsole.Name = consoleName;
        }

        public void AddEmulatorPath(string fileName)
        {
            Form1.ActiveConsole.EmulatorPath = fileName;
        }

        public void AddConsole(string consoleName)
        {
            if (!Consoles.Where(x => x.Name == consoleName).Any())
            {
                Consoles.Rows.Add(null, consoleName);
            }
        }

        public void SetEmulatorArgs(string emuArgs)
        {
            Form1.ActiveConsole.EmulatorArgs = emuArgs;
        }

        public void SetRomArgs(string romArgs)
        {
            Form1.ActiveConsole.RomArgs = romArgs;
        }
    }
}
