using System;
using System.Collections.Generic;
using System.Text;

using AjGo;

using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class PointTests
    {
        [Test]
        public void ShouldCreate()
        {
            Point p = new Point(0,0);
            Assert.IsNotNull(p);
            Assert.AreEqual(0, p.X);
            Assert.AreEqual(0, p.Y);
        }

        [Test]
        public void ShouldBeEquals()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(10, 10);
            Assert.AreEqual(p1, p2);
        }

        [Test]
        public void ShouldBeNotEquals()
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(11, 10);
            Assert.AreNotEqual(p1, p2);
        }     
    }
}

