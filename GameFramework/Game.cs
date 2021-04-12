using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using GameFramework.Controls;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Interface;
using GameFramework.Enum;
using GameFramework.Observer;
using GameFramework.Tracer;
using GameFramework.World;

namespace GameFramework
{
    public class Game
    {
        private IWorld _game_world;
        private IControls _controls;
        private bool _gameRunning;
        private int _waitTime;

        private IPlayer _player;
        private static Random rnd = new Random();

        private List<IMonster> _creatures;
        private List<IWorldObject> _objects;
        private StringBuilder gameGraphics = new StringBuilder();
        private StringBuilder gameConsole = new StringBuilder();


        //game constructor with World size
        public Game(IWorld world, List<IMonster> creatures, IPlayer player, List<IWorldObject> objects, IControls controls, IObserver deathObserver)
        {
            _player = player;
            _game_world = world;
            _creatures = creatures;
            _objects = objects;
            _gameRunning = true;
            _controls = controls;
            AttachObservers(deathObserver);
        }
        //starts the game
        public void Start()
        {
            Console.CursorVisible = false;
            TraceWorker.Write(TraceEventType.Start,1,"Game initial drawing");
            //while gameRunning is true the game will continue to run
            while (_gameRunning)
            {

                //clear graphics and make world string
                //Wanted to get more experience with stringbuilder
                //If I didn't I could have colored the world
                //And printed more dynamically, oh well
                gameGraphics.Clear();
                _game_world.PrintPlayground(gameGraphics);


                //If there is a player append player info
                if (_player != null)
                {
                    gameGraphics.Append(_player.EquippedWeapon != null
                        ? $"\n{_player.Name}  HP: {_player.HP} max Damage: {_player.Hit()} Equipped: {_player.EquippedWeapon.Name}"
                        : $"\n{_player.Name}  HP: {_player.HP} max Damage: {_player.Hit()} Equipped: None");
                    gameGraphics.AppendLine();
                }


                //draw game UI
                gameGraphics.Append("While on top of items press e to acquire it");
                gameGraphics.AppendLine();
                gameGraphics.Append("Type next movement 'a,w,s,d,e' : ");
                gameGraphics.AppendLine();
                //Event based on user move, if the player movement stops the game it will set to false.
                Console.WriteLine(gameGraphics);
                Console.WriteLine(gameConsole);
                if (gameConsole.Length > 0)
                {
                    gameConsole.Clear();
                    //Console.WriteLine("Press enter to continue...");
                    //Console.ReadLine();
                    Thread.Sleep(500 * _waitTime);
                    _waitTime = 0;
                    Console.Clear();
                    Console.WriteLine(gameGraphics);
                }
                Console.SetCursorPosition(0,0);
                _gameRunning = GameAction(_controls.ReadNextKey());

                //Console.WriteLine($"{_player.Position.Col} && {_player.Position.Row}");
                //Console.WriteLine(_player);

                //if the player died end the game
                if (!_player.IsAlive())
                {
                    break;
                }
            }

            Console.CursorVisible = true;
            Console.Clear();
            Console.WriteLine(gameGraphics);
            Console.WriteLine(_player.HasKey ? "\nYou obtained the key and won!" : "\nYou died and didn't obtain the key so you lost :(");
            TraceWorker.Write(TraceEventType.Stop,3, "Game has ended");
        }
        private bool GameAction(InputKey move)
        {
            //Player moves based on input direction
            _player?.Move(move);
            //  is the object that the player is on, if none returns null
            var obj = _objects.Find(x => x.Position.Equals(_player.Position));
            var creature = _creatures.Find(x => x.Position.Equals(_player.Position));
            //checks if player collides with a non passable World object or World walls
            CheckCollision(obj, move);
            //Check if the player is on a WorldObject
            CheckOnItem(obj, move);
            //Check if player is on a creature
            CheckOnCreature(creature);
            //Check if player won the game
            return CheckWin();
        }


        //Generate random position property
        private void GetRandomPosition(IWorldObject objWorldObject)
        {
            objWorldObject.Position.Row = rnd.Next(_game_world.MaxWidth);
            objWorldObject.Position.Col = rnd.Next(_game_world.MaxHeight);
        }

        //Collision
        private void CheckCollision(IWorldObject obj, InputKey move)
        {
            if (obj != null && obj.Block || _player.Position.Col == -1 || _player.Position.Col == _game_world.MaxHeight || _player.Position.Row == -1 || _player.Position.Row == _game_world.MaxWidth)
            {
                //redo move
                switch (move)
                {
                    case InputKey.FORWARD:
                        _player.Move(InputKey.BACK);
                        break;
                    case InputKey.BACK:
                        _player.Move(InputKey.FORWARD);
                        break;
                    case InputKey.LEFT:
                        _player.Move(InputKey.RIGHT);
                        break;
                    case InputKey.RIGHT:
                        _player.Move(InputKey.LEFT);
                        break;
                }
            }

        }

        private void CheckOnItem(IWorldObject obj, InputKey move)
        {

            //Checks if player is on an object
            if (obj != null && obj.Position.Equals(_player.Position))
            {
                //add to consoleLog
                gameConsole.AppendLine($"{_player.Name} walked on {obj.Name}");
                _waitTime++;

                //if the player does "USE"
                if (move == InputKey.USE)
                {
                    //not fan of using if obj is type to check if obj is weapon to use AscendPlayer
                    //Because what if I have objects that aren't weapons or regular worldObjects 
                    //I'd have to do add an extra if check each time.
                    //Ideally it should always do obj.use and act differently depending on type.
                    //if (obj is IWeapon weapon)
                    //{

                        //Decorate player - look into undo a decorating.
                        //If player already is decorated then remove decorating? And apply new decorating TBD

                        //Have to use a new function besides .Use since I can't return the new IPlayer using obj.Use()
                        //weapon.AscendPlayer(ref _player);
                        //_game_world.Player = _player;
                    //}
                    //Fixed

                    //use the object /Decorate object / Eat food
                    obj.Use(ref _player, _objects, GetRandomPosition);
                    gameConsole.AppendLine($"{_player.Name} used {obj.Name}");

                    //consoleLog waittime based on log size
                    _waitTime++;

                }

            }
        }
        private void CheckOnCreature(IMonster creature)
        {
            if (creature != null && creature.Position.Equals(_player.Position))
            {
                //if true the player won the fight
                if (Fight(_player, creature))
                {
                    //TODO make creature drop items??
                    //creature.OnDeath => Add WorldObject to list of world objects
                    creature.OnDeath(_objects);
                    _creatures.Remove(creature);
                }

            }
        }
        private bool CheckWin()
        {
            if (_player.HasKey)
            {
                //wins game
                return false;
            }

            //continue game
            return true;
        }
        //Takes the user input and creates and action based on the move
        private bool Fight(IPlayer player, IMonster enemy)
        {
            do
            {
                if (player.IsAlive())
                {
                    var damage = player.Hit(enemy);
                    enemy.ReceiveHit(damage);
                    gameConsole.AppendLine($"{player.Name} attacks {enemy.Name} dealing {damage}");
                    _waitTime++;

                    gameConsole.AppendLine($"{enemy.Name} has {enemy.HP} HP left");
                    _waitTime++;
                }

                if (enemy.IsAlive())
                {
                    var damage = enemy.Hit(player);
                    player.ReceiveHit(damage);
                    gameConsole.AppendLine($"{enemy.Name} attacks {player.Name} dealing {damage}");
                    _waitTime++;
                    gameConsole.AppendLine($"{player.Name} has {player.HP} HP left");
                    _waitTime++;
                }
            } while (player.IsAlive() && enemy.IsAlive());

            if (player.IsAlive())
            {
                return true;
            }
            if (enemy.IsAlive())
            {
                return false;
            }
            return true;
        }
        private void AttachObservers(IObserver observer)
        {
            foreach (var creature in _creatures)
            {
                creature.Attach(observer);
            }
            _player.Attach(observer);
        }
    }
}
