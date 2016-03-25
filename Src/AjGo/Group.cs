using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo
{
    public class Group : PointSet
    {
        private PointSet liberties = new PointSet();
        private Color color;
        private GroupPosition groupposition;
        private GroupSet neighbours;

        public Group(Color c)
        {
            color = c;
        }

        public Color Color
        {
            get { return color; }
        }

        public int CountLiberties
        {
            get { return liberties.Count; }
        }

        public GroupPosition GroupPosition
        {
            get { return groupposition; }
            set { groupposition=value; }
        }

        public GroupSet Neighbours
        {
            get
            {
                if (neighbours == null)
                    neighbours = new GroupSet();

                return neighbours;
            }
        }

        public PointSet Liberties
        {
            get { return liberties; }
        }

        public override void Add(Point p)
        {
            base.Add(p);
            liberties.Remove(p);
        }

        public override void Add(short x, short y)
        {
            base.Add(x, y);
            liberties.Remove(x, y);
        }

        public void AddLiberty(Point p)
        {
            liberties.Add(p);
        }

        public void AddLiberty(short x, short y)
        {
            liberties.Add(x, y);
        }

        public void RemoveLiberty(Point p)
        {
            liberties.Remove(p);
        }

        public void RemoveLiberty(short x, short y)
        {
            liberties.Remove(x, y);
        }

        public void Merge(Group gr)
        {
            if (gr.Color != Color)
                throw new InvalidOperationException();

            base.Add(gr);
            liberties.Add(gr.liberties);
        }

        public new Group Clone()
        {
            Group gr = new Group(color);
            gr.points.AddRange(points);
            gr.liberties = liberties.Clone();
            return gr;
        }

        public override PointSet CalculateLiberties(Position pos)
        {
            liberties = base.CalculateLiberties(pos);
            return liberties;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            if (!base.Equals(obj))
                return false;

            Group gr = obj as Group;

            if (gr == null)
                return false;

            return gr.color == color;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * (((int)color) + 3);
        }
    }
}

