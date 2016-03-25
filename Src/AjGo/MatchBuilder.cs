using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AjGo
{
    public class MatchBuilder
    {
        private Color [,] cells = new Color[12,12];
        private short width;
        private short height;
        private Match match;

        private void SetColor(short x, short y, Color c)
        {
            cells[x, y] = c;
        }

        public Match GetMatch()
        {
            return GetMatch(null);
        }

        public Match GetMatch(string name)
        {
            if (match != null)
                return match;

            match = new Match(width, height,name);

            for (short x = 0; x < width; x++)
                for (short y = 0; y < height; y++)
                    match.SetColor(x, y, cells[x, y]);

            return match;
        }

        public void MakeRow(short y, string rowdes)
        {
            short x = 0;

            foreach (char ch in rowdes)
                switch (ch)
                {
                    case 'X':
                        SetColor(x, y, Color.Black);
                        x++;
                        break;
                    case 'O':
                        SetColor(x, y, Color.White);
                        x++;
                        break;
                    case 'R':
                        SetColor(x, y, Color.Red);
                        x++;
                        break;
                    case 'Y':
                        SetColor(x, y, Color.Yellow);
                        x++;
                        break;
                    case 'B':
                        SetColor(x, y, Color.Blue);
                        x++;
                        break;
                    case 'G':
                        SetColor(x, y, Color.Green);
                        x++;
                        break;
                    case '-':
                        SetColor(x, y, Color.Border);
                        x++;
                        break;
                    case '|':
                        SetColor(x, y, Color.Border);
                        x++;
                        break;
                    case '+':
                        SetColor(x, y, Color.Border);
                        x++;
                        break;
                    case '.':
                        SetColor(x, y, Color.Empty);
                        x++;
                        break;
                }

            if (x > width)
                width = x;
            if (y + 1 > height)
                height = (short) (y + 1);
        }

        public void MakeMatch(TextReader description)
        {
            string line = description.ReadLine();
            short nrow = 0;

            while (line != null)
            {
                MakeRow(nrow, line);
                nrow++;
                line = description.ReadLine();
            }
        }

        public void MakeMatch(string desc)
        {
            TextReader reader = new StringReader(desc);
            MakeMatch(reader);
        }
    }
}
