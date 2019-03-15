﻿using System;
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
            if (!Consoles.Where(x => x.Name == consoleName).Any() && !string.IsNullOrWhiteSpace(consoleName))
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
