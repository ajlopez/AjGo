using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo
{
    public class Move
    {
        public Color Color;
        public Point Point;

        public Move(short x, short y, Color c)
        {
            Color = c;
            Point = new Point(x, y);
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            Move m = obj as Move;

            if (m == null)
                return false;

            return (m.Point == Point && m.Color == Color);
        }

        public override int GetHashCode()
        {
            return Point.GetHashCode() + Color.GetHashCode() * 31;
        }
    }
}

