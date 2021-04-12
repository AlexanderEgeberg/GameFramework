using GameFramework.Entities;
using GameFramework.Entities.Creatures.ConcretePlayers;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Enum;

namespace GameFramework.Factory
{
    public static class PlayerFactory
    {
        public static IPlayer GetPlayer(PlayerType type, Position position, int hp, string name, string symbol, int attackDamage, int defense)
        {

            switch (type)
            {
                case PlayerType.Warrior: return new Warrior(position, name, symbol, attackDamage, defense, hp);
                case PlayerType.Mage: return new Mage(position, name, symbol, attackDamage, defense, hp);
                case PlayerType.Ranger: return new Ranger(position, name, symbol, attackDamage, defense, hp);


            }

            return null;
        }
    }
}