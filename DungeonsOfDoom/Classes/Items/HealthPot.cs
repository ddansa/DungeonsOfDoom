using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Classes
{
    class HealthPot : Item
    {
        public HealthPot(string name, string tile, int stat) : base(name, tile, stat)
        {
        }
        public override void PickUp(Player player)
        {
            player.Health += Stat;
        }
    }
}
