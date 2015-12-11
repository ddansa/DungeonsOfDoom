using DungeonsOfDoom.Classes.Interfaces;

namespace DungeonsOfDoom.Classes.Beings
{
    class Fox : Enemy, IPickupAble
    {
        public Fox() : base("Fox", "F", 40, 10, 0.5)
        {
        }

        public void PickUp(Player player)
        {
            Name += " Skin";
        }
    }
}
