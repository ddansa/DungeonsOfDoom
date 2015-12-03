using System;
using DungeonsOfDoom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Classes
{
    abstract class Being : GameObject
    {
        public Being(string name, string tile, int health, int damage, double speed) : base(name, tile)
        {
            Health = health;
            Damage = damage;
            Speed = speed;
        }


        public int Health { get; set; }
        public int Damage { get; set; }
        public double Speed { get; set; }
    }
}
