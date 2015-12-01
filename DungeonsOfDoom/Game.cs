using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Game
    {
        const int mapWidth = 20;
        const int mapHeight = 10;
        Room[,] rooms = new Room[mapWidth, mapHeight];
        Player player = new Player();

        public void Start()
        {
            CreateMap();


            do
            {
                Console.Clear();
                DisplayPlayerInfo();
                DrawMap();
                WaitForCommand();
            } while (player.Health > 0);


            Console.WriteLine("RIP");
            Console.ReadKey();
        }

        private void CreateMap()
        {
            Random rnd = new Random();
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Room room = new Room();
                    rooms[x, y] = room;

                    int r = rnd.Next(7);
                    if(r == 1 && !(player.X == x && player.Y == y))
                    {
                        room.RoomMonster = new Monster("Mon", 30, 20);
                    }
                }
            }
        }

        private void DisplayPlayerInfo()
        {
            Console.WriteLine("Health: " + player.Health);
        }

        private void DrawMap()
        {
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Room room = rooms[x, y];

                    if (player.X == x && player.Y == y)
                        Console.Write("P");
                    else if (room.RoomMonster != null)
                        Console.Write("M");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        private void WaitForCommand()
        {
            Console.WriteLine("Enter movement");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            int x = player.X;
            int y = player.Y;
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    x--;
                    break;
                case ConsoleKey.RightArrow:
                    x++;
                    break;
                case ConsoleKey.DownArrow:
                    y++;
                    break;
                case ConsoleKey.UpArrow:
                    y--;
                    break;
                //default: break;
            }
            if (rooms[x, y].RoomMonster != null)
                player.Health -= 5;

            player.X = x;
            player.Y = y;
        }
    }
}
