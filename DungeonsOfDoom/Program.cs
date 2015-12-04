using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sets console encoding to allow "box drawing"
            // full character sheet: http://www.ascii.ca/cp850.htm
            Console.OutputEncoding = Encoding.GetEncoding(850);
            Game game = new Game();
            game.Start();           
        }
    }
}
