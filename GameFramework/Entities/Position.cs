﻿namespace GameFramework.Entities
{
    public class Position
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }
        //Check if two entities have same position
        public bool Equals(Position other)
        {
            return Row == other.Row && Col == other.Col;
        }
    }
}
