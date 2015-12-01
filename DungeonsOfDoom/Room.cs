using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Room
    {
        public bool Wall { get; set; }
        public Monster RoomMonster { get; set; }
        public Item RoomItem { get; set; }
    }
}
