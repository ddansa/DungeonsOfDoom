using System;
using System.Collections.Generic;
using DungeonsOfDoom.Classes.Interfaces;

namespace DungeonsOfDoom.Classes.Beings
{
    class Player : Being
    {
        public Player(string name, int health, int damage, double speed, int posX, int posY, Random rng) : base(name, "P", health, damage, speed)
        {
            X = posX;
            Y = posY;
            Rng = rng;
            BackPack = new List<IPickupAble>();
        }

        public void Fight(Being target)
        {

            if (Speed > target.Speed)
                FightRound(this, target);

            else if (Speed < target.Speed)
                FightRound(target, this);
            else
            {
                if (Rng.NextDouble() <= 0.5)
                    FightRound(target, this);
                else
                    FightRound(this, target);
            }
            if (target.Health <= 0)
                Game.AddEvent(target.Name + " was slain");
        }

        private static void FightRound(Being first, Being second)
        {
            second.Health -= first.Damage;
            Game.AddEvent(first.Name + " Hit " + second.Name + " for " + first.Damage + " damage");

            if (second.Health <= 0)
                return;
                
            first.Health -= second.Damage;
            Game.AddEvent(second.Name + " Hit " + first.Name + " for " + second.Damage + " damage");
        }

        public Random Rng { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<IPickupAble> BackPack { get; set; }
    }
}
