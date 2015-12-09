using DungeonsOfDoom.Classes.Beings;

namespace DungeonsOfDoom.Classes.Interfaces
{
    interface IPickupAble
    {
        void PickUp(Player player);
        string Name { get; set; }
    }
}
