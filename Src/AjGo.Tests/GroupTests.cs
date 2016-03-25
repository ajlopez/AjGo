using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class GroupTests
    {
        [Test]
        public void ShouldCreate()
        {
            Group gr = new Group(Color.Black);

            Assert.IsNotNull(gr);
            Assert.AreEqual(0, gr.Count);
            Assert.AreEqual(0, gr.CountLiberties);
        }

        [Test]
        public void TestAdd1()
        {
            Group gr = new Group(Color.Black);

            gr.Add(new Point(10, 10));

            Assert.AreEqual(1, gr.Count);
            Assert.AreEqual(0, gr.CountLiberties);
        }

        [Test]
        public void TestAdd2()
        {
            Group gr = new Group(Color.Black);

            gr.Add(new Point(10, 10));
            gr.Add(new Point(10, 12));

            Assert.AreEqual(2, gr.Count);
            Assert.AreEqual(0, gr.CountLiberties);
        }

        [Test]
        public void TestAdd3()
        {
            Group gr = new Group(Color.Black);

            gr.Add(new Point(10, 10));
            gr.Add(new Point(10, 10));

            Assert.AreEqual(1, gr.Count);
            Assert.AreEqual(0, gr.CountLiberties);
        }

        [Test]
        public void TestAdd1b()
        {
            Group ps = new Group(Color.Black);

            ps.Add(10, 10);

            Assert.AreEqual(1, ps.Count);
        }

        [Test]
        public void TestAdd2b()
        {
            Group gr = new Group(Color.Black);

            gr.Add(10, 10);
            gr.Add(10, 12);

            Assert.AreEqual(2, gr.Count);
            Assert.AreEqual(0, gr.CountLiberties);
        }

        [Test]
        public void TestAdd3b()
        {
            Group gr = new Group(Color.Black);

            gr.Add(10, 10);
            gr.Add(10, 10);

            Assert.AreEqual(1, gr.Count);
            Assert.AreEqual(0, gr.CountLiberties);
        }

        [Test]
        public void TestRemove1()
        {
            Group gr = new Group(Color.Black);

            gr.Add(new Point(10, 10));
            gr.Remove(new Point(10, 10));

            Assert.AreEqual(0, gr.Count);
            Assert.AreEqual(0, gr.CountLiberties);
        }

        [Test]
        public void TestRemove2()
        {
            Group gr = new Group(Color.Black);

            gr.Add(new Point(8, 8));
            gr.Add(new Point(10, 10));
            gr.Add(new Point(12, 15));

            gr.Remove(new Point(10, 10));

            Assert.AreEqual(2, gr.Count);
        }

        [Test]
        public void TestMerge1()
        {
            Group gr1 = new Group(Color.Black);
            Group gr2 = new Group(Color.Black);

            gr1.Merge(gr2);

            Assert.AreEqual(0, gr1.Count);
            Assert.AreEqual(0, gr2.Count);
            Assert.AreEqual(0, gr1.CountLiberties);
            Assert.AreEqual(0, gr2.CountLiberties);
        }

        [Test]
        public void TestMerge2()
        {
            Group gr1 = new Group(Color.Black);
            Group gr2 = new Group(Color.Black);

            gr1.Add(new Point(10, 10));
            gr2.Add(new Point(5, 5));

            gr1.Merge(gr2);

            Assert.AreEqual(2, gr1.Count);
            Assert.AreEqual(1, gr2.Count);
            Assert.AreEqual(0, gr1.CountLiberties);
            Assert.AreEqual(0, gr2.CountLiberties);
        }

        [Test]
        public void TestMerge3()
        {
            Group gr1 = new Group(Color.Black);
            Group gr2 = new Group(Color.Black);

            gr1.Add(new Point(10, 10));
            gr1.Add(new Point(11, 11));
            gr1.Add(new Point(14, 3));

            gr2.Add(new Point(5, 5));
            gr2.Add(new Point(1, 2));
            gr2.Add(new Point(4, 7));

            gr1.Merge(gr2);

            Assert.AreEqual(6, gr1.Count);
            Assert.AreEqual(3, gr2.Count);
            Assert.AreEqual(0, gr1.CountLiberties);
            Assert.AreEqual(0, gr2.CountLiberties);
        }

        [Test]
        public void TestMerge4()
        {
            Group gr1 = new Group(Color.Black);
            Group gr2 = new Group(Color.Black);

            gr1.Add(new Point(10, 10));
            gr1.Add(new Point(11, 11));
            gr1.Add(new Point(14, 3));
            gr1.AddLiberty(9, 10);

            gr2.Add(new Point(5, 5));
            gr2.Add(new Point(1, 2));
            gr2.Add(new Point(4, 7));
            gr2.Add(new Point(11, 11));
            gr2.Add(new Point(14, 3));
            gr2.AddLiberty(9, 11);

            gr1.Merge(gr2);

            Assert.AreEqual(6, gr1.Count);
            Assert.AreEqual(5, gr2.Count);
            Assert.AreEqual(2, gr1.CountLiberties);
            Assert.AreEqual(1, gr2.CountLiberties);
        }

        [Test]
        public void TestMerge5()
        {
            Group gr1 = new Group(Color.Black);

            gr1.Add(new Point(10, 10));
            gr1.Add(new Point(11, 11));
            gr1.Add(new Point(14, 3));
            gr1.AddLiberty(9, 10);

            gr1.Merge(gr1);

            Assert.AreEqual(3, gr1.Count);
            Assert.AreEqual(1, gr1.CountLiberties);
        }

        [Test]
        public void TestClone1()
        {
            Group gr = new Group(Color.Black);

            Group gr2 = gr.Clone();

            Assert.AreEqual(0, gr.Count);
            Assert.AreEqual(0, gr2.Count);

            Assert.AreEqual(0, gr.CountLiberties);
            Assert.AreEqual(0, gr2.CountLiberties);

            Assert.AreEqual(Color.Black, gr2.Color);
        }

        [Test]
        public void TestClone2()
        {
            Group gr = new Group(Color.Black);

            gr.Add(new Point(1, 1));
            gr.Add(new Point(2, 17));
            gr.Add(new Point(3, 4));

            Group gr2 = gr.Clone();

            Assert.AreEqual(3, gr.Count);
            Assert.AreEqual(3, gr2.Count);

            Assert.AreEqual(0, gr.CountLiberties);
            Assert.AreEqual(0, gr2.CountLiberties);
        }

        [Test]
        public void TestClone3()
        {
            Group gr = new Group(Color.Black);

            gr.Add(new Point(11, 12));
            gr.Add(new Point(12, 17));
            gr.Add(new Point(13, 4));

            Group gr2 = gr.Clone();

            Assert.AreEqual(3, gr.Count);
            Assert.AreEqual(3, gr2.Count);
            Assert.AreEqual(0, gr.CountLiberties);
            Assert.AreEqual(0, gr2.CountLiberties);

            gr2.Merge(gr);

            Assert.AreEqual(3, gr.Count);
            Assert.AreEqual(3, gr2.Count);
            Assert.AreEqual(0, gr.CountLiberties);
            Assert.AreEqual(0, gr2.CountLiberties);
        }

        [Test]
        public void TestCalculateLiberties1()
        {
            Group gr = new Group(Color.Black);
            gr.Add(10, 10);

            Position p = new Position();
            p.Play(new Move(10, 10, Color.Black));

            Assert.AreEqual(4, gr.CalculateLiberties(p).Count);
        }

        public void TestCalculateLiberties2()
        {
            Group gr = new Group(Color.Black);
            gr.Add(10, 10);

            Position p = new Position();
            p.Play(new Move(10, 10, Color.Black));
            p.CalculateColors();

            Assert.AreEqual(4, gr.CalculateLiberties(p).Count);
        }

        [Test]
        public void TestCalculateFrontier1()
        {
            Group gr = new Group(Color.Black);
            gr.Add(10, 10);

            Position p = new Position();
            p.Play(new Move(10, 10, Color.Black));

            Assert.AreEqual(8, gr.CalculateFrontier(p).Count);
        }

        [Test]
        public void NeighboursTest1()
        {
            Group gr = new Group(Color.Black);

            Assert.IsNotNull(gr.Neighbours);
            Assert.AreEqual(0, gr.Neighbours.Count);
        }

        [Test]
        public void EqualsTest1()
        {
            Group gr1 = new Group(Color.White);
            Group gr2 = new Group(Color.White);

            Assert.IsTrue(gr1.Equals(gr2));
        }

        [Test]
        public void EqualsTest2()
        {
            Group gr1 = new Group(Color.White);
            Group gr2 = new Group(Color.Black);

            Assert.IsFalse(gr1.Equals(gr2));
        }

        [Test]
        public void EqualsTest3()
        {
            Group gr1 = new Group(Color.Black);
            gr1.Add(10, 10);
            gr1.Add(11, 11);

            Group gr2 = new Group(Color.Black);
            gr2.Add(10, 10);
            gr2.Add(11, 11);

            Assert.IsTrue(gr1.Equals(gr2));
        }

        [Test]
        public void EqualsTest4()
        {
            Group gr1 = new Group(Color.White);
            gr1.Add(10, 10);
            gr1.Add(11, 11);

            Group gr2 = new Group(Color.White);
            gr2.Add(11, 11);
            gr2.Add(10, 10);

            Assert.IsFalse(gr1.Equals(gr2));
        }
    }
}
