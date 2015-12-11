using DungeonsOfDoom.Classes.Beings;

namespace DungeonsOfDoom.Classes.Items
{
    class HealthPot : Item
    {
        public HealthPot(string name, string tile, int stat) : base(name, tile, stat)
        {
        }
        public override void PickUp(Player player)
        {
            player.Health += Stat;
        }
    }
}
