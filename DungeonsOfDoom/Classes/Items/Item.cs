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

        public abstract void PickUp(Player player);

        public int Stat { get; set; }
    }
}
