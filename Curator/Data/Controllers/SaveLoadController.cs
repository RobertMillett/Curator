using System;
using System.IO;
using System.Collections.Generic;

namespace Curator.Data.Controllers
{
    public class SaveLoadController
    {
        public static string DataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Curator", "XmlDoc.xml");
        private CuratorDataSet CuratorData;

        public SaveLoadController(CuratorDataSet curatorDataSet)
        {
            CuratorData = curatorDataSet;
        }

        public void Save()
        {
            CuratorData.AcceptChanges();
            CuratorData.WriteXml(DataPath);
        }

        public void Load()
        {
            if (!File.Exists(DataPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(DataPath));
                CuratorData.WriteXml(DataPath);
            }

            CuratorData.ReadXml(DataPath);
            CuratorData.AcceptChanges();
        }

        public void SaveActiveConsole()
        {
            Form1.ActiveConsole?.AcceptChanges();
        }

        public void Exit()
        {
            try
            {
                CuratorData.RejectChanges();
            }
            catch
            {
                return;
            }
            
            CuratorData.WriteXml(DataPath);
        }

        public void SaveRomsForActiveConsole(List<CuratorDataSet.ROMRow> roms)
        {
            foreach(var rom in roms)
            {
                rom.AcceptChanges();
            }
        }
    }
}
