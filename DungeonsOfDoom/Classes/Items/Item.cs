using DungeonsOfDoom.Classes.Beings;

namespace DungeonsOfDoom.Classes.Items
{
    abstract class Item : GameObject
    {
        protected Item(string name, string tile, int stat) : base(name, tile)
        {
            Stat = stat;
        }

        public virtual void PickUp(Player player) { }

        public int Stat { get; set; }
    }
}
