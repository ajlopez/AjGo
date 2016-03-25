using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo
{
    class Utilities
    {
        public static Color EnemyColor(Color color)
        {
            if (color == Color.Black)
                return Color.White;

            if (color == Color.White)
                return Color.Black;

            throw new InvalidOperationException();
        }
    }
}
