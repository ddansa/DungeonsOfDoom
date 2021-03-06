﻿namespace DungeonsOfDoom.Classes
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
