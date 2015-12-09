using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonsOfDoom;
using DungeonsOfDoom.Classes.Beings;

namespace DungeonsOfDoom.Classes
{
    class BattleSystem
    {
        public BattleSystem()
        {
            Rng = Game._rnd;
        }


        public void Start(Being fighter1, Being fighter2)
        {
            Fighter1 = fighter1;
            Fighter2 = fighter2;

            FightRound();
        }

        private void FightRound()
        {
            Being first = (SpeedCheck())
                ? Fighter1
                : Fighter2;

            Being second = (first != Fighter1)
                ? Fighter1
                : Fighter2;

            first.Attack(second);
            if (second.Health <= 0)
                return;
            second.Attack(first);
        }

        private bool SpeedCheck()
        {
            if (Fighter1.Speed > Fighter2.Speed)
            {
                return true;
            }
            else if (Fighter2.Speed > Fighter1.Speed)
                return false;
            else if (Rng.NextDouble() <= 0.5)
                return true;
            return false;
        }
        private Being Fighter1 { get; set; }
        private Being Fighter2 { get; set; }
        private Random Rng { get; set; }
    }
}
