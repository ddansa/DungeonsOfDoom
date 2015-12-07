using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonsOfDoom.Classes.Beings;
using DungeonsOfDoom.Classes.Items;

namespace DungeonsOfDoom.Classes
{
    class Room
    {
        public Enemy RoomMonster { get; set; }
        public Item RoomItem { get; set; }
        public string Tile { get; set; }
        public bool IsWall { get; set; }
    }
}
