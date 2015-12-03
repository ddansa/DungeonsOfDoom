using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Item
    {
        public Item(string name, string type, int stat)
        {
            Name = name;
            Type = type;
            Stat = stat;
        }

        public string Name { get; set; }

        // Implemented types = "HealthPot", "Weapon"
        public string Type { get; set; }
        public int Stat { get; set; }
    }
}
