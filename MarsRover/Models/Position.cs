using System;

namespace MarsRover.Models
{
    public class Position : Tuple<int, int>, IComparable<Position>
    {
        public int X { get { return Item1; } }
        public int Y { get { return Item2; } }

        public Position(int x, int y) : base(x, y)
        {
        }

        public int CompareTo(Position other)
        {
            if (X == other.X && Y == other.Y)
                return 0;

            return -1;
        }

        public Position GoEast { get { return new Position(X + 1, Y); } }

        public Position GoWest { get { return new Position(X - 1, Y); } }

        public Position GoNorth { get { return new Position(X, Y + 1); } }

        public Position GoSouth { get { return new Position(X, Y - 1); } }
    }
}
