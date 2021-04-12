using System.Collections.Generic;
using GameFramework.Entities;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Interface;
using GameFramework.Observer;

namespace GameFramework.Decorator
{
    public abstract class MonsterDecorator : IMonster
    {
        private IMonster _monsterImplementation;

        protected MonsterDecorator(IMonster monster)
        {
            _monsterImplementation = monster;
        }
        public void Attach(IObserver observer)
        {
            _monsterImplementation.Attach(observer);
        }

        public void Detach(IObserver observer)
        {
            _monsterImplementation.Detach(observer);
        }

        public void Notify(ICreature creature)
        {
            _monsterImplementation.Notify(creature);
        }

        public int HP
        {
            get => _monsterImplementation.HP;
            set => _monsterImplementation.HP = value;
        }

        public string Name
        {
            get => _monsterImplementation.Name;
            set => _monsterImplementation.Name = value;
        }

        public string Symbol
        {
            get => _monsterImplementation.Symbol;
            set => _monsterImplementation.Symbol = value;
        }

        public int AttackDamage
        {
            get => _monsterImplementation.AttackDamage;
            set => _monsterImplementation.AttackDamage = value;
        }

        public int Defense
        {
            get => _monsterImplementation.Defense;
            set => _monsterImplementation.Defense = value;
        }

        public Position Position
        {
            get => _monsterImplementation.Position;
            set => _monsterImplementation.Position = value;
        }

        public int Hit(ICreature enemy)
        {
            return _monsterImplementation.Hit(enemy);
        }

        public void ReceiveHit(int damage)
        {
            _monsterImplementation.ReceiveHit(damage);
        }

        public bool IsAlive()
        {
            return _monsterImplementation.IsAlive();
        }

        public List<IWorldObject> LootDropList
        {
            get => _monsterImplementation.LootDropList;
            set => _monsterImplementation.LootDropList = value;
        }

        public void OnDeath(List<IWorldObject> objects)
        {
            _monsterImplementation.OnDeath(objects);
        }

        public int Hit()
        {
            return _monsterImplementation.Hit();
        }
    }
}