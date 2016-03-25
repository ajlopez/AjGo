using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class GroupPositionTests
    {
        [Test]
        public void ShouldCreate()
        {
            Position pos = new Position();
            GroupPosition gp = new GroupPosition(pos);

            Assert.IsNotNull(gp);
            Assert.IsNotNull(gp.Groups);
            Assert.AreEqual(0, gp.Groups.Count);
        }

        [Test]
        public void CalculateGroupsTest1()
        {
            Position pos = new Position();
            GroupPosition gp = new GroupPosition(pos);

            gp.CalculateGroups();

            Assert.IsNotNull(gp.Groups);
            Assert.AreEqual(0, gp.Groups.Count);
        }

        [Test]
        public void CalculateGroupsTest2()
        {
            Position pos = new Position();
            GroupPosition gp = new GroupPosition(pos);

            pos.Play(new Move(10, 10, Color.Black));

            gp.CalculateGroups();

            Assert.IsNotNull(gp.Groups);
            Assert.AreEqual(1, gp.Groups.Count);

            Group gr = gp.Groups[0];

            Assert.AreEqual(Color.Black, gr.Color);
            Assert.AreEqual(1, gr.Points.Count);
            Assert.AreEqual(4, gr.CountLiberties);

            foreach (Group group in gp.Groups)
                Assert.AreEqual(gp, group.GroupPosition);
        }

        [Test]
        public void CalculateGroupsTest3()
        {
            Position pos = new Position();
            GroupPosition gp = new GroupPosition(pos);

            pos.Play(new Move(10, 10, Color.Black));
            pos.Play(new Move(10, 11, Color.Black));

            gp.CalculateGroups();

            Assert.IsNotNull(gp.Groups);
            Assert.AreEqual(1, gp.Groups.Count);

            Group gr = gp.Groups[0];

            Assert.AreEqual(Color.Black, gr.Color);
            Assert.AreEqual(2, gr.Points.Count);
            Assert.AreEqual(6, gr.CountLiberties);

            foreach (Group group in gp.Groups)
                Assert.AreEqual(gp, group.GroupPosition);
        }

        [Test]
        public void CalculateGroupsTest4()
        {
            Position pos = new Position();
            GroupPosition gp = new GroupPosition(pos);

            pos.Play(new Move(10, 10, Color.Black));
            pos.Play(new Move(10, 11, Color.Black));
            pos.Play(new Move(10, 12, Color.White));

            gp.CalculateGroups();

            Assert.IsNotNull(gp.Groups);
            Assert.AreEqual(2, gp.Groups.Count);

            Group gr;
            
            gr = gp.Groups[0];

            Assert.AreEqual(Color.Black, gr.Color);
            Assert.AreEqual(2, gr.Points.Count);
            Assert.AreEqual(5, gr.CountLiberties);

            gr = gp.Groups[1];

            Assert.AreEqual(Color.White, gr.Color);
            Assert.AreEqual(1, gr.Points.Count);
            Assert.AreEqual(3, gr.CountLiberties);

            foreach (Group group in gp.Groups)
                Assert.AreEqual(gp, group.GroupPosition);
        }

        [Test]
        public void CalculateGroupsTest5()
        {
            Position pos = new Position();
            GroupPosition gp = new GroupPosition(pos);

            pos.Play(new Move(0, 0, Color.Black));
            pos.Play(new Move(0, 1, Color.Black));
            pos.Play(new Move(0, 2, Color.White));

            gp.CalculateGroups();

            Assert.IsNotNull(gp.Groups);
            Assert.AreEqual(2, gp.Groups.Count);

            Group gr;

            gr = gp.Groups[0];

            Assert.AreEqual(Color.Black, gr.Color);
            Assert.AreEqual(2, gr.Points.Count);
            Assert.AreEqual(2, gr.CountLiberties);

            gr = gp.Groups[1];

            Assert.AreEqual(Color.White, gr.Color);
            Assert.AreEqual(1, gr.Points.Count);
            Assert.AreEqual(2, gr.CountLiberties);

            foreach (Group group in gp.Groups)
                Assert.AreEqual(gp, group.GroupPosition);
        }

        [Test]
        public void CalculateGroupsTest6()
        {
            Position pos = new Position();
            GroupPosition gp = new GroupPosition(pos);

            pos.Play(new Move(0, 1, Color.Black));
            pos.Play(new Move(1, 1, Color.Black));
            pos.Play(new Move(1, 0, Color.Black));

            gp.CalculateGroups();

            Assert.IsNotNull(gp.Groups);
            Assert.AreEqual(1, gp.Groups.Count);
            Assert.AreEqual(gp.GetGroup(1, 0), gp.GetGroup(0, 1));
            Assert.AreEqual(gp.GetGroup(1, 0), gp.GetGroup(1, 1));
            Assert.AreEqual(gp.GetGroup(0, 1), gp.GetGroup(1, 1));

            foreach (Group group in gp.Groups)
                Assert.AreEqual(gp, group.GroupPosition);
        }

        [Test]
        public void KillGroupTest1()
        {
            Position pos = new Position();
            GroupPosition gp = new GroupPosition(pos);

            pos.Play(new Move(10, 10, Color.Black));

            gp.CalculateGroups();

            Assert.IsNotNull(gp.Groups);
            Assert.AreEqual(1, gp.Groups.Count);

            Group gr = gp.Groups[0];

            gp.KillGroup(gr);

            Assert.AreEqual(0, gp.Groups.Count); 
        }

        [Test]
        public void KillGroupTest2()
        {
            Position pos = new Position();
            GroupPosition gp = new GroupPosition(pos);

            pos.Play(new Move(10, 10, Color.Black));
            pos.Play(new Move(10, 11, Color.Black));
            pos.Play(new Move(10, 12, Color.Black));

            gp.CalculateGroups();

            Assert.IsNotNull(gp.Groups);
            Assert.AreEqual(1, gp.Groups.Count);

            Group gr = gp.Groups[0];

            gp.KillGroup(gr);

            Assert.AreEqual(0, gp.Groups.Count);
        }

        [Test]
        public void KillGroupTest3()
        {
            Position pos = new Position();
            GroupPosition gp = new GroupPosition(pos);

            pos.Play(new Move(10, 10, Color.Black));
            pos.Play(new Move(10, 11, Color.Black));
            pos.Play(new Move(10, 12, Color.Black));

            pos.Play(new Move(11, 10, Color.White));
            pos.Play(new Move(11, 11, Color.White));
            pos.Play(new Move(11, 12, Color.White));

            gp.CalculateGroups();

            Assert.IsNotNull(gp.Groups);
            Assert.AreEqual(2, gp.Groups.Count);

            Group gr = gp.Groups[0];

            gp.KillGroup(gr);

            Assert.AreEqual(1, gp.Groups.Count);

            gr = gp.Groups[0];

            Assert.AreEqual(Color.White, gr.Color);
            Assert.AreEqual(8, gr.CountLiberties);
            Assert.AreEqual(3, gr.Count);
        }

        [Test]
        public void GetGroupTest1()
        {
            Position pos = new Position();
            GroupPosition gp = new GroupPosition(pos);

            gp.CalculateGroups();

            Assert.IsNull(gp.GetGroup(-1, 0));
            Assert.IsNull(gp.GetGroup(0, -1));
            Assert.IsNull(gp.GetGroup(pos.Width, 0));
            Assert.IsNull(gp.GetGroup(0, pos.Height));
        }

        [Test]
        public void GetGroupTest2()
        {
            Position pos = new Position();
            pos.SetColor(0, 0, Color.Black);

            GroupPosition gp = new GroupPosition(pos);

            gp.CalculateGroups();

            Group gr = gp.GetGroup(0, 0);

            Assert.IsNotNull(gr);
            Assert.AreEqual(1, gr.Count);
            Assert.AreEqual(Color.Black, gr.Color);
        }

        [Test]
        public void GetGroupTest3()
        {
            Position pos = new Position();

            pos.SetColor(0, 0, Color.Black);
            pos.SetColor(0, 1, Color.Black);
            pos.SetColor(0, 2, Color.Black);

            GroupPosition gp = new GroupPosition(pos);

            gp.CalculateGroups();

            Group gr = gp.GetGroup(0, 0);

            Assert.IsNotNull(gr);
            Assert.AreEqual(3, gr.Count);
            Assert.AreEqual(Color.Black, gr.Color);

            Group gr2 = gp.GetGroup(0, 2);

            Assert.AreEqual(gr, gr2);
        }

        [Test]
        public void GetGroupTest4()
        {
            Position pos = new Position();

            pos.SetColor(10, 0, Color.Black);
            pos.SetColor(10, 1, Color.Black);
            pos.SetColor(10, 2, Color.Black);
            pos.SetColor(11, 0, Color.White);

            GroupPosition gp = new GroupPosition(pos);

            gp.CalculateGroups();

            Group gr = gp.GetGroup(10, 0);

            Assert.IsNotNull(gr);
            Assert.AreEqual(3, gr.Count);
            Assert.AreEqual(Color.Black, gr.Color);

            Group gr2 = gp.GetGroup(11, 0);

            Assert.IsNotNull(gr2);
            Assert.AreEqual(1, gr2.Count);
            Assert.AreEqual(Color.White, gr2.Color);
        }

        [Test]
        public void GroupsInPointSetTest1()
        {
            Position position = new Position();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();

            PointSet ps = new PointSet();

            ps.Add(0, 0);

            List<Group> groups = gp.GroupsInPointSet(ps);

            Assert.IsNotNull(groups);
            Assert.AreEqual(0, groups.Count);
        }

        [Test]
        public void GroupsInPointSetTest2()
        {
            Position position = new Position();
            position.SetColor(0, 0, Color.Black);
            position.SetColor(1, 1, Color.Black);

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();

            PointSet ps = new PointSet();

            ps.Add(0, 0);

            List<Group> groups = gp.GroupsInPointSet(ps);

            Assert.IsNotNull(groups);
            Assert.AreEqual(1, groups.Count);
        }

        [Test]
        public void GroupsInPointSetTest3()
        {
            Position position = new Position();
            position.SetColor(0, 0, Color.Black);
            position.SetColor(1, 1, Color.Black);

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();

            PointSet ps = new PointSet();

            ps.Add(0, 0);
            ps.Add(1, 1);

            List<Group> groups = gp.GroupsInPointSet(ps);

            Assert.IsNotNull(groups);
            Assert.AreEqual(2, groups.Count);
        }

        [Test]
        public void GroupsInPointSetTest4()
        {
            Position position = new Position();
            position.SetColor(10, 10, Color.Black);
            position.SetColor(11, 11, Color.Black);
            position.SetColor(11, 12, Color.Black);
            position.SetColor(11, 13, Color.Black);

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();

            PointSet ps = new PointSet();

            ps.Add(10, 10);
            ps.Add(11, 11);

            List<Group> groups = gp.GroupsInPointSet(ps);

            Assert.IsNotNull(groups);
            Assert.AreEqual(2, groups.Count);
        }

        [Test]
        public void NeighboursTest1()
        {
            Position position = new Position();
            position.SetColor(10, 10, Color.Black);
            position.SetColor(11, 11, Color.Black);
            position.SetColor(11, 12, Color.Black);
            position.SetColor(11, 13, Color.Black);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            List<Group> groups = gp.GetNeighbours(gp.GetGroup(11, 11));

            Assert.IsNotNull(groups);
            Assert.AreEqual(2, groups.Count);
        }

        [Test]
        public void NeighboursTest2()
        {
            Position position = new Position();
            position.SetColor(9, 9, Color.Black);
            position.SetColor(11, 11, Color.Black);
            position.SetColor(11, 12, Color.Black);
            position.SetColor(11, 13, Color.Black);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            List<Group> groups = gp.GetNeighbours(gp.GetGroup(9, 9));

            Assert.IsNotNull(groups);
            Assert.AreEqual(1, groups.Count);
        }

        [Test]
        public void NeighboursTest3()
        {
            Position position = new Position();
            position.SetColor(8, 8, Color.Black);
            position.SetColor(11, 11, Color.Black);
            position.SetColor(11, 12, Color.Black);
            position.SetColor(11, 13, Color.Black);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            List<Group> groups = gp.GetNeighbours(gp.GetGroup(8, 8));

            Assert.IsNotNull(groups);
            Assert.AreEqual(1, groups.Count);
        }

        [Test]
        public void NeighboursTest4()
        {
            Position position = new Position();
            position.SetColor(9, 9, Color.Black);
            position.SetColor(11, 11, Color.Black);
            position.SetColor(11, 12, Color.Black);
            position.SetColor(11, 13, Color.Black);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            List<Group> groups = gp.GetNeighbours(gp.GetGroup(9, 9));
            List<Group> groups2 = gp.GetNeighbours(gp.GetGroup(11, 11));

            Assert.IsNotNull(groups);
            Assert.AreEqual(1, groups.Count);

            Assert.IsNotNull(groups2);
            Assert.AreEqual(1, groups2.Count);
            Assert.AreEqual(Color.Blue, groups[0].Color);
            Assert.AreEqual(Color.Blue, groups2[0].Color);
            Assert.AreEqual(groups[0], groups2[0]);
        }

        [Test]
        public void ZoneGroupTest1()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            Group group = gp.GetZoneGroup(gp.GetGroup(0, 0));

            Assert.IsNull(group);

            group = gp.GetZoneGroup(gp.GetGroup(3, 3));

            Assert.IsNotNull(group);
            Assert.AreEqual(Color.Black, group.Color);
            Assert.AreEqual(1, group.Count);
        }

        [Test]
        public void ZoneGroupTest2()
        {
            Position position = new Position();

            position.SetColor(10, 10, Color.Black);
            position.SetColor(11, 11, Color.Black);
            position.SetColor(11, 12, Color.Black);
            position.SetColor(11, 13, Color.Black);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            Group group = gp.GetZoneGroup(gp.GetGroup(10, 10));

            Assert.IsNotNull(group);
            Assert.AreEqual(Color.Black, group.Color);
            Assert.AreEqual(4, group.Count);
        }

        [Test]
        public void ZoneGroupTest3()
        {
            Position position = new Position();

            position.SetColor(9, 9, Color.Black);
            position.SetColor(11, 11, Color.Black);
            position.SetColor(11, 12, Color.Black);
            position.SetColor(11, 13, Color.Black);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            Group group = gp.GetZoneGroup(gp.GetGroup(9, 9));

            Assert.IsNotNull(group);
            Assert.AreEqual(Color.Black, group.Color);
            Assert.AreEqual(4, group.Count);
        }

        [Test]
        public void CalculateNeighboursTest1()
        {
            Position position = new Position();
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            Assert.AreEqual(1, gp.Groups.Count);
            Assert.AreEqual(0, gp.Groups[0].Neighbours.Count);
        }

        [Test]
        public void CalculateNeighboursTest2()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            Assert.AreEqual(3, gp.Groups.Count);
            Assert.AreEqual(1, gp.GetGroup(3,3).Neighbours.Count);
            Assert.AreEqual(2, gp.GetGroup(2,2).Neighbours.Count);
            Assert.AreEqual(1, gp.GetGroup(0,0).Neighbours.Count);
        }

        [Test]
        public void CalculateNeighboursTest3()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);
            position.SetColor(4, 4, Color.Black);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            Assert.AreEqual(4, gp.Groups.Count);
            Assert.AreEqual(2, gp.GetGroup(3, 3).Neighbours.Count);
            Assert.AreEqual(2, gp.GetGroup(4, 4).Neighbours.Count);
            Assert.AreEqual(3, gp.GetGroup(2, 2).Neighbours.Count);
            Assert.AreEqual(1, gp.GetGroup(0, 0).Neighbours.Count);
        }

        [Test]
        public void CalculateNeighboursTest4()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);
            position.SetColor(3, 4, Color.Black);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            Assert.AreEqual(3, gp.Groups.Count);
            Assert.AreEqual(1, gp.GetGroup(3, 3).Neighbours.Count);
            Assert.AreEqual(1, gp.GetGroup(3, 4).Neighbours.Count);
            Assert.AreEqual(2, gp.GetGroup(2, 2).Neighbours.Count);
            Assert.AreEqual(1, gp.GetGroup(0, 0).Neighbours.Count);
        }

        [Test]
        public void CalculateNeighboursTest5()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);
            position.SetColor(4, 4, Color.White);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            Assert.AreEqual(7, gp.Groups.Count);
            Assert.AreEqual(3, gp.GetGroup(3, 3).Neighbours.Count);
            Assert.AreEqual(3, gp.GetGroup(4, 4).Neighbours.Count);
            Assert.AreEqual(4, gp.GetGroup(2, 2).Neighbours.Count);
            Assert.AreEqual(2, gp.GetGroup(0, 0).Neighbours.Count);
        }

        [Test]
        public void ZoneTest1()
        {
            Position position = new Position();
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            GroupSet zone = gp.GetZone(gp.GetGroup(0, 0));
            Assert.IsNotNull(zone);
            Assert.AreEqual(position.Size, zone.Size);
        }

        [Test]
        public void ZoneTest2()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            GroupSet zone = gp.GetZone(gp.GetGroup(3,3));

            Assert.IsNotNull(zone);
            Assert.AreEqual(3, zone.Count);
            Assert.AreEqual(position.Size, zone.Size);

            List<GroupSet> zones = gp.Zones;

            Assert.IsNotNull(zones);
            Assert.AreEqual(1, zones.Count);
        }

        [Test]
        public void ZoneTest3()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);
            position.SetColor(18 - 3, 18 - 3, Color.White);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            GroupSet zone = gp.GetZone(gp.GetGroup(3, 3));

            Assert.IsNotNull(zone);
            Assert.AreEqual(2, zone.Count);
            Assert.AreEqual(9, zone.Size);

            List<GroupSet> zones = gp.Zones;

            Assert.IsNotNull(zones);
            Assert.AreEqual(3, zones.Count);
        }
    }
}

