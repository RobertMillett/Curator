using System;
using System.IO;

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
        }
    }
}
