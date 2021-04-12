using System;
using System.Collections.Generic;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Interface;

namespace GameFramework.Entities.Objects.Abstracts
{
    public abstract class WorldObject : IWorldObject
    {

        public Position Position { get; set; }

        public bool Loot { get; set; }
        public string Name { get; set; }

        public string Symbol { get; set; }
        public bool Block { get; set; }

        public bool Reusable { get; set; }

        protected WorldObject(Position position, bool loot, string name, string symbol, bool block, bool reusable) 
        {
            Position = position;
            Loot = loot;
            Name = name;
            Symbol = symbol;
            Block = block;
            Reusable = reusable;
        }

        public abstract void Use(ref IPlayer creature, List<IWorldObject> objList, Action<IWorldObject> testAction);

        public void ClearItem(List<IWorldObject> objList)
        {
            if (Loot)
            {

                if (!Reusable)
                {
                    objList.Remove(this);
                }
                else
                {
                    //gives this new x.y coordinates
                    //testAction.Invoke(this);
                }
            }
        }
    }

}