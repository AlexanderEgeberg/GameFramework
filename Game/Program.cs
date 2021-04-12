using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using GameFramework;
using GameFramework.Config;
using GameFramework.Controls;
using GameFramework.Decorator;
using GameFramework.Entities;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Interface;
using GameFramework.Enum;
using GameFramework.Factory;
using GameFramework.Observer;
using GameFramework.Tracer;
using GameFramework.World;

namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create deathobserver
            DeathObserver deathObserver = new DeathObserver();
            //parameters game size, read from XML file - unused switched to array dimensions
            List<int> configDataInt = XMLConfig.ReadConfiguration();
            //list of monster entities
            List<IMonster> monsters = new List<IMonster>();
            //list of objects in the world
            List<IWorldObject> objects = new List<IWorldObject>();
            //your class choice
            PlayerType playerType;
            IPlayer player = null;



            //SOLID - O: open for extension, but closed for modification
            //To add more  control keys you create a new Key class and add it to IKey dictionary
            //Extension through keys list
            //Assign the key for Left,Right,Forward,Back,Use
            //If wanna add more keys to the game, create another IKey class
            //If you wanna reassign a key change the button property
            //Could be loaded from config file
            Dictionary<InputKey, IKey> leftKeyboard = new Dictionary<InputKey, IKey>()
            {
                {InputKey.LEFT,new LeftKey('a')},
                {InputKey.RIGHT,new RightKey('d')},
                {InputKey.FORWARD,new ForwardKey('w')},
                {InputKey.BACK,new BackKey('s')},
                {InputKey.USE,new UseKey('q')},
            };


            //Create controls
            IControls leftControls = new Controls(leftKeyboard);

            //Reassign a button
            leftControls.Keys[InputKey.USE].Button = 'e';
            

            //test controls
            //while (true)
            //{
            //    Console.WriteLine(rightControls.ReadNextKey());
            //}


            //TODO could make more grids to have more worlds
            //game world, size depending on grid dimensions
            string[,] grid =
            {
                {"*","*","*","*","#","H","#","*","*","*","*"},
                {"*","*","*","*","#","*","#","*","*","*","*"},
                {"#","#","#","#","#","*","#","*","*","*","*"},
                {"K","*","D","*","*","*","#","*","*","*","*"},
                {"#","#","#","#","#","*","#","#","#","#","#"},
                {"*","*","*","*","#","*","#","*","*","*","S"},
                {"*","*","*","*","#","*","Z1","*","*","*","B"},
                {"*","*","*","*","#","*","#","*","*","*","W"},
                {"#","#","#","#","#","*","#","#","#","#","#"},
                {"A","*","*","*","F","*","#","*","*","*","*"},
                {"#","#","#","#","#","Z","#","*","*","*","*"},

            };

            //start the game
            StartGame();
            void StartGame()
           {
               //get class choice
               while (true)
               {
                   Console.WriteLine("Choose your Class, 1. Ranger 2. Mage. 3. Warrior");
                   var choice = Console.ReadLine();
                   if (choice == "1" ||choice != null && choice.ToLower() == "ranger")
                   {
                       playerType = PlayerType.Ranger;
                       break;
                   }
                   if (choice == "2" || choice.ToLower() == "mage")
                   {
                       playerType = PlayerType.Mage;
                       break;

                   }
                   if (choice == "3" || choice.ToLower() == "warrior")
                   {
                       playerType = PlayerType.Warrior;
                       break;
                   }
               }

               #region game info
                Console.Clear();
                Console.WriteLine("Game information:");
                Console.WriteLine("H is you");
                Console.WriteLine("# is walls, some may be passable");
                Console.WriteLine("K is a key, retrieve this and you win");
                Console.WriteLine("D is a dragon be careful it's strong");
                Console.WriteLine("A is food");
                Console.WriteLine("Z is a zombie");
                Console.WriteLine("B,S,W are weapons Bow,Sword,Wand respectively");
                Console.WriteLine("Press enter when you'd like to start the game");
                Console.ReadLine();
                Console.Clear();

                #endregion

               objects.Clear();
               monsters.Clear();
               int rows = grid.GetLength(0);
               int cols = grid.GetLength(1);


               //Create entities with positions
               for (int y = 0; y < rows; y++)
               {
                   for (int x = 0; x < cols; x++)
                   {
                       string element = grid[y, x];
                       switch (element)
                       {
                           case "H":
                               //TODO add more customizability from user
                               player = PlayerFactory.GetPlayer(playerType,new Position(y,x),1,"Alex",element,5,1);
                               break;
                           case "Z":

                               //Create a zombie
                               monsters.Add(CreatureFactory.GetCreature(CreatureType.Dragon, new Position(y, x), 10, element, 50, 0));
                               break;
                           case "Z1":

                               //Create a zombie with loot
                               var zombie = CreatureFactory.GetCreature(CreatureType.Zombie, new Position(y, x), 10, "Z", 2, 0);
                               List<IWorldObject> loot = new List<IWorldObject>()
                               {
                                   WorldObjectFactory.GetWorldObject(WorldObjectType.Food,zombie.Position,true,"Apple",false,1,false,"A"),
                                   WorldObjectFactory.GetWorldObject(WorldObjectType.Food,zombie.Position,true,"Apple",false,1,false,"A")
                               };

                               zombie = new LootDecorated(zombie, loot);
                               monsters.Add(zombie);

                               break;
                           case "D":
                               monsters.Add(CreatureFactory.GetCreature(CreatureType.Dragon, new Position(y, x), 10, "Dragon", element, 50, 0));
                               break;
                           case "#":
                               objects.Add(WorldObjectFactory.GetWorldObject(WorldObjectType.Food, new Position(y, x), false, "Wall", true, 0, true, element));
                               break;
                           case "F":
                               objects.Add(WorldObjectFactory.GetWorldObject(WorldObjectType.Wall, new Position(y, x), false, "Fake wall", true, 0, false, "#"));
                               break;
                           case "A":
                               objects.Add(WorldObjectFactory.GetWorldObject(WorldObjectType.Food, new Position(y, x), true, "Apple", false, 15, false, element));
                               break;
                           case "W":
                               objects.Add(WorldObjectFactory.GetWorldObject(WorldObjectType.Wand, new Position(y, x), true, "Basic Wand", false, 10, false, element));
                               break;
                           case "B":
                               objects.Add(WorldObjectFactory.GetWorldObject(WorldObjectType.Bow, new Position(y, x), true, "Wooden Bow", false, 3, false, element));
                               break;
                           case "S":
                               objects.Add(WorldObjectFactory.GetWorldObject(WorldObjectType.Sword, new Position(y, x), true, "Wooden Sword", false, 5, false, element));
                               break;
                           case "K":
                               objects.Add(WorldObjectFactory.GetWorldObject(WorldObjectType.Key, new Position(y, x), true, "Key", false, 0, false, element));
                               break;
                       }
                   }
               }

               //create the visual world with dimensions, creatures,player, and objects
               World world = new World(grid.GetLength(0), grid.GetLength(1), monsters, player, objects);

               // create a game with the world, creatures, palyer, objects, controls, observers and keys
               Game game = new Game(world, monsters, player, objects, leftControls, deathObserver);
               //game.Start();

               Start();



               Console.WriteLine("Would you like to try again Y/N");
               var input = Console.ReadLine();
               if (input.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                { 
                    Console.Clear();
                    StartGame();
                }
           }
            
            void Start()
            {
                World world = new World(grid.GetLength(0), grid.GetLength(1), monsters, player, objects);
                Game game = new Game(world, monsters, player, objects, leftControls, deathObserver);

               // game.Start();

                var _gameRunning = true;

                Console.CursorVisible = false;
                TraceWorker.Write(TraceEventType.Start, 1, "Game initial drawing");
                //while gameRunning is true the game will continue to run
                while (_gameRunning)
                {

                    //clear graphics and make world string
                    //Wanted to get more experience with stringbuilder
                    //If I didn't I could have colored the world
                    //And printed more dynamically, oh well
                    game.gameGraphics.Clear();
                    game._game_world.PrintPlayground(game.gameGraphics);


                    //If there is a player append player info
                    if (game._player != null)
                    {
                        game.gameGraphics.Append(game._player.EquippedWeapon != null
                            ? $"\n{game._player.Name}  HP: {game._player.HP} max Damage: {game._player.Hit()} Equipped: {game._player.EquippedWeapon.Name}"
                            : $"\n{game._player.Name}  HP: {game._player.HP} max Damage: {game._player.Hit()} Equipped: None");
                        game.gameGraphics.AppendLine();
                    }


                    //draw game UI
                    game.gameGraphics.Append("While on top of items press e to acquire it");
                    game.gameGraphics.AppendLine();
                    game.gameGraphics.Append("Type next movement 'a,w,s,d,e' : ");
                    game.gameGraphics.AppendLine();
                    //Event based on user move, if the player movement stops the game it will set to false.
                    Console.WriteLine(game.gameGraphics);
                    Console.WriteLine(game.gameConsole);
                    if (game.gameConsole.Length > 0)
                    {
                        game.gameConsole.Clear();
                        //Console.WriteLine("Press enter to continue...");
                        //Console.ReadLine();
                        Thread.Sleep(500 * game._waitTime);
                        game._waitTime = 0;
                        Console.Clear();
                        Console.WriteLine(game.gameGraphics);
                    }
                    Console.SetCursorPosition(0, 0);
                    _gameRunning = game.GameAction(game._controls.ReadNextKey());

                    //Console.WriteLine($"{_player.Position.Col} && {_player.Position.Row}");
                    //Console.WriteLine(_player);

                    //if the player died end the game
                    if (!game._player.IsAlive())
                    {
                        break;
                    }
                }

                Console.CursorVisible = true;
                Console.Clear();
                Console.WriteLine(game.gameGraphics);
                Console.WriteLine(game._player.HasKey ? "\nYou obtained the key and won!" : "\nYou died and didn't obtain the key so you lost :(");
                TraceWorker.Write(TraceEventType.Stop, 3, "Game has ended");
            }
        }
    }
}
