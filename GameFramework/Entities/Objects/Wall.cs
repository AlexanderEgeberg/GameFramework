using System;
using System.Collections.Generic;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Abstracts;
using GameFramework.Entities.Objects.Interface;

namespace GameFramework.Entities.Objects
{
    public class Wall : WorldObject
    {
        public Wall(Position position, bool loot, string name, string symbol, bool block, bool reusable) : base(position, loot, name, symbol, block, reusable)
        {
        }

        public override void Use(ref IPlayer creature, List<IWorldObject> objList, Action<IWorldObject> testAction)
        {
            //Console.WriteLine("Secret wall");

        }
    }
}
