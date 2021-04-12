using System.Diagnostics;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Creatures.Observer;
using GameFramework.Tracer;

namespace GameFramework.Entities.Creatures.Abstracts
{
    public abstract class Creature : CreatureObserver, ICreature
    {
        public  Position Position { get; set; }
        private int _hp;
        public int HP
        {
            get => _hp;
            set { _hp = value;
                if (_hp <= 0)
                {
                    Notify(this);
                }
            }
        }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int AttackDamage { get; set; }
        public int Defense { get; set; }

        protected Creature(Position position, string name, string symbol, int attackDamage, int defense, int hp)
        {
            Position = position;
            Name = name;
            Symbol = symbol;
            AttackDamage = attackDamage;
            Defense = defense;
            HP = hp;
        }

        public int Hit(ICreature enemy)
        {
            //add damage calculation here

            var damage = AttackDamage;

            TraceWorker.Write(TraceEventType.Information, 13, $"{this.GetType().Name} {this.Name} hit {enemy.GetType().Name} {enemy.Name} for {damage}");

            return damage;
        }
        public int Hit()
        {
            //for calculating damage

            var damage = AttackDamage;


            return damage;
        }

        public void ReceiveHit(int damage)
        {
            //Add resistance here
            this.HP -= damage - this.Defense;
            this.IsAlive();
        }

        public bool IsAlive()
        {
            return this.HP > 0;
        }
    }
}
