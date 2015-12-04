using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Classes
{
    abstract class GameObject
    {
        protected GameObject(string name, string tile)
        {
            Name = name;
            Tile = tile;
        }

        public string Name { get; set; }
        public string Tile { get; set; }
    }
}
