using System;
using System.Collections.Generic;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Abstracts;
using GameFramework.Entities.Objects.Interface;

namespace GameFramework.Entities.Objects
{
    public class Food : WorldObject
    {
        public int Value { get; set; }

        public Food(Position position, bool loot, string name, string symbol, bool block, bool reusable, int value) : base(position, loot, name, symbol, block, reusable)
        {
            Value = value;
        }

        public override void Use(ref IPlayer creature, List<IWorldObject> objList, Action<IWorldObject> testAction)
        {

            creature.Eat(Value);
            ClearItem(objList);

        }
    }
}
