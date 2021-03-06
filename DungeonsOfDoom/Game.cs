﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DungeonsOfDoom.Classes;
using DungeonsOfDoom.Classes.Beings;
using DungeonsOfDoom.Classes.Interfaces;
using DungeonsOfDoom.Classes.Items;

namespace DungeonsOfDoom
{
    class Game
    {
        readonly char[] _walls = Properties.Resources.WallList.ToCharArray();
        readonly string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static List<GameEvent> EventsList = new List<GameEvent>();
        public static Random Rnd = new Random();
        public static EventLog GameEvents = new EventLog();
        static int[] _mapWidth;
        static int _mapHeight;
        static Room[,] _rooms;
        static Player _player;
        static BattleSystem _battle;

        public Game()
        {
            _player = new Player("Player", 100, 30, 0.5, 1, 1);
            _battle = new BattleSystem();
        }

        public string Menu()
        {
            int mapCount = Directory.GetFiles(_baseDirectory + "Maps", "*.txt").Length;
            string[] files = Directory.GetFiles(_baseDirectory + "Maps", "*.txt");
            string map;
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
                    if (menuPosition == i)
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

                if (selected)
                {
                    map = files[menuPosition];
                    break;
                }

                Console.Clear();

            } while (true);

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
            _player = new Player("Player", 100, 30, 0.5, 1, 1);
            EventsList.Clear();
            Start();
        }

        private void UpdateMap()
        {
            Console.Clear();
            Console.WriteLine("Press I/B to show Backpack, Press L to show full Log");
            Console.WriteLine("Use Arrow keys to move");
            DrawMap();
            DisplayPlayerInfo();
            GameEvents.DisplayEventLog(true, 5);
        }

        private void CreateMap(string file)
        {

            string map = File.ReadAllText(file);
            string[] mapRows = map.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            _mapHeight = mapRows.Length;
            _mapWidth = new int[_mapHeight];

            int maxWidth = mapRows[0].Length;

            maxWidth = mapRows.Select(row => row.Length).Concat(new[] { maxWidth }).Max();

            _rooms = new Room[maxWidth, _mapHeight];

            for (int y = 0; y < mapRows.Length; y++)
            {
                _mapWidth[y] = mapRows[y].Length;
                for (int x = 0; x < mapRows[y].Length; x++)
                {
                    Room room = new Room();
                    _rooms[x, y] = room;
                    char c = mapRows[y][x];

                    if (c == "P"[0])
                    {
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
                        if (Rnd.Next(100) < 15)
                        {
                            double rng = Rnd.NextDouble();
                            if (rng <= 0.5)
                                room.RoomMonster = new Goblin();
                            else if (rng <= 0.7)
                                room.RoomMonster = new Ogre();
                            else
                                room.RoomMonster = new Fox();
                        }
                        // 5% chance to spawn an Item
                        else if (Rnd.Next(100) < 5)
                        {
                            // 50% chance to spawn Health potion or Sword
                            if (Rnd.NextDouble() <= 0.5)
                                // Sword damage is random between 5 and 15
                                room.RoomItem = new Weapon("Sword", "W", Rnd.Next(5, 16));
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
            Console.WriteLine("Enter Command");
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
                case ConsoleKey.I:
                case ConsoleKey.B:
                    _player.BackPack.DisplayBackPack();
                    return;
                case ConsoleKey.L:
                    GameEvents.DisplayEventLog(pauseAfterPrint:true);
                    return;
                // default: return exits the function if input is not an arrow key
                default: return;
            }

            Room targetRoom = _rooms[x, y];

            // exits function if the target room is a Wall (prevents movement etc.)
            if (targetRoom.IsWall)
                return;

            if (targetRoom.RoomMonster != null)
            {
                // Initialize Battle round
                _battle.Start(_player, targetRoom.RoomMonster);

                if (targetRoom.RoomMonster.Health <= 0)
                {
                    // Checks if the monster implements the IPickupAble interface
                    if (targetRoom.RoomMonster is IPickupAble)
                    {
                        IPickupAble monster = (IPickupAble) targetRoom.RoomMonster;
                        // If the monster can be picked up, adds to backpack
                        _player.BackPack.Add(monster);
                    }
                    // removes the monster
                    targetRoom.RoomMonster = null;
                }
            }


            if (targetRoom.RoomItem != null)
            {
                _player.BackPack.Add(targetRoom.RoomItem);
                targetRoom.RoomItem = null;
            }

            // Moves the player
            _player.X = x;
            _player.Y = y;
        }
    }
}