using System;
using System.Collections.Generic;
using DungeonsOfDoom.Classes.Interfaces;

namespace DungeonsOfDoom.Classes.Beings
{
    class Player : Being
    {
        public Player(string name, int health, int damage, double speed, int posX, int posY) : base(name, "P", health, damage, speed)
        {
            X = posX;
            Y = posY;
            BackPack = new Inventory(this);
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Inventory BackPack { get; set; }
    }
}
