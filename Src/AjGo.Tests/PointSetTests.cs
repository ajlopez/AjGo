using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class PointSetTests
    {
        [Test]
        public void ShouldCreate()
        {
            PointSet ps = new PointSet();

            Assert.IsNotNull(ps);
            Assert.AreEqual(0, ps.Count);
        }

        [Test]
        public void NewTest1()
        {
            PointSet ps = new PointSet();

            ps.Add(3, 3);
            ps.Add(17, 3);
            ps.Add(10, 4);

            PointSet ps2 = new PointSet(ps);

            Assert.AreEqual(3, ps2.Count);
        }

        [Test]
        public void NewTest2()
        {
            PointSet ps = new PointSet();

            ps.Add(3, 3);
            ps.Add(17, 3);
            ps.Add(10, 4);

            PointSet ps2 = new PointSet(ps);

            ps2.Add(1, 2);
            ps2.Add(3, 4);

            PointSet ps3 = new PointSet(ps, ps2);

            Assert.AreEqual(5, ps3.Count);
        }

        [Test]
        public void TestAdd1()
        {
            PointSet ps = new PointSet();

            ps.Add(new Point(10, 10));

            Assert.AreEqual(1, ps.Count);
        }

        [Test]
        public void TestAdd2()
        {
            PointSet ps = new PointSet();

            ps.Add(new Point(10, 10));
            ps.Add(new Point(10, 12));

            Assert.AreEqual(2, ps.Count);
        }

        [Test]
        public void TestAdd3()
        {
            PointSet ps = new PointSet();

            ps.Add(new Point(10, 10));
            ps.Add(new Point(10, 10));

            Assert.AreEqual(1, ps.Count);
        }

        [Test]
        public void TestAdd1b()
        {
            PointSet ps = new PointSet();

            ps.Add(10,10);

            Assert.AreEqual(1, ps.Count);
        }

        [Test]
        public void TestAdd2b()
        {
            PointSet ps = new PointSet();

            ps.Add(10,10);
            ps.Add(10,12);

            Assert.AreEqual(2, ps.Count);
        }

        [Test]
        public void TestAdd3b()
        {
            PointSet ps = new PointSet();

            ps.Add(10,10);
            ps.Add(10,10);

            Assert.AreEqual(1, ps.Count);
        }

        [Test]
        public void TestRemove1()
        {
            PointSet ps = new PointSet();

            ps.Add(new Point(10, 10));
            ps.Remove(new Point(10, 10));

            Assert.AreEqual(0, ps.Count);
        }

        [Test]
        public void TestRemove2()
        {
            PointSet ps = new PointSet();

            ps.Add(new Point(8, 8));
            ps.Add(new Point(10, 10));
            ps.Add(new Point(12, 15));

            ps.Remove(new Point(10, 10));

            Assert.AreEqual(2, ps.Count);
        }

        [Test]
        public void TestRemove3()
        {
            PointSet ps = new PointSet();

            ps.Add(new Point(8, 8));
            ps.Add(new Point(10, 10));
            ps.Add(new Point(12, 15));

            ps.Remove(new Point(13, 1));

            Assert.AreEqual(3, ps.Count);
        }

        [Test]
        public void TestMerge1()
        {
            PointSet ps1 = new PointSet();
            PointSet ps2 = new PointSet();

            ps1.Add(ps2);

            Assert.AreEqual(0, ps1.Count);
            Assert.AreEqual(0, ps2.Count);
        }

        [Test]
        public void TestMerge2()
        {
            PointSet ps1 = new PointSet();
            PointSet ps2 = new PointSet();

            ps1.Add(new Point(10, 10));
            ps2.Add(new Point(5, 5));

            ps1.Add(ps2);

            Assert.AreEqual(2, ps1.Count);
            Assert.AreEqual(1, ps2.Count);
        }

        [Test]
        public void TestMerge3()
        {
            PointSet ps1 = new PointSet();
            PointSet ps2 = new PointSet();

            ps1.Add(new Point(10, 10));
            ps1.Add(new Point(11, 11));
            ps1.Add(new Point(14, 3));

            ps2.Add(new Point(5, 5));
            ps2.Add(new Point(1, 2));
            ps2.Add(new Point(4, 7));

            ps1.Add(ps2);

            Assert.AreEqual(6, ps1.Count);
            Assert.AreEqual(3, ps2.Count);
        }

        [Test]
        public void TestMerge4()
        {
            PointSet ps1 = new PointSet();
            PointSet ps2 = new PointSet();

            ps1.Add(new Point(10, 10));
            ps1.Add(new Point(11, 11));
            ps1.Add(new Point(14, 3));

            ps2.Add(new Point(5, 5));
            ps2.Add(new Point(1, 2));
            ps2.Add(new Point(4, 7));
            ps2.Add(new Point(11, 11));
            ps2.Add(new Point(14, 3));

            ps1.Add(ps2);

            Assert.AreEqual(6, ps1.Count);
            Assert.AreEqual(5, ps2.Count);
        }

        [Test]
        public void TestMerge5()
        {
            PointSet ps1 = new PointSet();

            ps1.Add(new Point(10, 10));
            ps1.Add(new Point(11, 11));
            ps1.Add(new Point(14, 3));

            ps1.Add(ps1);

            Assert.AreEqual(3, ps1.Count);
        }

        [Test]
        public void TestSubtract1()
        {
            PointSet ps = new PointSet();
            ps.Add(3, 3);

            PointSet ps2 = new PointSet();
            ps2.Add(4, 4);

            ps.Subtract(ps2);

            Assert.AreEqual(1, ps.Count);
        }

        [Test]
        public void TestSubtract2()
        {
            PointSet ps = new PointSet();
            ps.Add(3, 3);
            ps.Add(4, 4);
            ps.Add(5, 5);

            PointSet ps2 = new PointSet();
            ps2.Add(4, 4);

            ps.Subtract(ps2);

            Assert.AreEqual(2, ps.Count);
        }

        [Test]
        public void TestSubtract3()
        {
            PointSet ps = new PointSet();
            ps.Add(3, 3);
            ps.Add(4, 4);
            ps.Add(5, 5);
            ps.Add(6, 6);

            PointSet ps2 = ps.Clone();

            ps.Subtract(ps2);

            Assert.AreEqual(0, ps.Count);
        }

        [Test]
        public void TestClone1()
        {
            PointSet ps = new PointSet();
            PointSet ps2 = ps.Clone();

            Assert.AreEqual(0, ps.Count);
            Assert.AreEqual(0, ps2.Count);
        }

        [Test]
        public void TestClone2()
        {
            PointSet ps = new PointSet();

            ps.Add(new Point(1, 1));
            ps.Add(new Point(2, 17));
            ps.Add(new Point(3, 4));

            PointSet ps2 = ps.Clone();

            Assert.AreEqual(3, ps.Count);
            Assert.AreEqual(3, ps2.Count);
        }

        [Test]
        public void TestClone3()
        {
            PointSet ps = new PointSet();

            ps.Add(new Point(11, 12));
            ps.Add(new Point(12, 17));
            ps.Add(new Point(13, 4));

            PointSet ps2 = ps.Clone();

            Assert.AreEqual(3, ps.Count);
            Assert.AreEqual(3, ps2.Count);

            ps2.Add(ps);

            Assert.AreEqual(3, ps.Count);
            Assert.AreEqual(3, ps2.Count);
        }

        [Test]
        public void TestLiberties1()
        {
            Position pos = new Position();
            PointSet ps = new PointSet();
            ps.Add(new Point(10, 10));

            Assert.AreEqual(4, ps.CalculateLiberties(pos).Count);
        }

        [Test]
        public void TestLiberties2()
        {
            Position pos = new Position();
            PointSet ps = new PointSet();

            ps.Add(new Point(0, 0));

            Assert.AreEqual(2, ps.CalculateLiberties(pos).Count);
        }

        [Test]
        public void TestLiberties3()
        {
            Position pos = new Position();
            PointSet ps = new PointSet();

            ps.Add(new Point((short) (pos.Width-1), (short) (pos.Height-1)));

            Assert.AreEqual(2, ps.CalculateLiberties(pos).Count);
        }

        [Test]
        public void TestLiberties4()
        {
            Position pos = new Position();
            PointSet ps = new PointSet();

            pos.Play(new Move(10, 10, Color.Black));
            pos.Play(new Move(10, 11, Color.Black));

            ps.Add(new Point(10, 10));
            ps.Add(new Point(10, 11));

            Assert.AreEqual(6, ps.CalculateLiberties(pos).Count);
        }

        [Test]
        public void TestLiberties5()
        {
            Position pos = new Position();
            PointSet ps = new PointSet();

            pos.Play(new Move(10, 10, Color.Black));
            pos.Play(new Move(10, 11, Color.Black));
            pos.Play(new Move(10, 12, Color.White));

            ps.Add(new Point(10, 10));
            ps.Add(new Point(10, 11));

            Assert.AreEqual(5, ps.CalculateLiberties(pos).Count);
        }

        [Test]
        public void TestLiberties6()
        {
            Position pos = new Position();
            PointSet ps = new PointSet();

            pos.Play(new Move(10, 10, Color.Black));
            pos.Play(new Move(10, 11, Color.White));
            pos.Play(new Move(11, 10, Color.White));
            pos.Play(new Move(9, 10, Color.White));
            pos.Play(new Move(10, 9, Color.White));

            ps.Add(new Point(10, 10));

            Assert.AreEqual(0, ps.CalculateLiberties(pos).Count);
        }

        [Test]
        public void TestFrontier1()
        {
            Position pos = new Position();
            PointSet ps = new PointSet();
            ps.Add(new Point(10, 10));

            Assert.AreEqual(8, ps.CalculateFrontier(pos).Count);
        }

        [Test]
        public void TestFrontier2()
        {
            Position pos = new Position();
            PointSet ps = new PointSet();

            ps.Add(new Point(0, 0));

            Assert.AreEqual(3, ps.CalculateFrontier(pos).Count);
        }

        [Test]
        public void TestFrontier3()
        {
            Position pos = new Position();
            PointSet ps = new PointSet();

            ps.Add(new Point((short)(pos.Width - 1), (short)(pos.Height - 1)));

            Assert.AreEqual(3, ps.CalculateFrontier(pos).Count);
        }

        [Test]
        public void TestFrontier4()
        {
            Position pos = new Position();
            PointSet ps = new PointSet();

            pos.Play(new Move(10, 10, Color.Black));
            pos.Play(new Move(10, 11, Color.Black));

            ps.Add(new Point(10, 10));
            ps.Add(new Point(10, 11));

            Assert.AreEqual(10, ps.CalculateFrontier(pos).Count);
        }

        [Test]
        public void TestFrontier5()
        {
            Position pos = new Position();
            PointSet ps = new PointSet();

            pos.Play(new Move(10, 10, Color.Black));
            pos.Play(new Move(10, 11, Color.Black));
            pos.Play(new Move(10, 12, Color.White));

            ps.Add(new Point(10, 10));
            ps.Add(new Point(10, 11));

            Assert.AreEqual(10, ps.CalculateFrontier(pos).Count);
        }

        [Test]
        public void TestFrontier6()
        {
            Position pos = new Position();
            PointSet ps = new PointSet();

            pos.Play(new Move(10, 10, Color.Black));
            pos.Play(new Move(10, 11, Color.White));
            pos.Play(new Move(11, 10, Color.White));
            pos.Play(new Move(9, 10, Color.White));
            pos.Play(new Move(10, 9, Color.White));

            ps.Add(new Point(10, 10));

            Assert.AreEqual(8, ps.CalculateFrontier(pos).Count);
        }

        [Test]
        public void UnionTest1()
        {
            PointSet ps = PointSet.Union(new PointSet(), new PointSet());

            Assert.IsNotNull(ps);
            Assert.AreEqual(0, ps.Count);
        }

        [Test]
        public void UnionTest2()
        {
            PointSet ps1 = new PointSet();
            PointSet ps2 = new PointSet();

            ps1.Add(3, 3);
            ps2.Add(4, 4);

            PointSet ps = PointSet.Union(ps1, ps2);

            Assert.IsNotNull(ps);
            Assert.AreEqual(2, ps.Count);
        }

        [Test]
        public void UnionTest3()
        {
            PointSet ps1 = new PointSet();
            PointSet ps2 = new PointSet();

            ps1.Add(3, 3);
            ps2.Add(3, 3);

            PointSet ps = PointSet.Union(ps1, ps2);

            Assert.IsNotNull(ps);
            Assert.AreEqual(1, ps.Count);
        }


        [Test]
        public void DifferenceTest1()
        {
            PointSet ps1 = new PointSet();
            PointSet ps2 = new PointSet();

            PointSet ps = PointSet.Difference(ps1, ps2);

            Assert.IsNotNull(ps);
            Assert.AreEqual(0, ps.Count);
        }

        [Test]
        public void DifferenceTest2()
        {
            PointSet ps1 = new PointSet();
            PointSet ps2 = new PointSet();

            ps1.Add(3, 3);
            ps2.Add(4, 4);

            PointSet ps = PointSet.Difference(ps1, ps2);

            Assert.IsNotNull(ps);
            Assert.AreEqual(1, ps.Count);
        }

        [Test]
        public void DifferenceTest3()
        {
            PointSet ps1 = new PointSet();
            ps1.Add(3, 3);
            ps1.Add(3, 4);
            ps1.Add(17, 10);

            PointSet ps2 = ps1.Clone();
            ps2.Add(12, 12);

            PointSet ps = PointSet.Difference(ps1, ps2);

            Assert.IsNotNull(ps);
            Assert.AreEqual(0, ps.Count);
        }

        [Test]
        public void IntersectTest1()
        {
            PointSet ps = PointSet.Intersect(new PointSet(), new PointSet());

            Assert.IsNotNull(ps);
            Assert.AreEqual(0, ps.Count);
        }

        [Test]
        public void IntersectTest2()
        {
            PointSet ps1 = new PointSet();
            PointSet ps2 = new PointSet();

            ps1.Add(3, 3);
            ps1.Add(4, 4);
            ps1.Add(5, 5);

            ps2.Add(4, 4);
            ps2.Add(5, 6);
            ps2.Add(6, 6);

            PointSet ps = PointSet.Intersect(ps1, ps2);

            Assert.IsNotNull(ps);
            Assert.AreEqual(1, ps.Count);
        }

        [Test]
        public void IntersectTest3()
        {
            PointSet ps1 = new PointSet();

            ps1.Add(3, 3);
            ps1.Add(4, 4);
            ps1.Add(5, 5);

            PointSet ps2 = ps1.Clone();

            PointSet ps = PointSet.Intersect(ps1, ps2);

            Assert.IsNotNull(ps);
            Assert.AreEqual(3, ps.Count);
        }

        [Test]
        public void EqualsTest1()
        {
            PointSet ps = new PointSet();
            PointSet ps2 = new PointSet();

            Assert.IsTrue(ps.Equals(ps2));
        }

        [Test]
        public void EqualsTest2()
        {
            PointSet ps = new PointSet();
            PointSet ps2 = new PointSet();
            ps.Add(10, 10);

            Assert.IsFalse(ps.Equals(ps2));
        }

        [Test]
        public void EqualsTest3()
        {
            PointSet ps = new PointSet();
            PointSet ps2 = new PointSet();
            ps.Add(10, 10);
            ps2.Add(10, 10);

            Assert.IsTrue(ps.Equals(ps2));
        }

        [Test]
        public void EqualsTest4()
        {
            PointSet ps = new PointSet();
            PointSet ps2 = new PointSet();
            ps.Add(10, 10);
            ps.Add(11, 11);
            ps2.Add(11, 11);
            ps2.Add(10, 10);

            Assert.IsFalse(ps.Equals(ps2));
        }

        [Test]
        public void EqualsTest5()
        {
            PointSet ps = new PointSet();

            for (short k = 1; k < 10; k++)
                ps.Add(k, k);

            PointSet ps2 = ps.Clone();

            Assert.IsTrue(ps.Equals(ps2));
        }
    }
}

