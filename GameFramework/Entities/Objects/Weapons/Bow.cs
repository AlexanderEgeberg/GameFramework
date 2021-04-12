using System;
using System.Collections.Generic;
using GameFramework.Decorator.Weapons;
using GameFramework.Entities.Creatures.ConcretePlayers;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Abstracts;
using GameFramework.Entities.Objects.Interface;

namespace GameFramework.Entities.Objects.Weapons
{
    public class Bow : Weapon
    {

        public Bow(Position position, bool loot, string name, string symbol, bool block, bool reusable, int damageModifier) : base(position, loot, name, symbol, block, reusable, damageModifier)
        {
        }

        public override void AscendPlayer(ref IPlayer player)
        {
        }

        public override void Use(ref IPlayer creature, List<IWorldObject> objList, Action<IWorldObject> testAction)
        {
            if (creature is Ranger)
            {
                creature = new BowDecorated(creature,this);
                ClearItem(objList);
            }
        }


    }
}