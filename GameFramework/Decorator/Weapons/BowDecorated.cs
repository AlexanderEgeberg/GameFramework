using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Interface;

namespace GameFramework.Decorator.Weapons
{
    class BowDecorated : PlayerDecorator
    {
        public override IWeapon EquippedWeapon { get; set; }

        public BowDecorated(IPlayer aPlayer, IWeapon equippedWeapon) : base(aPlayer)
        {
            EquippedWeapon = equippedWeapon;
        }
        public override int Hit(ICreature enemy)
        {
            return base.Hit(enemy) + EquippedWeapon.DamageModifier;
        }
        public override int Hit()
        {
            return base.Hit() + EquippedWeapon.DamageModifier;
        }

    }
}
