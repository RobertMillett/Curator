using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Data.SteamDb
{
    public class GridUrlsResponse
    {
        public int TotalGrids { get; set; }
        public int TotalResults { get; set; }
        public List<GridUrls> Data { get; set; }

        public class GridUrls
        {
            public string Grid_url { get; set; }
        }
    }
}
