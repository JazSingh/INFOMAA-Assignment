﻿using System;
namespace INFOMAA_Assignment
{
    public struct Position : IEquatable<Position>
    {
        private int x;
        private int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X { get { return x; } }

        public int Y { get { return y; } }


        public override bool Equals(Object obj)
        {
            return obj is Position && this == (Position)obj;
        }

        public bool Equals(Position other)
        {
            return this.x == other.x && this.y == other.y;
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
            return string.Format("[Position: X={0}, Y={1}]", X, Y);
        }
    }
}