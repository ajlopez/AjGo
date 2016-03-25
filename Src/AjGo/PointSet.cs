using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo
{
    public class PointSet
    {
        protected List<Point> points = new List<Point>();

        public static PointSet Union(PointSet ps1, PointSet ps2)
        {
            return new PointSet(ps1, ps2);
        }

        public static PointSet Intersect(PointSet ps1, PointSet ps2)
        {
            PointSet ps = new PointSet();

            foreach (Point pt in ps1.Points)
                if (ps2.Points.Contains(pt))
                    ps.Add(pt);

            return ps;
        }

        public static PointSet Difference(PointSet ps1, PointSet ps2)
        {
            PointSet ps = new PointSet();


            foreach (Point pt in ps1.Points)
                if (!ps2.Points.Contains(pt))
                    ps.Add(pt);

            return ps;
        }

        public PointSet()
        {
        }

        public PointSet(PointSet ps)
        {
            Add(ps);
        }

        public PointSet(PointSet ps1, PointSet ps2)
        {
            Add(ps1);
            Add(ps2);
        }

        public virtual void Add(Point p)
        {
            if (points.Contains(p))
                return;

            points.Add(p);
        }

        public virtual void Add(short x, short y)
        {
            Add(new Point(x, y));
        }

        public virtual void Remove(Point p)
        {
            points.Remove(p);
        }

        public virtual void Remove(short x, short y)
        {
            Remove(new Point(x, y));
        }

        public int Count
        {
            get { return points.Count; }
        }

        public List<Point> Points
        {
            get { return points; }
        }

        public void Add(PointSet ps2)
        {
            if (points == ps2.points)
                return;

            foreach (Point p in ps2.points)
                Add(p);
        }

        public void Subtract(PointSet ps)
        {
            foreach (Point pt in ps.Points)
                Remove(pt);
        }

        public PointSet Clone()
        {
            PointSet ps = new PointSet();

            ps.points.AddRange(points);

            return ps;
        }

        public virtual PointSet CalculateLiberties(Position pos)
        {
            PointSet liberties = new PointSet();

            foreach (Point pt in points)
            {
                if (pos.IsEmpty(pt.X - 1, pt.Y))
                    liberties.Add(new Point((short) (pt.X - 1), pt.Y));

                if (pos.IsEmpty(pt.X + 1, pt.Y))
                    liberties.Add(new Point((short) (pt.X + 1), pt.Y));

                if (pos.IsEmpty(pt.X, pt.Y - 1))
                    liberties.Add(new Point(pt.X, (short) (pt.Y - 1)));

                if (pos.IsEmpty(pt.X, pt.Y + 1))
                    liberties.Add(new Point(pt.X, (short) (pt.Y + 1)));
            }

            return liberties;
        }

        public virtual PointSet CalculateFrontier(Position pos)
        {
            PointSet frontier = new PointSet();

            foreach (Point pt in points)
            {
                for (short dx = -1; dx <= 1; dx++)
                    for (short dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0)
                            continue;

                        short x = (short)(pt.X + dx);
                        short y = (short)(pt.Y + dy);

                        if (pos.GetColor(x, y) == Color.Border)
                            continue;

                        Point newp = new Point(x,y);

                        if (!points.Contains(newp))
                            frontier.Add(newp);
                    }
            }

            return frontier;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            PointSet ps = obj as PointSet;

            if (ps == null)
                return false;

            if (ps.Count != Count)
                return false;

            for (int k = 0; k < Count; k++)
                if (!points[k].Equals(ps.points[k]))
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;

            for (int k = 0; k < points.Count; k++)
            {
                hash *= 17;
                hash += points[k].GetHashCode();
            }

            return hash;
        }
    }
}
