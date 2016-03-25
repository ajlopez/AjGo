using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo
{
    public enum Color
    {
        Empty = 0,
        Black = 1,
        White = 2,
        Border = 3,
        Blue = 4,
        Red = 5,
        Green = 6,
        Yellow = 7
    }

    public class ColorCount
    {
        public int[] Count = new int[8];

        public int Whites
        {
            get { return Count[(int)Color.White]; }
        }

        public int Blacks
        {
            get { return Count[(int)Color.Black]; }
        }
    }
}
