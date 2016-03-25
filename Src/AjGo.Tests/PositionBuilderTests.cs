using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class PositionBuilderTests
    {
        [Test]
        public void ShouldCreatePosition()
        {
            PositionBuilder pb = new PositionBuilder();

            Position pos = pb.GetPosition();

            Assert.IsNotNull(pos);
            Assert.AreEqual(19, pos.Width);
            Assert.AreEqual(19, pos.Height);

            Assert.IsTrue(pos.IsEmpty(0, 0));
            Assert.IsTrue(pos.IsEmpty(18, 18));

            Assert.AreEqual(0, pos.CountColor(Color.Black));
            Assert.AreEqual(0, pos.CountColor(Color.White));
        }

        [Test]
        public void BuildPositionTest1()
        {
            PositionBuilder pb = new PositionBuilder();

            pb.MakeRow(0, "XX..OO");

            Position pos = pb.GetPosition();

            Assert.AreEqual(Color.Black, pos.GetColor(0, 0));
            Assert.AreEqual(Color.Black, pos.GetColor(1, 0));
            Assert.IsTrue(pos.IsEmpty(2, 0));
            Assert.IsTrue(pos.IsEmpty(3, 0));
            Assert.AreEqual(Color.White, pos.GetColor(4, 0));
            Assert.AreEqual(Color.White, pos.GetColor(5, 0));

            Assert.AreEqual(2, pos.CountColor(Color.Black));
            Assert.AreEqual(2, pos.CountColor(Color.White));
        }

        [Test]
        public void BuildPositionTest2()
        {
            PositionBuilder pb = new PositionBuilder();

            pb.MakePosition("XX..OO");

            Position pos = pb.GetPosition();

            Assert.AreEqual(Color.Black, pos.GetColor(0, 0));
            Assert.AreEqual(Color.Black, pos.GetColor(1, 0));
            Assert.IsTrue(pos.IsEmpty(2, 0));
            Assert.IsTrue(pos.IsEmpty(3, 0));
            Assert.AreEqual(Color.White, pos.GetColor(4, 0));
            Assert.AreEqual(Color.White, pos.GetColor(5, 0));

            Assert.AreEqual(2, pos.CountColor(Color.Black));
            Assert.AreEqual(2, pos.CountColor(Color.White));
        }

        [Test]
        public void BuildPositionTest3()
        {
            PositionBuilder pb = new PositionBuilder();

            pb.MakePosition("XX..OO\nXX..OO\n");

            Position pos = pb.GetPosition();

            Assert.AreEqual(Color.Black, pos.GetColor(0, 0));
            Assert.AreEqual(Color.Black, pos.GetColor(1, 0));
            Assert.IsTrue(pos.IsEmpty(2, 0));
            Assert.IsTrue(pos.IsEmpty(3, 0));
            Assert.AreEqual(Color.White, pos.GetColor(4, 0));
            Assert.AreEqual(Color.White, pos.GetColor(5, 0));

            Assert.AreEqual(Color.Black, pos.GetColor(0, 1));
            Assert.AreEqual(Color.Black, pos.GetColor(1, 1));
            Assert.IsTrue(pos.IsEmpty(2, 1));
            Assert.IsTrue(pos.IsEmpty(3, 1));
            Assert.AreEqual(Color.White, pos.GetColor(4, 1));
            Assert.AreEqual(Color.White, pos.GetColor(5, 1));

            Assert.AreEqual(4, pos.CountColor(Color.Black));
            Assert.AreEqual(4, pos.CountColor(Color.White));
        }

        [Test]
        public void BuildPositionTest4()
        {
            PositionBuilder pb = new PositionBuilder();

            pb.MakePosition("XX..OO\nXX..OO\n..XX..OO\n");

            Position pos = pb.GetPosition();

            Assert.AreEqual(Color.Black, pos.GetColor(0, 0));
            Assert.AreEqual(Color.Black, pos.GetColor(1, 0));
            Assert.IsTrue(pos.IsEmpty(2, 0));
            Assert.IsTrue(pos.IsEmpty(3, 0));
            Assert.AreEqual(Color.White, pos.GetColor(4, 0));
            Assert.AreEqual(Color.White, pos.GetColor(5, 0));

            Assert.AreEqual(Color.Black, pos.GetColor(0, 1));
            Assert.AreEqual(Color.Black, pos.GetColor(1, 1));
            Assert.IsTrue(pos.IsEmpty(2, 1));
            Assert.IsTrue(pos.IsEmpty(3, 1));
            Assert.AreEqual(Color.White, pos.GetColor(4, 1));
            Assert.AreEqual(Color.White, pos.GetColor(5, 1));

            Assert.AreEqual(Color.Black, pos.GetColor(2, 2));
            Assert.AreEqual(Color.Black, pos.GetColor(3, 2));
            Assert.IsTrue(pos.IsEmpty(4, 2));
            Assert.IsTrue(pos.IsEmpty(5, 2));
            Assert.AreEqual(Color.White, pos.GetColor(6, 2));
            Assert.AreEqual(Color.White, pos.GetColor(7, 2));

            Assert.AreEqual(6, pos.CountColor(Color.Black));
            Assert.AreEqual(6, pos.CountColor(Color.White));
        }
    }
}

