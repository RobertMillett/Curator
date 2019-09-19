using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Curator.Data.Controllers
{
    public class RomFolderController
    {
        private CuratorDataSet.RomFolderDataTable RomFolderData;

        public RomFolderController(CuratorDataSet.RomFolderDataTable romFolderData)
        {
            RomFolderData = romFolderData;
        }

        public CuratorDataSet.RomFolderRow GetRomFolderById(int romFolder_Id)
        {
            return RomFolderData.Where(x => x.RowState != DataRowState.Deleted).Where(x => x.Id == romFolder_Id).First();
        }

        internal void AddToActiveConsole(string path)
        {
            var romFolder = RomFolderData.NewRomFolderRow();
            romFolder.Path = path;
            romFolder.Console_Id = Form1.ActiveConsole.Id;
            RomFolderData.Rows.Add(romFolder);
        }

        internal void AddToConsole(string path, CuratorDataSet.ConsoleRow console)
        {
            var romFolder = RomFolderData.NewRomFolderRow();
            romFolder.Path = path;
            romFolder.Console_Id = console.Id;
            RomFolderData.Rows.Add(romFolder);
        }

        public void Remove(string path)
        {
            RomFolderData.Rows.Remove(RomFolderData.Where(x => x.RowState != DataRowState.Deleted).Where(x => x.Path == path).First());
        }

        public void Remove(CuratorDataSet.RomFolderRow romfolder)
        {
            RomFolderData.Rows.Remove(romfolder);
        }

        public List<CuratorDataSet.RomFolderRow> GetRomFoldersForActiveConsole()
        {            
            if (Form1.ActiveConsole == null)
                return new List<CuratorDataSet.RomFolderRow>();

            return RomFolderData.Where(x => x.RowState != DataRowState.Deleted).Where(x => x.Console_Id == Form1.ActiveConsole.Id).ToList();
        }

        public List<CuratorDataSet.RomFolderRow> GetRomFoldersForConsole(CuratorDataSet.ConsoleRow console)
        {
            if (Form1.ActiveConsole == null)
                return new List<CuratorDataSet.RomFolderRow>();

            return RomFolderData.Where(x => x.RowState != DataRowState.Deleted).Where(x => x.Console_Id == console.Id).ToList();
        }

        public CuratorDataSet.RomFolderRow GetRomFolderByPath(string path)
        {
            return RomFolderData.Where(x => x.Path == path).First();
        }

        internal List<CuratorDataSet.RomFolderRow> ValidateFolders()
        {
            var missingFolders = new List<CuratorDataSet.RomFolderRow>();

            foreach(var folder in RomFolderData)
            {
                if (!Directory.Exists(folder.Path))
                {
                    missingFolders.Add(folder);
                }
            }

            return missingFolders;
        }
    }
}
