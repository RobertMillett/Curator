using System.Linq;

namespace Curator.Data.Controllers
{
    public class ConsoleController
    {
        private CuratorDataSet.ConsoleDataTable Consoles;

        public ConsoleController(CuratorDataSet.ConsoleDataTable consoles)
        {
            Consoles = consoles;
        }

        public void SetActiveConsole(string name)
        {
            Form1.ActiveConsole = name == string.Empty ? null : Consoles.Where(x => x.Name == name).First();
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
            if (!Consoles.Where(x => x.Name == consoleName).Any())
            {
                Consoles.Rows.Add(null, consoleName);
                return true;
            }
            return false;
        }

        public void Remove(string name)
        {
            Consoles.Rows.Remove(Consoles.Where(x => x.Name == name).First());
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
