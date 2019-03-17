using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            return RomFolderData.Where(x => x.Id == romFolder_Id).First();
        }

        internal void Add(string path)
        {
            var romFolder = RomFolderData.NewRomFolderRow();
            romFolder.Path = path;
            romFolder.Console_Id = Form1.ActiveConsole.Id;
            RomFolderData.Rows.Add(romFolder);
        }

        public void Remove(string path)
        {
            RomFolderData.Rows.Remove(RomFolderData.Where(x => x.Path == path).First());
        }

        public List<CuratorDataSet.RomFolderRow> GetRomFoldersForActiveConsole()
        {
            if (Form1.ActiveConsole == null)
                return new List<CuratorDataSet.RomFolderRow>();

             return RomFolderData.Where(x => x.Console_Id == Form1.ActiveConsole.Id).ToList();
        }

        public CuratorDataSet.RomFolderRow GetRomFolderByPath(string path)
        {
            return RomFolderData.Where(x => x.Path == path).First();
        }
    }
}
