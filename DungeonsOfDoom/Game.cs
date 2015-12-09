using System;
using DungeonsOfDoom.Classes;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonsOfDoom.Classes.Beings;
using DungeonsOfDoom.Classes.Items;

namespace DungeonsOfDoom
{
    class Game
    {
        readonly char[] _walls = Properties.Resources.WallList.ToCharArray();
        readonly Random _rnd = new Random();
        readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static int[] _mapWidth;
        static int _mapHeight;
        static Room[,] _rooms;
        static Player _player;


        public Game()
        {
            _player = new Player("Player", 100, 30, 0.5, 1, 1, _rnd);
        }

        public string Menu()
        {
            int mapCount = Directory.GetFiles(_baseDirectory + "Maps", "*.txt").Length;
            string[] files = Directory.GetFiles(_baseDirectory + "Maps", "*.txt");
            string map = "";
            int menuPosition = 0;
            bool selected = false;

            do
            {
                Console.WriteLine("Select Map");
                Console.WriteLine("Up/Down, Enter to select");
                Console.WriteLine();
                for (int i = 0; i < mapCount; i++)
                {
                    string mapName = Path.GetFileNameWithoutExtension(files[i]);
                    if(menuPosition == i)
                        Console.WriteLine("■ Map - " + mapName);
                    else
                        Console.WriteLine("Map - " + mapName);
                }

               
                ConsoleKeyInfo input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (menuPosition > 0)
                            menuPosition--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (menuPosition < mapCount - 1)
                            menuPosition++;
                        break;
                    case ConsoleKey.Enter:
                        selected = true;
                        break;
                    default: break;
                }

                if (selected) {
                    map = files[menuPosition];
                    break;
                }

                Console.Clear();

            } while (true);


            Console.WriteLine(map);
            return map;
        }

        public void Start()
        {
            CreateMap(Menu());
            do
            {
                UpdateMap();
                WaitForCommand();
            } while (_player.Health > 0);

            Console.WriteLine("You died..");
            Console.WriteLine("Press any key to restart");
            Console.ReadKey();
            Console.Clear();
            Restart();
        }

        private void Restart()
        {
            _player = new Player("Player", 100, 30, 0.5, 1, 1, _rnd);
            Start();
        }

        private void UpdateMap()
        {
            Console.Clear();
            DrawMap();
            DisplayPlayerInfo();
        }

        private void CreateMap(string file)
        {

            string map = File.ReadAllText(file);
            string[] mapRows = map.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            _mapHeight = mapRows.Length;
            _mapWidth = new int[_mapHeight];

            int maxWidth = 0;

            foreach (string row in mapRows)
            {
                if (row.Length > maxWidth)
                    maxWidth = row.Length;
            }

            _rooms = new Room[maxWidth, _mapHeight];

            for (int y = 0; y < mapRows.Length; y++)
            {
                _mapWidth[y] = mapRows[y].Length;
                for (int x = 0; x < mapRows[y].Length; x++)
                {
                    Room room = new Room();
                    _rooms[x, y] = room;
                    char c = mapRows[y][x];

                    if (c == "P"[0]) { 
                        _player.X = x;
                        _player.Y = y;
                    }

                    if (_walls.Contains(c))
                    {
                        room.IsWall = true;
                        room.Tile = c.ToString();
                        continue;
                    }

                    room.Tile = "·";

                    if (_player.X != x && _player.Y != y)
                    {
                        // 15% chance to spawn a Monster
                        if (_rnd.Next(100) < 15)
                        {
                            double rng = _rnd.NextDouble();
                            if (rng <= 0.5)
                                room.RoomMonster = new Goblin();
                            else if (rng <= 0.9)
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
            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth[y]; x++)
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
