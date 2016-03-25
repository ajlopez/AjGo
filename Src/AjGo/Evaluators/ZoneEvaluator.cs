using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Evaluators
{
    public class ZoneEvaluator
    {
        public ZoneEvaluation Evaluate(GroupSet zone, Position position)
        {
            return new ZoneEvaluation(zone, position);
        }
    }

    public class ZoneEvaluation
    {
        private Color color;
        private GroupSet zone;
        private GroupSet greenzones;
        private GroupSet eyegroups;
        private int internals;
        private int greeneyes;
        private int blueeyes;
        private bool issafe;
        private int ngroups;
        private int nsafegroups;
        private int stonesize;
        private int pointvalue;

        private static Color GetZoneColor(GroupSet zone)
        {
            Color color = Color.Empty;

            foreach (Group group in zone.Groups)
            {
                if (group.Color == Color.White || group.Color == Color.Yellow)
                    return Color.White;
                if (group.Color == Color.Black || group.Color == Color.Blue)
                    return Color.Black;
                if (group.Color == Color.Green)
                    color = Color.Green;
                else
                    return group.Color;
            }

            return color;
        }

        private bool IsTrueEye(Group group)
        {
            foreach (Group n in group.Neighbours.Groups)
                if (n.Color == color)
                {
                    if (!IsSafeGroup(n))
                        return false;
                }
                else if ((color == Color.Black && n.Color == Color.Blue) || (color == Color.White && n.Color == Color.Yellow))
                {
                    foreach (Group n2 in group.Neighbours.Groups)
                        if (n2.Color == color)
                            if (!IsSafeGroup(n2))
                                return false;
                }

            return true;
        }

        private bool IsSafeGroup(Group group)
        {
            if (greeneyes + blueeyes < 2)
                return false;

            short neyes=0;

            foreach (Group n in group.Neighbours.Groups)
                if (eyegroups.Groups.Contains(n))
                    neyes++;
                else if ((group.Color == Color.Black && n.Color == Color.Blue) || (group.Color == Color.White && n.Color == Color.Yellow))
                {
                    foreach (Group n2 in n.Neighbours.Groups)
                        if (eyegroups.Groups.Contains(n2)) {
                            neyes++;
                            break; // no more than one green eye
                        }
                }

            return neyes >= 2;
        }

        public ZoneEvaluation(GroupSet zone, Position position) {
            color = GetZoneColor(zone);

            this.zone = zone;
            greenzones = zone.GetNeighboursByColor(Color.Green);
            eyegroups = new GroupSet();

            if (color == Color.Black)
            {
                internals = CountInternals(Color.Blue, Color.Black, position);

                foreach (Group gr in zone.Groups)
                {
                    if (gr.Color == Color.Green)
                    {
                        greeneyes++;
                        eyegroups.Add(gr);
                    }
                    else if (gr.Color == Color.Blue)
                    {
                        int nblacks = 0;
                        foreach (Group n in gr.Neighbours.Groups)
                            if (n.Color == Color.Black)
                                nblacks++;

                        if (nblacks == gr.Neighbours.Count)
                        {
                            blueeyes++;
                            eyegroups.Add(gr);
                        }
                    }
                }
            }

            if (color == Color.White)
            {
                internals = CountInternals(Color.Yellow, Color.White, position);

                foreach (Group gr in zone.Groups)
                {
                    if (gr.Color == Color.Green)
                    {
                        greeneyes++;
                        eyegroups.Add(gr);
                    }
                    else if (gr.Color == Color.Yellow)
                    {
                        int nwhites = 0;
                        foreach (Group n in gr.Neighbours.Groups)
                            if (n.Color == Color.White)
                                nwhites++;

                        if (nwhites == gr.Neighbours.Count)
                        {
                            blueeyes++;
                            eyegroups.Add(gr);
                        }
                    }
                }
            }

            GroupSet falseeyes = new GroupSet();

            foreach (Group eyegroup in eyegroups.Groups)
                if (!IsTrueEye(eyegroup))
                    falseeyes.Add(eyegroup);

            foreach (Group gr in zone.Groups)
                if (gr.Color == color)
                {
                    ngroups++;
                    stonesize += gr.Count;
                    if (IsSafeGroup(gr))
                        nsafegroups++;
                }

            if (eyegroups.Count>=2)
                issafe = true;

            foreach (Group group in zone.Groups)
                if (group.Color != color)
                    foreach (Point pt in group.Points)
                        pointvalue += position.ValuePoint(pt.X, pt.Y);
        }

        private int CountInternals(Color color, Color color2, Position position)
        {
            int internals = 0;

            foreach (Group group in zone.Groups)
                if (group.Color == color)
                    foreach (Point point in group.Points)
                    {
                        ColorCount cc = position.CountFrontierColors(point);
                        if (cc.Count[(int)color] + cc.Count[(int)color2] + cc.Count[(int)Color.Border] == 8)
                            internals++;
                    }
                else if (group.Color == Color.Green)
                {
                    internals += group.Count;
                    internals += group.CalculateFrontier(position).Count;
                }

            return internals;
        }

        public int PointValue
        {
            get { return pointvalue; }
        }

        public Color Color {
            get { return color; }
        }

        public int Size
        {
            get { return zone.Size; }
        }

        public int StoneSize
        {
            get
            {
                return stonesize;
            }
        }

        public int NGroups
        {
            get { return ngroups; }
        }

        public int NSafeGroups
        {
            get { return nsafegroups; }
        }

        public bool IsSafe
        {
            get { return issafe; }
        }

        public int InternalCount
        {
            get { return internals; }
        }

        public int TrueEyes
        {
            get { return eyegroups.Count; }
        }

        public int GreenEyes
        {
            get { return greeneyes; }
        }

        public int BlueEyes
        {
            get { return blueeyes; }
        }

        public int GreenLife
        {
            get
            {
                int size = 0;

                foreach (Group group in greenzones.Groups)
                    size += group.Count;

                return size;
            }
        }
    }
}

