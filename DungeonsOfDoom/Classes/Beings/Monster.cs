namespace DungeonsOfDoom.Classes.Beings
{
    abstract class Monster : Being
    {
        protected Monster(string name, string tile, int health, int damage, double speed) : base(name, tile, health, damage, speed)
        {
        }
    }
}
