using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Interface;

namespace GameFramework.Entities.Objects.Abstracts
{
    public abstract class Weapon : WorldObject, IWeapon
    {
        

        //unused
        public int DamageModifier { get; set; }
        public abstract void AscendPlayer(ref IPlayer player);

        protected Weapon(Position position, bool loot, string name, string symbol, bool block, bool reusable, int damageModifier) : base(position, loot, name, symbol, block, reusable)
        {

            DamageModifier = damageModifier;
        }


        //Hoped I could change type of the Use function for EquippedWeapon classes 
        //public abstract IPlayer Use(IPlayer player);

    }
}