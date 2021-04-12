using System.Collections.Generic;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Interface;

namespace GameFramework.Decorator
{
    public class LootDecorated : MonsterDecorator
    {
        public LootDecorated(IMonster monster, List<IWorldObject> lootList) : base(monster)
        {
            if (lootList != null)
            {
                foreach (var loot in lootList)
                {
                    LootDropList.Add(loot);
                }
            }
        }
    }
}