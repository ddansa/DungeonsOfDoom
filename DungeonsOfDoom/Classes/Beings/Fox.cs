using DungeonsOfDoom.Classes.Interfaces;

namespace DungeonsOfDoom.Classes.Beings
{
    class Fox : Enemy, IPickupAble
    {
        public Fox() : base("Fox", "F", 40, 10, 0.5)
        {
    }

        public override void PickUp(Player player)
        {
            Name += " Skin";
            player.BackPack.Add(this);
            Game.AddEvent("You picked up a " + Name);
        }
    }
}
