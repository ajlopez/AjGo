using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo
{
    public class MatchResult
    {
        private Match match;
        private Point point;
        private bool haslast;

        public MatchResult(Match match, Point point, bool haslast)
        {
            this.match = match;
            this.point = point;
            this.haslast = haslast;
        }

        public Match Match
        {
            get { return match; }
        }

        public Point Point
        {
            get { return point; }
        }

        public bool HasLast
        {
            get { return haslast; }
        }
    }

    public class Match
    {
        private Color[,] cells;
        private short width;
        private short height;
        private string name;

        public Match(short width, short height)
        {
            cells = new Color[width, height];
            this.width = width;
            this.height = height;

            for (short x = 0; x < width; x++)
                for (short y = 0; y < height; y++)
                    cells[x, y] = Color.Empty;
        }

        public Match(short width, short height, string name) : this(width,height)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        public short Width
        {
            get { return width; }
        }

        public short Height
        {
            get { return height; }
        }

        public bool IsName(string name)
        {
            if (this.name == null)
                return false;

            if (this.name.StartsWith(name))
                return true;

            return false;
        }

        public void SetColor(short x, short y, Color c)
        {
            cells[x, y] = c;
        }

        public Color GetColor(short x, short y)
        {
            return cells[x, y];
        }

        private bool MatchCell(short x, short y, short x2, short y2, Position p)
        {
            Color c2 = cells[x,y];

            if (c2 == Color.Green)
                return true;

            Color c = p.GetColor(x2 - 1, y2 - 1);

            if (c2 == c)
                return true;

            if (c2 == Color.Red && c == Color.Empty)
                return true;

            if (c2 == Color.Yellow && c == Color.White)
                return true;
            if (c2 == Color.Yellow && c == Color.Empty)
                return true;

            if (c2 == Color.Blue && c == Color.Black)
                return true;
            if (c2 == Color.Blue && c == Color.Empty)
                return true;

            return false;
        }

        public List<MatchResult> MatchMoves(Position p)
        {
            return MatchMoves(p, null);
        }

        public List<MatchResult> MatchMoves(Position p, Point lastmove)
        {
            List<MatchResult> results = new List<MatchResult>();

            for (short reflect = 0; reflect<=1; reflect++)
            for (short dx = 0; dx<p.Width+2-width; dx++)
            for (short dy = 0; dy<p.Height+2-height; dy++)
            for (short rotate = 0; rotate < 4; rotate++)
            {
                bool match = true;
                bool haslast = false;
                List<Point> points2 = new List<Point>();

                for (short x = 0; match && x < width; x++)
                    for (short y = 0; match && y < height; y++)
                    {
                        short x2 = (short) (x+dx);
                        short y2 = (short) (y+dy);

                        for (short nr = 0; nr < rotate; nr++)
                        {
                            short ny = x2;
                            short nx = (short)(p.Width + 2 - y2 - 1);

                            x2 = nx;
                            y2 = ny;
                        }

                        if (reflect == 1)
                        {
                            short aux = x2;
                            x2 = y2;
                            y2 = aux;
                        }

                        if (!MatchCell(x, y, x2, y2, p))
                            match = false;
                        if (lastmove != null && lastmove.X == x2 && lastmove.Y == y2)
                            haslast = true;
                        else if (cells[x, y] == Color.Red)
                            points2.Add(new Point((short)(x2 - 1), (short)(y2 - 1)));
                    }

                if (match == true)
                    foreach (Point pt in points2)
                    {
                        bool repeated = false;

                        foreach (MatchResult r in results)
                        {
                            if (r.Point.Equals(pt))
                            {
                                repeated = true;
                                break;
                            }
                        }

                        if (!repeated)
                            results.Add(new MatchResult(this, pt, haslast));
                    }
            }

            return results;
        }
    }
}

