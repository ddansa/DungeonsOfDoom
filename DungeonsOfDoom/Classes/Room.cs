using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Classes
{
    class Room
    {
        public Monster RoomMonster { get; set; }
        public Item RoomItem { get; set; }
        public string Tile { get; set; }
        public bool IsWall { get; set; }

        /*
        public WallType Wall { get; set; }

        public enum WallType
        {
            Horizontal,
            Vertical,
            TopLeftCorner,
            TopRightCorner,
            BotLeftCorner,
            BotRightCorner
        }*/
    }
}
