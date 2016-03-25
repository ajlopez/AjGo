using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo
{
    public class Position
    {
        private short width;
        private short height;

        private Color[,] cells;

        public short Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public short Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        public int Size
        {
            get
            {
                return height * width;
            }
        }


        public Position()
            : this(19,19)
        {
        }

        public Position(short width, short height)
        {
            this.width = width;
            this.height = height;
            cells = new Color[width+2, height+2];

            for (short x = 0; x < width + 2; x++)
                for (short y = 0; y < height + 2; y++)
                    if (x == 0 || x == width + 1 || y == 0 || y == height + 1)
                        cells[x, y] = Color.Border;
                    else
                        cells[x, y] = Color.Empty;
        }

        public Position Clone()
        {
            Position p = new Position(width, height);

            for (short x = 0; x < width; x++)
                for (short y = 0; y < height; y++)
                    p.cells[x + 1, y + 1] = cells[x + 1, y + 1];

            return p;
        }

        public Color GetColor(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y > height)
                return Color.Border;

            return cells[x + 1, y + 1];
        }

        public void SetColor(int x, int y, Color color)
        {
            if (cells[x + 1, y + 1] == Color.Border && color != Color.Border)
                throw new InvalidOperationException();

            cells[x + 1, y + 1] = color;
        }

        public bool IsEmpty(int x, int y)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
                return false;

            return (cells[x + 1, y + 1] != Color.White && cells[x + 1, y + 1] != Color.Black);
        }

        public short CountColor(Color color)
        {
            short n = 0;

            for (short x = 0; x < width; x++)
                for (short y = 0; y < height; y++)
                    if (cells[x + 1, y + 1] == color)
                        n++;

            return n;
        }

        public ColorCount CountColors()
        {
            ColorCount cc = new ColorCount();

            for (short x=0; x<width; x++)
                for (short y=0; y<height; y++)
                    cc.Count[(int)GetColor(x,y)]++;

            return cc;
        }

        public ColorCount CountColors(PointSet ps)
        {
            ColorCount cc = new ColorCount();

            foreach (Point pt in ps.Points)
                cc.Count[(int) GetColor(pt.X, pt.Y)]++;

            return cc;
        }

        public ColorCount CountFrontierColors(Point pt)
        {
            ColorCount cc = new ColorCount();

            for (short dx = -1; dx <= 1; dx++)
                for (short dy = -1; dy <= 1; dy++)
                    if (dx != 0 || dy != 0)
                        cc.Count[(int) GetColor((short)(pt.X + dx), (short)(pt.Y + dy))]++;

            return cc;
        }

        public short NoColor(int x, int y, Color color)
        {
            short nc = 0;

            if (cells[x,y+1]==color)
                nc++;

            if (cells[x + 2, y + 1] == color)
                nc++;

            if (cells[x + 1, y] == color)
                nc++;

            if (cells[x + 1, y + 2] == color)
                nc++;

            return nc;
        }

        public void Play(Move move)
        {
            if (cells[move.Point.X + 1, move.Point.Y + 1] == Color.Border)
                throw new InvalidOperationException();

            cells[move.Point.X + 1, move.Point.Y + 1] = move.Color;
        }

        public void CalculateColors()
        {
            RemoveColors();

            for (short x = 0; x < width; x++)
                for (short y = 0; y < height; y++)
                    CalculateColor(x, y);

            for (short x = 0; x < width; x++)
                for (short y = 0; y < height; y++)
                    ReviewColor(x, y);
        }

        private void CalculateColor(short x, short y)
        {
            if (cells[x + 1, y + 1] != Color.Empty)
                return;

            short nw=0;
            short nb=0;

            // Count in liberties
            short nwlib = 0;
            short nblib = 0;

            for (short dx = -1; dx<=1 ; dx++)
                for (short dy = -1; dy<=1; dy++)
                    switch (cells[x + 1 + dx, y + 1 + dy])
                    {
                        case Color.Black:
                            nb++;
                            if (dx == 0 || dy == 0)
                                nblib++;
                            break;
                        case Color.White:
                            nw++;
                            if (dx == 0 || dy == 0)
                                nwlib++;
                            break;
                    }

            if (nb > 0 && nw > 0)
            {
                cells[x + 1, y + 1] = Color.Red;

                // new evaluation

                if (nblib >= 1 && nwlib == 0)
                    cells[x + 1, y + 1] = Color.Blue;
                else if (nwlib >= 1 && nblib == 0)
                    cells[x + 1, y + 1] = Color.Yellow;
            }
            else if (nb > 0)
                cells[x + 1, y + 1] = Color.Blue;
            else if (nw > 0)
                cells[x + 1, y + 1] = Color.Yellow;
            else
                cells[x + 1, y + 1] = Color.Green;
        }


        private void ReviewColor(short x, short y)
        {
            if (cells[x + 1, y + 1] != Color.Green)
                return;

            if (cells[x, y] == Color.Green && cells[x+1, y] == Color.Green && cells[x, y+1] == Color.Green)
                return;

            if (cells[x, y+2] == Color.Green && cells[x, y+1] == Color.Green && cells[x+1, y+2] == Color.Green)
                return;

            if (cells[x+2, y] == Color.Green && cells[x+1, y] == Color.Green && cells[x+2, y+1] == Color.Green)
                return;

            if (cells[x+2, y+2] == Color.Green && cells[x+2, y+1] == Color.Green && cells[x+1, y+2] == Color.Green)
                return;

            short nw = 0;
            short nb = 0;

            for (short dx = -1; dx <= 1; dx++)
                for (short dy = -1; dy <= 1; dy++)
                    switch (cells[x + 1 + dx, y + 1 + dy])
                    {
                        case Color.Blue:
                            nb++;
                            break;
                        case Color.Yellow:
                            nw++;
                            break;
                    }

            if (nb > 0 && nw > 0)
                cells[x + 1, y + 1] = Color.Red;
            else if (nb > 0)
                cells[x + 1, y + 1] = Color.Blue;
            else if (nw > 0)
                cells[x + 1, y + 1] = Color.Yellow;
            else
                cells[x + 1, y + 1] = Color.Green;
        }

        private void RemoveColors()
        {
            for (short x = 0; x <= width+1; x++)
                for (short y = 0; y <= height + 1; y++)
                {
                    if (x == 0 || y == 0 || x == width + 1 || y == height + 1)
                        cells[x, y] = Color.Border;
                    else if (cells[x, y] != Color.Border && cells[x, y] != Color.Black && cells[x, y] != Color.White)
                        cells[x, y] = Color.Empty;
                }
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            Position p = obj as Position;

            if (p == null)
                return false;

            if (p.width != width)
                return false;
            if (p.height != height)
                return false;

            for (short x = 0; x < width + 2; x++)
                for (short y = 0; y < width + 2; y++)
                    if (cells[x, y] != p.cells[x, y])
                        return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hash = width.GetHashCode() * 3 + height.GetHashCode() * 5;

            for (short x = 0; x < width + 2; x++)
                for (short y = 0; y < width + 2; y++)
                    if (cells[x, y] == Color.Black)
                        hash += (y * width + x) * 13;
                    else if (cells[x,y]==Color.White)
                        hash += (y * width + x) * 17;

            return hash;
        }

        private Color InvertedColor(Color c)
        {
            switch (c)
            {
                case Color.White:
                    return Color.Black;
                case Color.Black:
                    return Color.White;
                case Color.Yellow:
                    return Color.Blue;
                case Color.Blue:
                    return Color.Yellow;
            }

            return c;
        }

        public Position InvertedClone()
        {
            Position pos = new Position(width, height);

            for (short x=0; x<width; x++)
                for (short y=0; y<height; y++)
                    pos.SetColor(x,y,InvertedColor(GetColor(x,y)));

            return pos;
        }

        private bool IsFreePath(PointSet ps)
        {
            for (int np = 1; np < ps.Points.Count - 1; np++)
                if (!IsEmpty(ps.Points[np].X, ps.Points[np].Y))
                    return false;

            return true;
        }

        private static bool PointTouchPath(Point pt, PointSet path)
        {
            Point auxpt;
            int touched = 0;

            auxpt = new Point((short)(pt.X - 1), pt.Y);
            if (path.Points.Contains(auxpt))
                touched++;
            auxpt = new Point((short)(pt.X + 1), pt.Y);
            if (path.Points.Contains(auxpt))
                touched++;
            auxpt = new Point(pt.X, (short) (pt.Y-1));
            if (path.Points.Contains(auxpt))
                touched++;
            auxpt = new Point(pt.X, (short)(pt.Y + 1));
            if (path.Points.Contains(auxpt))
                touched++;

            return (touched>=2);
        }

        private void AddAndExpand(List<PointSet> paths, PointSet path, short maxlength)
        {
            if (path.Count > maxlength)
                return;

            if (paths.Contains(path))
                return;

            paths.Add(path);

            if (path.Count >= maxlength)
                return;

            Point lastpoint = path.Points[path.Count - 1];

            Point newpoint;

            newpoint = new Point((short)(lastpoint.X - 1), lastpoint.Y);

            if (!path.Points.Contains(newpoint) && !PointTouchPath(newpoint,path))
            {
                PointSet newpath = path.Clone();
                newpath.Add(newpoint);
                AddAndExpand(paths, newpath, maxlength);
            }

            newpoint = new Point((short)(lastpoint.X + 1), lastpoint.Y);

            if (!path.Points.Contains(newpoint) && !PointTouchPath(newpoint, path))
            {
                PointSet newpath = path.Clone();
                newpath.Add(newpoint);
                AddAndExpand(paths, newpath, maxlength);
            }

            newpoint = new Point(lastpoint.X, (short) (lastpoint.Y-1));

            if (!path.Points.Contains(newpoint) && !PointTouchPath(newpoint, path))
            {
                PointSet newpath = path.Clone();
                newpath.Add(newpoint);
                AddAndExpand(paths, newpath, maxlength);
            }

            newpoint = new Point(lastpoint.X, (short)(lastpoint.Y + 1));

            if (!path.Points.Contains(newpoint) && !PointTouchPath(newpoint, path))
            {
                PointSet newpath = path.Clone();
                newpath.Add(newpoint);
                AddAndExpand(paths, newpath, maxlength);
            }
        }

        public List<PointSet> GetPathsFrom(short x, short y, short maxlength)
        {
            List<PointSet> paths = new List<PointSet>();

            if (GetColor(x, y) == Color.Border)
                return paths;

            PointSet path = new PointSet();
            path.Add(x, y);

            AddAndExpand(paths, path, maxlength);

            return paths;
        }

        private int ValueDistance(int dx)
        {
            int xval = 1;

            switch (dx)
            {
                case 0:
                    xval = 1;
                    break;
                case 1:
                    xval = 2;
                    break;
                case 2:
                    xval = 4;
                    break;
                case 3:
                    xval = 4;
                    break;
                case 4:
                    xval = 4;
                    break;
                default:
                    xval = 1;
                    break;
            }

            return xval;
        }

        public int ValuePoint(short x, short y)
        {
            int xval = 1;
            int yval = 1;
            int dx;
            int dy;

            if (x <= (width-1) / 2)
                dx = x;
            else
                dx = (width-1) - x;

            if (y <= (height-1) / 2)
                dy = y;
            else
                dy = (height-1) - y;

            xval = ValueDistance(dx);
            yval = ValueDistance(dy);

            return xval + yval;
        }

        public List<PointSet> GetPathsFromTo(short x, short y, short x2, short y2, short maxlength)
        {
            List<PointSet> allpaths = GetPathsFrom(x, y, maxlength);
            List<PointSet> paths = new List<PointSet>();

            foreach (PointSet ps in allpaths)
            {
                Point endpoint = ps.Points[ps.Count - 1];

                if (endpoint.X == x2 && endpoint.Y == y2)
                    paths.Add(ps);
            }

            return paths;
        }

        public List<PointSet> GetFreePathsFromTo(short x, short y, short x2, short y2, short maxlength)
        {
            List<PointSet> allpaths = GetPathsFrom(x, y, maxlength);
            List<PointSet> paths = new List<PointSet>();

            foreach (PointSet ps in allpaths)
            {
                Point endpoint = ps.Points[ps.Count - 1];

                if (endpoint.X == x2 && endpoint.Y == y2 && IsFreePath(ps))
                {                    
                    paths.Add(ps);
                }
            }

            return paths;
        }
    }
}

