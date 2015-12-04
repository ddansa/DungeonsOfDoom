using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Classes
{
    class Player : Being
    {
        public Player(string name, int health, int damage, double speed, int posX, int posY, Random rng) : base(name, "P", health, damage, speed)
        {
            X = posX;
            Y = posY;
            Rng = rng;
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
        }

        private void FightRound(Being first, Being second)
        {
            second.Health -= first.Damage;
            if (second.Health <= 0)
                return;
            first.Health -= second.Damage;
        }

        public Random Rng { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
