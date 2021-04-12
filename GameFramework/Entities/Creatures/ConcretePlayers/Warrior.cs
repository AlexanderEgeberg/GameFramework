using GameFramework.Entities.Creatures.Abstracts;

namespace GameFramework.Entities.Creatures.ConcretePlayers
{
    public class Warrior : Player
    {
        public Warrior(Position position, string name, string symbol, int attackDamage, int defense, int hp) : base(position, name, symbol, attackDamage, defense, hp)
        {
        }
    }
}