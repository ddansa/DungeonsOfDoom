using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Game
    {
        const int MapWidth = 22;
        const int MapHeight = 12;
        readonly Room[,] _rooms = new Room[MapWidth, MapHeight];
        readonly Player _player = new Player();
        readonly Random _rnd = new Random();


        public void Start()
        {
            CreateMap();
            do
            {
                UpdateMap();
                WaitForCommand();
            } while (_player.Health > 0);

            Console.WriteLine("You died..");
            Console.ReadKey();
        }

        private void UpdateMap()
        {
            Console.Clear();
            DrawMap();
            DisplayPlayerInfo();
        }

        private void CreateMap()
        {
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    Room room = new Room();
                    _rooms[x, y] = room;

                    // Checks if the current room is an "outer" cell and sets it to a wall
                    if (x == 0 || y == 0 || x == MapWidth - 1 || y == MapHeight - 1)
                    {
                        room.Wall = true;
                        // Skips the rest of the function
                        continue;
                    }

                    if (_player.X != x && _player.Y != y)
                    {
                        // 15% chance to spawn a Monster
                        if (_rnd.Next(100) < 15)
                        {
                            room.RoomMonster = new Monster("Mon", 35, 20);
                        }
                        // 5% chance to spawn an Item
                        else if (_rnd.Next(100) < 5)
                        {
                            // 50% chance to spawn Health potion or Sword
                            if (_rnd.Next(2) == 0)
                                // Sword damage is random between 5 and 15
                                room.RoomItem = new Item("Sword", "Weapon", _rnd.Next(5, 16));
                            else
                                room.RoomItem = new Item("Health Potion", "HealthPot", 25);
                        }
                    }
                }
            }
        }

        private void DisplayPlayerInfo()
        {
            Console.Write("Health: " + _player.Health);
            Console.Write(" Damage: " + _player.Damage);
            Console.WriteLine();
        }

        private void DrawMap()
        {
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    Room room = _rooms[x, y];

                    if (_player.X == x && _player.Y == y)
                        Console.Write("P");
                    else if (room.RoomMonster != null)
                        Console.Write("M");
                    else if (room.RoomItem != null)
                        Console.Write(room.RoomItem.Type[0]);
                    else
                        MakeCell(x, y);

                }
                Console.WriteLine();
            }
        }

        private static void MakeCell(int x, int y)
        {
            string st;

            // Draws a "wall" if the cell is on the outside
            if (y == 0 && x == 0)
                st = "┌";
            else if (y == MapHeight - 1 && x == 0)
                st = "└";
            else if (y == MapHeight - 1 && x == MapWidth - 1)
                st = "┘";
            else if (y == 0 && x == MapWidth - 1)
                st = "┐";
            else if (y == 0 || y == MapHeight - 1)
                st = "─";
            else if (x == 0 || x == MapWidth - 1)
                st = "│";
            else
                st = "·";

            Console.Write(st);
        }

        private void WaitForCommand()
        {
            Console.WriteLine("Enter movement");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            int x = _player.X;
            int y = _player.Y;

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
                // default: return exits the function if input is not an arrow key
                default: return;
            }

            Room targetRoom = _rooms[x, y];

            // exits function if the target room is a Wall (prevents movement etc.)
            if (targetRoom.Wall)
                return;

            if (targetRoom.RoomMonster != null)
            {
                // basic combat function
                Monster monster = targetRoom.RoomMonster;
                _player.Health -= monster.Damage;
                monster.Health -= _player.Damage;

                if (monster.Health <= 0)
                {
                    // removes the monster if it's "dead"
                    targetRoom.RoomMonster = null;
                }
            }

            if (targetRoom.RoomItem != null)
            {
                // Picks up the item and adds the stats
                switch (targetRoom.RoomItem.Type)
                {
                    case "Weapon":
                        _player.Damage += targetRoom.RoomItem.Stat;
                        break;
                    case "HealthPot":
                        _player.Health += targetRoom.RoomItem.Stat;
                        break;
                    default: break;
                }
                // Removes the item after stats are added.
                targetRoom.RoomItem = null;
            }

            // Moves the player
            _player.X = x;
            _player.Y = y;
        }
    }
}
