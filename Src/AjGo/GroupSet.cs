using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo
{
    public class GroupSet
    {
        private List<Group> groups = new List<Group>();

        public void Add(Group group)
        {
            if (!groups.Contains(group))
                groups.Add(group);
        }

        public void Remove(Group group)
        {
            groups.Remove(group);
        }

        public List<Group> Groups
        {
            get { return groups; }
        }

        public int Count
        {
            get { return groups.Count; }
        }

        public int Size
        {
            get
            {
                int size = 0;

                foreach (Group group in groups)
                    size += group.Count;

                return size;
            }
        }

        public GroupSet GetNeighboursByColor(Color color)
        {
            GroupSet gs = new GroupSet();

            foreach (Group group in groups)
                foreach (Group neighbour in group.Neighbours.Groups)
                    if (neighbour.Color == color && !groups.Contains(neighbour))
                        gs.Add(neighbour);

            return gs;
        }
    }
}
