using System;
using System.Collections.Generic;
using System.Text;

using AjGo;

using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class MoveTests
    {
        [Test]
        public void ShouldCreate()
        {
            Move m = new Move(10, 10, Color.White);
            Assert.AreEqual(Color.White, m.Color);
            Assert.AreEqual(new Point(10, 10), m.Point);
        }
    }
}
