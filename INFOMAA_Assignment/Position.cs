using System;
namespace INFOMAA_Assignment
{
    public struct Position : IEquatable<Position>
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }


        public override bool Equals(Object obj)
        {
            return obj is Position && this == (Position)obj;
        }

        public bool Equals(Position other)
        {
            return X == other.X && this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"[Position: X={X}, Y={Y}]";
        }
    }
}