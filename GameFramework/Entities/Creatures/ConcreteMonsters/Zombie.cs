using GameFramework.Entities.Creatures.Abstracts;

namespace GameFramework.Entities.Creatures.ConcreteMonsters
{
    public class Zombie : Monster
    {
        public Zombie(Position position, string name, string symbol, int attackDamage, int defense, int hp) : base(position, name, symbol, attackDamage, defense, hp)
        {
        }
    }
}
