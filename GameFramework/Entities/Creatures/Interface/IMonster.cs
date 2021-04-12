using System.Collections.Generic;
using GameFramework.Entities.Objects.Interface;

namespace GameFramework.Entities.Creatures.Interface
{
    public interface IMonster : ICreature
    {
        public List<IWorldObject> LootDropList { get; set; }
        void OnDeath(List<IWorldObject> objects);
    }
}