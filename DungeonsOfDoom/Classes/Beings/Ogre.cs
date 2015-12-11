namespace DungeonsOfDoom.Classes.Beings
{
    class Ogre : Enemy
    {
        public Ogre() : base("Ogre", "O", 60, 20, 0.2)
        { 
        }

        public override void Attack(Being target)
        {
            if (Game.Rnd.NextDouble() <= .5)
                base.Attack(target);
            else {
                Game.GameEvents.AddEvent("The Ogre missed.. and hit himself");
                base.Attack(this);
            }
        }

    }
}
