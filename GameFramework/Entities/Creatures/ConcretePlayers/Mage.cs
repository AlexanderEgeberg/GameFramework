using GameFramework.Entities.Creatures.Abstracts;

namespace GameFramework.Entities.Creatures.ConcretePlayers
{
    public class Mage : Player
    {
        public Mage(Position position, string name, string symbol, int attackDamage, int defense, int hp) : base(position, name, symbol, attackDamage, defense, hp)
        {
        }
    }
}