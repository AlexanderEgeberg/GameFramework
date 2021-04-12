using GameFramework.Entities.Creatures.Abstracts;

namespace GameFramework.Entities.Creatures.ConcreteMonsters
{
    class Dragon : Monster
    {
        public Dragon(Position position, string name, string symbol, int attackDamage, int defense, int hp) : base(position, name, symbol, attackDamage, defense, hp)
        {
        }
    }
}
