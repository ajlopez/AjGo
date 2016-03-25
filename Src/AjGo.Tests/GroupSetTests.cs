using System;
using System.Collections.Generic;
using System.Text;

using AjGo;

using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class GroupSetTests
    {
        [Test]
        public void CreateTest1()
        {
            GroupSet gs = new GroupSet();

            Assert.IsNotNull(gs);
            Assert.IsNotNull(gs.Groups);
            Assert.AreEqual(0, gs.Count);
        }

        [Test]
        public void AddTest1()
        {
            GroupSet gs = new GroupSet();

            gs.Add(new Group(Color.Black));
            gs.Add(new Group(Color.White));

            Assert.AreEqual(2, gs.Count);
        }

        [Test]
        public void AddTest2()
        {
            GroupSet gs = new GroupSet();

            Group group1 = new Group(Color.Black);
            Group group2 = new Group(Color.White);

            gs.Add(group1);
            gs.Add(group2);

            Assert.AreEqual(2, gs.Count);

            gs.Add(group1);
            gs.Add(group2);

            Assert.AreEqual(2, gs.Count);
        }

        [Test]
        public void RemoveTest1()
        {
            GroupSet gs = new GroupSet();

            Group group1 = new Group(Color.Black);
            Group group2 = new Group(Color.White);

            gs.Add(group1);
            gs.Add(group2);

            Assert.AreEqual(2, gs.Count);

            gs.Remove(group1);

            Assert.AreEqual(1, gs.Count);
        }

        [Test]
        public void RemoveTest2()
        {
            GroupSet gs = new GroupSet();

            Group group1 = new Group(Color.Black);
            Group group2 = new Group(Color.White);

            gs.Add(group1);
            gs.Add(group2);

            Assert.AreEqual(2, gs.Count);

            gs.Remove(new Group(Color.Black));

            Assert.AreEqual(1, gs.Count);
        }

        [Test]
        public void SizeTest1()
        {
            GroupSet gs = new GroupSet();

            Assert.IsNotNull(gs);
            Assert.AreEqual(0, gs.Size);
        }

        [Test]
        public void SizeTest2()
        {
            GroupSet gs = new GroupSet();
            gs.Add(new Group(Color.Black));
            gs.Add(new Group(Color.Black));

            Assert.AreEqual(0, gs.Size);
        }

        [Test]
        public void SizeTest3()
        {
            GroupSet gs = new GroupSet();
            Group group1 = new Group(Color.Black);
            Group group2 = new Group(Color.White);

            group1.Add(3, 3);
            group2.Add(4, 4);

            gs.Add(group1);
            gs.Add(group2);

            Assert.AreEqual(2, gs.Size);
        }

        [Test]
        public void SizeTest4()
        {
            GroupSet gs = new GroupSet();
            Group group1 = new Group(Color.Black);
            Group group2 = new Group(Color.White);

            group1.Add(3, 3);
            group1.Add(3, 4);
            group2.Add(4, 4);
            group2.Add(4, 5);

            gs.Add(group1);
            gs.Add(group2);

            Assert.AreEqual(4, gs.Size);
        }

        [Test]
        public void GetNeighboursByColorTest1()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);
            position.SetColor(18 - 3, 18 - 3, Color.White);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            GroupSet zone = gp.GetZone(gp.GetGroup(3, 3));

            GroupSet neighbours = zone.GetNeighboursByColor(Color.Green);

            Assert.IsNotNull(neighbours);
            Assert.AreEqual(1, neighbours.Count);
        }

        [Test]
        public void GetNeighboursByColorTest2()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);
            position.SetColor(3, 4, Color.Black);
            position.SetColor(18 - 3, 18 - 3, Color.White);
            position.CalculateColors();

            GroupPosition gp = new GroupPosition(position);
            gp.CalculateGroups();
            gp.CalculateNeighbours();

            GroupSet zone = gp.GetZone(gp.GetGroup(3, 3));

            GroupSet neighbours = zone.GetNeighboursByColor(Color.Blue);

            Assert.IsNotNull(neighbours);
            Assert.AreEqual(0, neighbours.Count);
        }
    }
}
