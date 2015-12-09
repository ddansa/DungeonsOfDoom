using DungeonsOfDoom.Classes.Beings;
using DungeonsOfDoom.Classes.Interfaces;

namespace DungeonsOfDoom.Classes.Items
{
    abstract class Item : GameObject, IPickupAble
    {
        protected Item(string name, string tile, int stat) : base(name, tile)
        {
            Stat = stat;
        }

        public virtual void PickUp(Player player)
        {
            player.BackPack.Add(this);
        }

        public int Stat { get; set; }
    }
}
