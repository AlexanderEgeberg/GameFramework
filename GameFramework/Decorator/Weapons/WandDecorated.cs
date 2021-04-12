using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Interface;

namespace GameFramework.Decorator.Weapons
{
    public class WandDecorated : PlayerDecorator
    {
        public override IWeapon EquippedWeapon { get; set; }
        public WandDecorated(IPlayer aPlayer, IWeapon equippedWeapon) : base(aPlayer)
        {
            EquippedWeapon = equippedWeapon;
        }

        public override int Hit(ICreature enemy)
        {

            //if (enemy is Dragon)
            //{
            //    //if enemy is a Zombie double the damage
            //    return base.Hit(enemy) * 2;
            //}
            return base.Hit(enemy) + EquippedWeapon.DamageModifier;
        }
        public override int Hit()
        {
            return base.Hit() + EquippedWeapon.DamageModifier;
        }
    }
}
