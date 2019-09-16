using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Curator.Data.Controllers
{
    public class ConsoleController
    {
        private CuratorDataSet.ConsoleDataTable Consoles;

        public ConsoleController(CuratorDataSet.ConsoleDataTable consoles)
        {
            Consoles = consoles;
        }

        public void SetActiveConsole(CuratorDataSet.ConsoleRow console)
        {
            Form1.ActiveConsole = console;
        }

        public void UpdateName(string consoleName)
        {
            Form1.ActiveConsole.Name = consoleName;
        }

        public void AddEmulatorPath(string fileName)
        {
            Form1.ActiveConsole.EmulatorPath = fileName;
        }

        public bool Add(string consoleName)
        {
            if (!Consoles.Where(x => x.RowState != System.Data.DataRowState.Deleted).Where(x => x.Name == consoleName).Any() && !string.IsNullOrWhiteSpace(consoleName))
            {
                Consoles.Rows.Add(null, consoleName);
                return true;
            }
            return false;
        }

        public List<CuratorDataSet.ConsoleRow> GetAllConsoles()
        {
            return Consoles.ToList();
        }

        public void Remove(string name)
        {
            Consoles.Rows.Remove(Consoles.Where(x => x.RowState != System.Data.DataRowState.Deleted).Where(x => x.Name == name).First());
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
