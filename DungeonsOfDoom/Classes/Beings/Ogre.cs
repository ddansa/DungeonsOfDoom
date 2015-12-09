namespace DungeonsOfDoom.Classes.Beings
{
    class Ogre : Enemy
    {
        public Ogre() : base("Ogre", "O", 60, 20, 0.2)
        { 
        }

        public override void Attack(Being target)
        {
            if (Game._rnd.NextDouble() <= .5)
                base.Attack(target);
            else { 
                Game.AddEvent("The Ogre missed.. and hit himself");
                base.Attack(this);
            }
        }

    }
}
