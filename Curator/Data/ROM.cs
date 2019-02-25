using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Data
{
    public class ROM
    {
        public int ID { get; private set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int ConsoleId { get; set; }
    }
}
