using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Player
    {
        public Player()
        {
            Health = 100;
            Damage = 30;
            X = 1;
            Y = 1;
        }

        public int Health { get; set; }
        public int Damage { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
