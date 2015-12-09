using DungeonsOfDoom.Classes.Beings;

namespace DungeonsOfDoom.Classes.Items
{
    class Weapon : Item
    {
        public Weapon(string name, string tile, int stat) : base(name, tile, stat)
        {
        }
        public override void PickUp(Player player)
        {
            player.Damage += Stat;
            player.BackPack.Add(this);
        }
    }
}
