using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonsOfDoom.Classes.Beings;
using DungeonsOfDoom.Classes.Interfaces;

namespace DungeonsOfDoom.Classes
{
    class Inventory
    {
        public Inventory(Player player)
        {
            Owner = player;
            Content = new List<IPickupAble>();
        }

        public Player Owner { get; set; }
        public List<IPickupAble> Content { get; set; } 


        public void Add(IPickupAble item)
        {
            item.PickUp(Owner);
            Content.Add(item);
            Game.GameEvents.AddEvent("You picked up a " + item.Name);
        }

        public void DisplayBackPack()
        {
            Console.Clear();
            Console.WriteLine("Backpack Content");
            Console.WriteLine("----------------");
            // Loops the content of BackPack and prints into console
            foreach (IPickupAble item in Content)
            {
                Console.WriteLine("- " + item.Name);
            }
            Console.ReadKey();
        }
    }
}
