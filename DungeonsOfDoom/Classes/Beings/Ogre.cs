namespace DungeonsOfDoom.Classes.Beings
{
    class Ogre : Enemy
    {
        public Ogre() : base("Ogre", "O", 60, 20, 0.2)
        { 
        }

        public override void PickUp(Player player)
        {
        }
    }
}
