using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Data.SteamDb
{
    public class GamesResponse
    {
        public int TotalGames { get; set; }
        public List<string> Games { get; set; }
    }
}
