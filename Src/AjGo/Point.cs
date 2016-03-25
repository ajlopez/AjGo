using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo
{
    public class Point
    {
        public short X;
        public short Y;

        public Point(short x, short y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            Point p = obj as Point;

            if (p == null)
                return false;

            return (p.X == X && p.Y == Y);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() * 17;
        }
    }
}

