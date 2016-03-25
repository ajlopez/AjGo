using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AjGo.WinForm
{
    class BoardImage
    {
        private Bitmap image;
        private Graphics graphics;
        private Brush brush;
        private short width;
        private short height;

        public BoardImage()
            : this(19,19)
        {
        }

        public BoardImage(short width, short height)
        {
            this.width = width;
            this.height = height;
            image = new Bitmap(width * 20, height * 20);
            graphics = Graphics.FromImage(image);
            brush = Brushes.Beige;
        }

        public Image Image
        {
            get { return image; }
        }

        private void DrawBoard()
        {
            graphics.FillRectangle(Brushes.LightGoldenrodYellow, 0, 0, image.Width, image.Height);
            for (short k = 0; k < width; k++)
            {
                graphics.DrawLine(Pens.Black, 10, 10 + k * 20, image.Width - 10, 10 + k * 20);
                graphics.DrawLine(Pens.Black, 10 + k * 20, 10, 10 + k * 20, image.Height - 10);
            }
        }

        private void DrawStone(short x, short y, Color color)
        {
            Brush brush = null;

            switch (color)
            {
                case Color.Black:
                    brush = Brushes.Black;
                    break;

                case Color.White:
                    brush = Brushes.White;
                    break;
            }

            int px = 10 + x * 20;
            int py = 10 + y * 20;

            graphics.FillEllipse(brush, px - 10, py - 10, 20, 20);

            if (color == Color.White)
                graphics.DrawEllipse(Pens.Black, px - 10, py - 10, 20, 20);
        }

        private void DrawLeftGroup(short x, short y, Color color)
        {
            Pen pen = null;

            switch (color)
            {
                case Color.Black:
                    pen = Pens.Gray;
                    break;

                case Color.White:
                    pen = Pens.Gray;
                    break;
            }

            int px = 10 + x * 20;
            int py = 10 + y * 20;

            graphics.DrawLine(pen, px, py, px - 10, py);
        }

        private void DrawUpGroup(short x, short y, Color color)
        {
            Pen pen = null;

            switch (color)
            {
                case Color.Black:
                    pen = Pens.Gray;
                    break;

                case Color.White:
                    pen = Pens.Gray;
                    break;
            }

            int px = 10 + x * 20;
            int py = 10 + y * 20;

            graphics.DrawLine(pen, px, py, px, py - 20);
        }

        public void DrawPosition(Game game)
        {
            DrawBoard();

            for (short x = 0; x < game.Position.Width; x++)
                for (short y = 0; y < game.Position.Height; y++) {
                    Color c = game.GetColor(x,y);

                    if (c == Color.White || c == Color.Black)
                    {
                        DrawStone(x, y, c);
                        Group gr = game.GetGroup(x, y);
                        if (gr == game.GetGroup((short) (x-1),y))
                            DrawLeftGroup(x,y,c);
                        if (gr == game.GetGroup(x,(short) (y-1)))
                            DrawUpGroup(x,y,c);
                    }
                }
        }

        public void DrawPointSet(PointSet ps, Brush brush, short radius)
        {
            foreach (Point pt in ps.Points)
            {
                int px = 10 + 20 * pt.X;
                int py = 10 + 20 * pt.Y;

                graphics.FillEllipse(brush, px - radius, py - radius, radius*2, radius*2);
            }
        }

        public void DrawColors(Position position)
        {
            for (short x = 0; x < position.Width; x++)
                for (short y = 0; y < position.Height; y++)
                {
                    Color c = position.GetColor(x, y);

                    if (c == Color.Empty || c == Color.Border || c == Color.White || c == Color.Black)
                        continue;

                    Brush brush = null;

                    switch (c)
                    {
                        case Color.Yellow:
                            brush = Brushes.Yellow;
                            break;
                        case Color.Blue:
                            brush = Brushes.Blue;
                            break;
                        case Color.Red:
                            brush = Brushes.Red;
                            break;
                        case Color.Green:
                            brush = Brushes.Green;
                            break;
                    }

                    int px = 10 + x * 20;
                    int py = 10 + y * 20;

                    graphics.FillEllipse(brush, px - 5, py - 5, 10, 10);
                }
        }
    }
}

