using GameFramework.Entities.Creatures.Interface;

namespace GameFramework.Entities.Objects.Interface
{
    public interface IWeapon : IWorldObject
    {
        public int DamageModifier { get; set; }
        void AscendPlayer(ref IPlayer player);
       // IPlayer Use(IPlayer player);
    }
}