using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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
            return RomData.Where(x => x.RowState != System.Data.DataRowState.Deleted).Where(x => RomNameConstructor(x) == romName).FirstOrDefault();
        }

        public List<CuratorDataSet.ROMRow> GetRomsForActiveConsole()
        {
            var romFolders = Form1._romFolderController.GetRomFoldersForActiveConsole();

            var roms = RomData.Where(x => x.RowState != System.Data.DataRowState.Deleted).Where(x => romFolders.Select(y => y.Id).Contains(x.RomFolder_Id));

            return roms.ToList();
        }

        public void DeleteAllRomsForRomFolder(int romFolderId)
        {
            var roms = GetRomsByRomFolderId(romFolderId);
            foreach (var rom in roms)
            {
                if (rom.RowState != System.Data.DataRowState.Deleted)
                    rom.Delete();
            }
        }

        public void SetRomEnabledState(CuratorDataSet.ROMRow rom, bool enabled)
        {
            rom.Enabled = enabled;
        }

        public void LoadRoms()
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
                    if (!RomData.Where(x => x.RowState != System.Data.DataRowState.Deleted).Where(x => x.FileName == rom).Any())
                    {
                        var romRow = RomData.NewROMRow();
                        romRow.Name = romName;
                        romRow.FileName = rom;
                        romRow.Extension = Path.GetExtension(rom);
                        romRow.RomFolder_Id = RomFolder.Id;
                        romRow.Enabled = true;

                        RomData.Rows.Add(romRow);
                    }
                }
            }
        }

        public void RenameRom(CuratorDataSet.ROMRow rom, string newName)
        {
            rom.Name = newName;
            RomData.Rows[RomData.Rows.IndexOf(rom)].AcceptChanges();
        }

        public void SetRomImage(CuratorDataSet.ROMRow rom, string gridPicturePath)
        {
            rom.GridPicture = gridPicturePath;
        }

        private CuratorDataSet.ROMRow GetRomById(int romId)
        {
            return RomData.Where(x => x.RowState != System.Data.DataRowState.Deleted).Where(x => x.Id == romId).First();
        }

        public IEnumerable<CuratorDataSet.ROMRow> GetRomsByRomFolderId(int romFolderId)
        {
            return RomData.Where(x => x.RowState != System.Data.DataRowState.Deleted).Where(x => x.RowState != System.Data.DataRowState.Deleted && x.RomFolder_Id == romFolderId);
        }

        public IEnumerable<CuratorDataSet.ROMRow> GetAllRoms()
        {
            return RomData.ToList();
        }

        public IEnumerable<CuratorDataSet.ROMRow> GetAllRomsWhere(Func<CuratorDataSet.ROMRow, bool> func)
        {
            return RomData.Where(x => x.RowState != System.Data.DataRowState.Deleted).Where(func);
        }
    }
}
