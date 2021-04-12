using System;
using System.Runtime.CompilerServices;
using GameFramework.Entities;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Interface;
using GameFramework.Enum;
using GameFramework.Observer;

namespace GameFramework.Decorator
{
    public abstract class PlayerDecorator : IPlayer
    {
        private IPlayer Player;

        protected PlayerDecorator(IPlayer aPlayer)
        {
            this.Player = aPlayer;
        }


        //Decorator skal bruge samme interface som tidligere object før det blev decorated.
        //IPlayer inheriter ICreature fordi player objekter skal bruge ICreature properties
        //Af den grund er decoratoren nød til at implementere alle Creature og Player properties & methoder
        //Selvom det nye decorated objekt kun omskriver Hit methoden.
        //Er der en bedre måde at gøre det her er på, eller er man bare nødt til at gøre det på den her måde?

        public bool HasKey { get => Player.HasKey; set => Player.HasKey = value; }
        public abstract IWeapon EquippedWeapon { get; set; }
        public int HP { get => Player.HP; set => Player.HP = value; }
        public string Name { get => Player.Name; set => Player.Name = value; }
        public string Symbol { get => Player.Symbol; set => Player.Symbol = value; }
        public int AttackDamage { get => Player.AttackDamage; set => Player.AttackDamage = value; }
        public int Defense { get => Player.Defense; set => Player.Defense = value; }
        public Position Position { get => Player.Position; set => Player.Position = value; }


        //Decorator made to change how the player hits
        public virtual int Hit(ICreature enemy)
        {
            return this.Player.Hit(enemy);
        }

        public void Attach(IObserver observer)
        {
            Player.Attach(observer);
        }

        public void Detach(IObserver observer)
        {
            Player.Detach(observer);
        }

        public void Eat(int food)
        {
            Player.Eat(food);
        }

        public bool IsAlive()
        {
            return Player.IsAlive();
        }

        public void Move(InputKey move)
        {
            Player.Move(move);
        }

        public void Notify(ICreature creature)
        {
            Player.Notify(creature);
        }

        public void ReceiveHit(int damage)
        {
            Player.ReceiveHit(damage);
        }

        public virtual int Hit()
        {
            return Player.Hit();
        }
    }
}