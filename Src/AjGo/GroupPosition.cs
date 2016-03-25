using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo
{
    public class GroupPosition
    {
        private Position position;
        private List<Group> groups;
        private Group[,] groupmap;
        private List<GroupSet> zones;

        public GroupPosition(Position pos)
        {
            position = pos;
            groups = new List<Group>();
        }

        public List<Group> Groups
        {
            get { return groups; }
        }

        public List<GroupSet> Zones
        {
            get
            {
                if (zones == null)
                    CalculateZones();

                return zones;
            }
        }

        public void CalculateZones()
        {
            zones = new List<GroupSet>();
            List<Group> processed = new List<Group>();

            foreach (Group group in groups) {
                if (!processed.Contains(group))
                {
                    GroupSet zone = GetZone(group);

                    foreach (Group gr in zone.Groups)
                        processed.Add(gr);

                    zones.Add(zone);
                }
            }
        }

        public void CalculateGroups()
        {
            groupmap = new Group[position.Width, position.Height];
            groups = new List<Group>();

            for (short x = 0; x < position.Width; x++)
                for (short y = 0; y < position.Height; y++)
                    ReviewCell(x, y);

            foreach (Group group in groups)
                group.GroupPosition = this;

            CalculateLiberties();
        }

        public void CalculateNeighbours()
        {
            for (short x = 0; x < position.Width; x++)
                for (short y = 0; y < position.Height; y++)
                {
                    Group group = GetGroup(x, y);

                    if (group==null)
                        continue;

                    Group group2;

                    if (x > 0)
                    {
                        group2 = GetGroup((short) (x - 1), y);

                        if (group != group2 && group2 != null)
                        {
                            group.Neighbours.Add(group2);
                            group2.Neighbours.Add(group);
                        }
                    }

                    if (y > 0)
                    {
                        group2 = GetGroup(x, (short) (y-1));

                        if (group != group2 && group2 != null)
                        {
                            group.Neighbours.Add(group2);
                            group2.Neighbours.Add(group);
                        }
                    }

                    // TODO Removed, now adjusted neighbours has same color
                    // with two liberties in common at least

                    //if (x > 0 && y > 0)
                    //{
                    //    group2 = GetGroup((short)(x - 1), (short)(y - 1));

                    //    if (group != group2 && group2 != null && group.Color == group2.Color && (group.Color==Color.White || group.Color==Color.Black) && position.GetColor((short)(x - 1), y) == Color.Red && position.GetColor(x, (short)(y - 1)) == Color.Red)
                    //    {
                    //        group.Neighbours.Add(group2);
                    //        group2.Neighbours.Add(group);
                    //    }

                    //}

                    //if (x > 0 && y < position.Height-1)
                    //{
                    //    group2 = GetGroup((short)(x - 1), (short)(y + 1));

                    //    if (group != group2 && group2 != null && group.Color == group2.Color && (group.Color==Color.White || group.Color==Color.Black) && position.GetColor((short)(x - 1), y) == Color.Red && position.GetColor(x, (short)(y + 1)) == Color.Red)
                    //    {
                    //        group.Neighbours.Add(group2);
                    //        group2.Neighbours.Add(group);
                    //    }

                    //}
                }

            // Adjusted Neighbours

            foreach (Group group in groups)
                foreach (Group group2 in groups)
                    if (group!=group2 && group.Color == group2.Color && !group.Neighbours.Groups.Contains(group2))
                    {
                        PointSet libertiesincommon = PointSet.Intersect(group.Liberties,group2.Liberties);

                        if (libertiesincommon.Count > 1)
                        {
                            group.Neighbours.Add(group2);
                            group2.Neighbours.Add(group);
                        }
                    }
        }

        public void CalculateLiberties()
        {
            foreach (Group group in groups)
                group.CalculateLiberties(position);
        }

        public void KillGroup(Group g)
        {
            foreach (Point p in g.Points)
            {
                AddLiberty(p.X, p.Y);
                groupmap[p.X, p.Y] = null;
                position.SetColor(p.X, p.Y, Color.Empty);
            }

            groups.Remove(g);
        }

        public void ReviewCell(short x, short y)
        {
            Color c = position.GetColor(x, y);

            if (c == Color.Empty || c==Color.Border)
                return;

            if (c == position.GetColor(x - 1, y))
                AddToGroup(groupmap[x - 1, y], x, y);

            if (c == position.GetColor(x, y - 1))
                AddToGroup(groupmap[x, y - 1], x, y);

            if (c == position.GetColor(x + 1, y))
                AddToGroup(groupmap[x + 1, y], x, y);

            if (c == position.GetColor(x, y + 1))
                AddToGroup(groupmap[x, y + 1], x, y);

            Group gr = groupmap[x, y];

            if (gr == null)
            {
                gr = new Group(c);
                gr.Add(x, y);
                groups.Add(gr);
                groupmap[x, y] = gr;
            }
        }

        public Group GetGroup(short x, short y)
        {
            if (x < 0 || x >= position.Width)
                return null;

            if (y < 0 || y >= position.Height)
                return null;

            return groupmap[x, y];
        }

        private void AddToGroup(Group gr, short x, short y)
        {
            if (gr == null)
                return;

            Group grxy = groupmap[x, y];

            if (grxy == null)
            {
                gr.Add(x, y);
                groupmap[x, y] = gr;
            }
            else if (grxy != gr)
            {
                gr.Merge(grxy);
                groups.Remove(grxy);

                foreach (Point p in grxy.Points)
                    groupmap[p.X, p.Y] = gr;
            }

        }

        private void AddLiberty(short x, short y)
        {
            Group gr;

            if (x > 0)
            {
                gr = groupmap[x - 1, y];
                if (gr != null)
                    gr.AddLiberty(x, y);
            }

            if (x < position.Width-1)
            {
                gr = groupmap[x + 1, y];
                if (gr != null)
                    gr.AddLiberty(x, y);
            }

            if (y > 0)
            {
                gr = groupmap[x, y-1];
                if (gr != null)
                    gr.AddLiberty(x, y);
            }

            if (y < position.Height-1)
            {
                gr = groupmap[x, y + 1];
                if (gr != null)
                    gr.AddLiberty(x, y);
            }
        }

        public List<Group> GroupsInPointSet(PointSet ps)
        {
            List<Group> groups = new List<Group>();

            foreach (Point pt in ps.Points)
            {
                Group group = groupmap[pt.X, pt.Y];

                if (group != null && !groups.Contains(group))
                    groups.Add(group);
            }

            return groups;
        }

        public List<Group> GetNeighbours(Group gr)
        {
            return gr.Neighbours.Groups;
            //PointSet frontier = gr.CalculateFrontier(position);
            //return GroupsInPointSet(frontier);
        }

        private bool AreZoneColor(Color c1, Color c2)
        {
            if (c1 == c2)
                return true;

            if (c1 == Color.Black && c2 == Color.Blue)
                return true;

            if (c2 == Color.Black && c1 == Color.Blue)
                return true;

            if (c1 == Color.White && c2 == Color.Yellow)
                return true;

            if (c2 == Color.White && c1 == Color.Yellow)
                return true;

            return false;
        }

        private void ExpandZone(GroupSet zone, Color zonecolor, Group gr)
        {
            List<Group> neighbours = GetNeighbours(gr);

            foreach (Group group in neighbours)
            {
                if (zone.Groups.Contains(group))
                    continue;
                if (AreZoneColor(zonecolor, group.Color) ||
                    ((zonecolor==Color.White || zonecolor==Color.Yellow) && IsColorGreenGroup(group,Color.Yellow)) ||
                    ((zonecolor==Color.Black || zonecolor==Color.Blue) && IsColorGreenGroup(group,Color.Blue)))
                {
                    zone.Add(group);
                    ExpandZone(zone, zonecolor, group);
                }
            }
        }

        private static bool IsColorGreenGroup(Group group, Color color)
        {
            if (group.Color != Color.Green)
                return false;

            if (group.Neighbours.Count == 0)
                return false;

            foreach (Group gr in group.Neighbours.Groups)
                if (gr.Color != color)
                    return false;

            return true;
        }

        private static bool IsBlueGreenGroup(Group group)
        {
            return IsColorGreenGroup(group, Color.Blue);
        }

        private static bool IsYellowGreenGroup(Group group)
        {
            return IsColorGreenGroup(group, Color.Yellow);
        }

        public GroupSet GetZone(Group gr)
        {
            GroupSet zone = new GroupSet();

            zone.Add(gr);

            if (IsBlueGreenGroup(gr))
                ExpandZone(zone, Color.Blue, gr);
            else if (IsYellowGreenGroup(gr))
                ExpandZone(zone, Color.Yellow, gr);
            else
                ExpandZone(zone, gr.Color, gr);

            return zone;
        }

        public GroupSet GetZone(short x, short y)
        {
            Group group = GetGroup(x,y);

            if (group==null)
                return null;

            foreach (GroupSet zone in Zones)
                if (zone.Groups.Contains(group))
                    return zone;

            return null;
        }

        public List<Group> GetZoneGroups(Group gr)
        {
            return GetZone(gr).Groups;
        }

        public Group GetZoneGroup(Group gr)
        {
            if (gr == null)
                return null;

            Group group = new Group(gr.Color);

            foreach (Group g in GetZoneGroups(gr))
                if (g.Color == gr.Color)
                    group.Add(g);

            group.CalculateLiberties(position);

            return group;
        }
    }
}

