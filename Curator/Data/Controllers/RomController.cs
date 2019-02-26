using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Curator.Data.Controllers
{
    public class RomController
    {
        private CuratorDataSet.ROMDataTable RomData;

        public RomController(CuratorDataSet.ROMDataTable romData)
        {
            RomData = romData;
        }

        public string RomNameConstructor(CuratorDataSet.ROMRow romItem)
        {
            return $"{romItem.Name} ({romItem.Extension.Trim('.').ToUpper()})";
        }

        public CuratorDataSet.ROMRow GetRom(string romName)
        {
            return RomData.Where(x => RomNameConstructor(x) == romName).First();
        }

        public void SetRomEnabledState(string romName, bool enabled)
        {
            GetRom(romName).Enabled = enabled;
        }

        public void GetRoms()
        {
            if (Form1.ActiveConsole == null)
                return;

            var RomFolders = Form1._romFolderController.GetRomFoldersForActiveConsole();

            foreach (var RomFolder in RomFolders)
            {
                var romList = Directory.GetFiles(RomFolder.Path);

                foreach (var rom in romList)
                {
                    var romName = Path.GetFileNameWithoutExtension(rom);
                    if (!RomData.Where(x => x.Name == romName).Any())
                    {
                        var romRow = RomData.NewROMRow();
                        romRow.Name = romName;
                        romRow.Extension = Path.GetExtension(rom);
                        romRow.RomFolder_Id = RomFolder.Id;
                        romRow.Enabled = true;

                        RomData.Rows.Add(romRow);
                    }
                }
            }
        }

        public IEnumerable<CuratorDataSet.ROMRow> GetRomsByRomFolderId(int romFolderId)
        {
            return RomData.Where(x => x.RomFolder_Id == romFolderId);
        }
    }
}
