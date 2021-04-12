using System.Collections.Generic;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Interface;

namespace GameFramework.Entities.Creatures.Abstracts
{
    public abstract class Monster : Creature, IMonster
    {
        private List<IWorldObject> _lootDropList = new List<IWorldObject>();
        public List<IWorldObject> LootDropList
        {
            get => _lootDropList;
            set => _lootDropList.Add((IWorldObject) value);
        }

        //Splitting up Monsters & Player classes from each together
        //TODO add monster only functions
        protected Monster(Position position, string name, string symbol, int attackDamage, int defense, int hp) : base(position, name, symbol, attackDamage, defense, hp)
        {
        }

        public void OnDeath(List<IWorldObject> objects)
        {
            if (LootDropList != null)
            {
                foreach (var loot in LootDropList)
                {
                    loot.Position = this.Position;
                    objects.Add(loot);
                }

            }
        }
    }
}