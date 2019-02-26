using System;
using Curator.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator
{
    public partial class Form1
    {
        public void UpdateSelectedRomDetails(CuratorDataSet.ROMRow rom)
        {
            romDetailsName.Text = rom.Name;
            romDetailsFolder.Text = _romFolderController.GetRomFolderById(rom.RomFolder_Id).Path;
            romDetailsCustomArgs.Text = rom.CustomArgs;
            romDetailsOverride.Checked = rom.OverrideArgs;
            romDetailsEnabledToggle.Checked = rom.Enabled;
        }
    }
}
