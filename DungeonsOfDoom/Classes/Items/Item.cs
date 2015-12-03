using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Classes
{
    abstract class Item : GameObject
    {
        public Item(string name, string tile, int stat) : base(name, tile)
        {
            Stat = stat;
        }

        public virtual void PickUp(Player player) { }

        public int Stat { get; set; }
    }
}
