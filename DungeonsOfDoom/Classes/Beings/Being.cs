namespace DungeonsOfDoom.Classes.Beings
{
    abstract class Being : GameObject
    {
        protected Being(string name, string tile, int health, int damage, double speed) : base(name, tile)
        {
            Health = health;
            Damage = damage;
            Speed = speed;
        }

        public virtual void Attack(Being target)
        {
            target.Health -= Damage;
            Game.AddEvent(Name + " Hit " + target.Name + " for " + Damage + " damage");
            if (target.Health <= 0)
                Game.AddEvent(target.Name + " was killed");
        }

        public int Health { get; set; }
        public int Damage { get; set; }
        public double Speed { get; set; }
    }
}
