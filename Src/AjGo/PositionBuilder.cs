using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AjGo
{
    public class PositionBuilder
    {
        private Position position;

        public Position GetPosition()
        {
            if (position == null)
                position = new Position();

            return position;
        }

        public void MakeRow(short nrow, string rowdes)
        {
            short ncol = 0;

            foreach (char ch in rowdes)
                switch (ch)
                {
                    case 'X':
                        GetPosition().SetColor(ncol, nrow, Color.Black);
                        ncol++;
                        break;
                    case 'O':
                        GetPosition().SetColor(ncol, nrow, Color.White);
                        ncol++;
                        break;
                    case '.':
                        GetPosition().SetColor(ncol, nrow, Color.Empty);
                        ncol++;
                        break;
                }
        }

        public void MakePosition(TextReader description)
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

        public void MakePosition(string desc)
        {
            TextReader reader = new StringReader(desc);
            MakePosition(reader);
        }

        public static void SavePosition(TextWriter writer, Position position)
        {
            for (short y = 0; y < position.Height; y++)
            {
                for (short x = 0; x < position.Width; x++)
                {
                    if (x > 0)
                        writer.Write(" ");
                    Color color = position.GetColor(x, y);

                    switch (color)
                    {
                        case Color.Black:
                            writer.Write("X");
                            break;
                        case Color.White:
                            writer.Write("O");
                            break;
                        default:
                            writer.Write(".");
                            break;
                    }
                }

                writer.WriteLine();
            }
        }
    }
}

