using System;
using DungeonsOfDoom.Classes;
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
        readonly Random _rnd = new Random();
        readonly Player _player;
        public Game()
        {
            _player = new Player("Player", 100, 30, 0.5, 1, 1, _rnd);
        }
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
                        room.IsWall = true;
                        if (y == 0 && x == 0) { 
                            room.Tile = "┌";
                        }
                        else if (y == 0 && x == MapWidth - 1) { 
                            room.Tile = "┐";
                        }
                        else if (y == MapHeight - 1 && x == 0) { 
                            room.Tile = "└";
                        }
                        else if (y == MapHeight - 1 && x == MapWidth - 1) { 
                            room.Tile = "┘";
                        }
                        else if (y == 0 || y == MapHeight - 1) { 
                            room.Tile = "─";
                        }
                        else if (x == 0 || x == MapWidth - 1) { 
                            room.Tile = "│";
                        }
                        // Skips the rest of the function
                        continue;
                    }
                    if (x == MapWidth / 2 && y > 0 && y < MapHeight && y != MapHeight / 2)
                    {
                        room.IsWall = true;
                        room.Tile = "│";
                        continue;
                    }

                    room.Tile = "·";

                    if (_player.X != x && _player.Y != y)
                    {
                        // 15% chance to spawn a Monster
                        if (_rnd.Next(100) < 15)
                        {
                            double rng = _rnd.NextDouble();
                            if(rng <= 0.5)
                                room.RoomMonster = new Goblin();
                            else if(rng <= 0.9)
                                room.RoomMonster = new Ogre();
                            else
                                room.RoomMonster = new Raider();
                        }
                        // 5% chance to spawn an Item
                        else if (_rnd.Next(100) < 5)
                        {
                            // 50% chance to spawn Health potion or Sword
                            if (_rnd.NextDouble() <= 0.5)
                                // Sword damage is random between 5 and 15
                                room.RoomItem = new Weapon("Sword", "W", _rnd.Next(5, 16));
                            else
                                room.RoomItem = new HealthPot("Healing Potion", "H", 25);
                        }
                    }
                }
            }
        }

        private void DisplayPlayerInfo()
        {
            Console.Write("Health: " + _player.Health);
            Console.Write(" Damage: " + _player.Damage);
            Console.WriteLine(" Speed: " + _player.Speed);
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
                        Console.Write(_player.Tile);
                    else if (room.RoomMonster != null)
                        Console.Write(room.RoomMonster.Tile);
                    else if (room.RoomItem != null)
                        Console.Write(room.RoomItem.Tile);
                    else
                        Console.Write(room.Tile);
                }
                Console.WriteLine();
            }
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
            if (targetRoom.IsWall)
                return;

            if (targetRoom.RoomMonster != null)
            {
                // basic combat function
                _player.Fight(targetRoom.RoomMonster);

                if (targetRoom.RoomMonster.Health <= 0)
                {
                    // removes the monster if it's "dead"
                    targetRoom.RoomMonster = null;
                }
            }

            if (targetRoom.RoomItem != null)
            {
                // Picks up the item and adds the stats
                targetRoom.RoomItem.PickUp(_player);
                // Removes the item after stats are added.
                // TO-DO 
                //  Backpack system
                targetRoom.RoomItem = null;
            }

            // Moves the player
            _player.X = x;
            _player.Y = y;
        }
    }
}
