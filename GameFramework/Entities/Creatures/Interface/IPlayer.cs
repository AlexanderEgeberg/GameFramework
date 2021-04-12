using GameFramework.Entities.Objects.Interface;
using GameFramework.Enum;

namespace GameFramework.Entities.Creatures.Interface
{
    public interface IPlayer : ICreature
    {
        public bool HasKey { get; set; }
        public IWeapon EquippedWeapon { get; set; }
        void Move(InputKey move);
        void Eat(int food);
    }
}