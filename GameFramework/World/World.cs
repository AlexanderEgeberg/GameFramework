using System;
using System.Collections.Generic;
using System.Text;
using GameFramework.Entities;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Entities.Objects.Interface;

namespace GameFramework.World
{
    public class World : IWorld
    {
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public IPlayer Player { get; set; }
        private List<IMonster> _creatures;
        private List<IWorldObject> _objects;
        private string _horizontalLine;

        //constructor creates World
        public World(int width, int height, List<IMonster> creatures, IPlayer player, List<IWorldObject> objects)
        {
            MaxWidth = width;
            MaxHeight = height;
            _creatures = creatures;
            Player = player;
            _objects = objects;

            //creates HL with length of World width
            for (int i = 0; i < width + 2; i++)
            {
                _horizontalLine += "-";
            }

        }

        //draw the gameboard
        public void PrintPlayground(StringBuilder sb)
        {
            Console.SetCursorPosition(0, 0);
            sb.Clear();
            //draw top line
            sb.Append(_horizontalLine);
            sb.AppendLine();

            //for each row uptill maxHeight start drawing
            for (int r = 0; r < MaxHeight; r++)
            {
                sb.Append("|");

                //draw the row
                PrintRowString(r, sb);

                sb.Append($"|");
                sb.AppendLine();

            }
            //draw bottom line
            sb.Append(_horizontalLine);
        }

        //Draw row function
        public void PrintRowString(int r, StringBuilder sb)
        {
            //for each Row print the column entity
            for (int c = 0; c < MaxWidth; c++)
            {
                //draws entity on row + column position
                PrintColRowChar(r,c, sb);
            }
        }

        public void PrintColRowChar(int row, int col, StringBuilder sb)
        {
            //gets current position
            Position p = new Position(row, col);

            var creature = _creatures.Find(x => x.Position.Equals(p));
            var objects = _objects.Find(x => x.Position.Equals(p));


            //TODO find a way to draw objects that doen't use if statements make it extendable - look at controls do same thing
            if (Player.Position.Equals(p))
            {
                sb.Append(Player.Symbol);
            }
            else if (objects != null)
            {
                sb.Append(objects.Symbol);
            }
            else if (creature != null)
            {
                sb.Append(creature.Symbol);
            }
            else
            {
                sb.Append("*");
            }
;
        }
    }
}