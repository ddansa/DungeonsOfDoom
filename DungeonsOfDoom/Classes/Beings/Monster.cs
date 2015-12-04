using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Classes
{
    abstract class Monster : Being
    {
        public Monster(string name, string tile, int health, int damage, double speed) : base(name, tile, health, damage, speed)
        {
        }
    }
}
