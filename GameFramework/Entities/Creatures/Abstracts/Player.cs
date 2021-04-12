using System.Diagnostics;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Interface;
using GameFramework.Enum;
using GameFramework.Tracer;

namespace GameFramework.Entities.Creatures.Abstracts
{
    public abstract class Player : Creature, IPlayer
    {
        public bool HasKey { get; set; }
        public IWeapon EquippedWeapon { get; set; }

        protected Player(Position position, string name, string symbol, int attackDamage, int defense, int hp) : base(position, name, symbol, attackDamage, defense, hp)
        {
        }

        public void Move(InputKey move)
        {
            switch (move)
            {
                case InputKey.FORWARD:
                    Position.Row--;
                    break;
                case InputKey.BACK:
                    Position.Row++;
                    break;
                case InputKey.RIGHT:
                    Position.Col++;
                    break;
                case InputKey.LEFT:
                    Position.Col--;
                    break;
            }
            //Undo comment if you want to trace movement, added to much spam in my opinion
            //TraceWorker.Write(TraceEventType.Information, 11, $"{this.GetType().Name} {this.Name} moved {move}");
        }

        public void Eat(int food)
        {
            TraceWorker.Write(TraceEventType.Information, 10, $"{this.GetType().Name} {this.Name} was killed");
            this.HP += food;
        }
    }
}
