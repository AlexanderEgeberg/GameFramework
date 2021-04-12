using System;
using System.Collections.Generic;
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



            //SOLID - O: open for extension, but closed for modification
            //To add more keys you create a new Key class and add it to IKey keys list
            //Extension through keys list
            //controls was quite a simple class and making it SOLID took way more time,
            //but it's much cleaner code if it had been a big class it would probably be easier for future expansion
            List<IKey> leftKeys = new List<IKey>()
            {
                new aKey(),
                new dKey(),
                new wKey(),
                new sKey(),
                new eKey()
            };
            List<IKey> rightKeys = new List<IKey>()
            {
                new iKey(),
                new jKey(),
                new kKey(),
                new lKey(),
                new uKey()
            };

            IPlayer player = null;

            //Create controls
            IControls leftControls = new Controls(leftKeys);
            IControls rightControls = new Controls(rightKeys);

            //test controls
            //while (true)
            //{
            //    Console.WriteLine(rightControls.ReadNextKey());
            //}


            //TODO could make more grids to have more worlds - too 
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
                                monsters.Add(CreatureFactory.GetCreature(CreatureType.Dragon, new Position(y, x), 10, "Zombie", element, 2, 0));
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
               Game puzzle = new Game(world, monsters, player, objects, leftControls,deathObserver);
               puzzle.Start();

               Console.WriteLine("Would you like to try again Y/N");
               var input = Console.ReadLine();
               if (input.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                { 
                    Console.Clear();
                    StartGame();
                }
           }
        }
    }
}
