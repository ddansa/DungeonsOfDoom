namespace DungeonsOfDoom.Classes.Beings
{
    abstract class Enemy : Being
    {
        protected Enemy(string name, string tile, int health, int damage, double speed) : base(name, tile, health, damage, speed)
        {
        }

        public abstract void PickUp(Player player);
    }
}
