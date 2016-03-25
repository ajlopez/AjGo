using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class MatchBuilderTests
    {
        [Test]
        public void ShouldCreateMatch()
        {
            MatchBuilder mb = new MatchBuilder();

            Match match = mb.GetMatch();

            Assert.IsNotNull(match);
            Assert.AreEqual(0, match.Width);
            Assert.AreEqual(0, match.Height);
        }

        [Test]
        public void BuildMatchTest1()
        {
            MatchBuilder mb = new MatchBuilder();

            mb.MakeRow(0, "XX..OO");

            Match match = mb.GetMatch();

            Assert.AreEqual(Color.Black, match.GetColor(0, 0));
            Assert.AreEqual(Color.Black, match.GetColor(1, 0));
            Assert.AreEqual(Color.Empty,match.GetColor(2, 0));
            Assert.AreEqual(Color.Empty,match.GetColor(3, 0));
            Assert.AreEqual(Color.White, match.GetColor(4, 0));
            Assert.AreEqual(Color.White, match.GetColor(5, 0));
        }

        [Test]
        public void BuildMatchTest2()
        {
            MatchBuilder mb = new MatchBuilder();

            mb.MakeMatch("XX..OO");

            Match match = mb.GetMatch();

            Assert.AreEqual(Color.Black, match.GetColor(0, 0));
            Assert.AreEqual(Color.Black, match.GetColor(1, 0));
            Assert.AreEqual(Color.Empty,match.GetColor(2, 0));
            Assert.AreEqual(Color.Empty,match.GetColor(3, 0));
            Assert.AreEqual(Color.White, match.GetColor(4, 0));
            Assert.AreEqual(Color.White, match.GetColor(5, 0));
        }

        [Test]
        public void BuildMatchTest3()
        {
            MatchBuilder mb = new MatchBuilder();

            mb.MakeMatch("XX..OO\nXX..OO\n");

            Match match = mb.GetMatch();

            Assert.AreEqual(Color.Black, match.GetColor(0, 0));
            Assert.AreEqual(Color.Black, match.GetColor(1, 0));
            Assert.AreEqual(Color.Empty,match.GetColor(2, 0));
            Assert.AreEqual(Color.Empty,match.GetColor(3, 0));
            Assert.AreEqual(Color.White, match.GetColor(4, 0));
            Assert.AreEqual(Color.White, match.GetColor(5, 0));

            Assert.AreEqual(Color.Black, match.GetColor(0, 1));
            Assert.AreEqual(Color.Black, match.GetColor(1, 1));
            Assert.AreEqual(Color.Empty,match.GetColor(2, 1));
            Assert.AreEqual(Color.Empty,match.GetColor(3, 1));
            Assert.AreEqual(Color.White, match.GetColor(4, 1));
            Assert.AreEqual(Color.White, match.GetColor(5, 1));
        }

        [Test]
        public void BuildMatchTest4()
        {
            MatchBuilder mb = new MatchBuilder();

            mb.MakeMatch("XX..OO\nXX..OO\n..XX..OO\n");

            Match match = mb.GetMatch();

            Assert.AreEqual(Color.Black, match.GetColor(0, 0));
            Assert.AreEqual(Color.Black, match.GetColor(1, 0));
            Assert.AreEqual(Color.Empty,match.GetColor(2, 0));
            Assert.AreEqual(Color.Empty,match.GetColor(3, 0));
            Assert.AreEqual(Color.White, match.GetColor(4, 0));
            Assert.AreEqual(Color.White, match.GetColor(5, 0));

            Assert.AreEqual(Color.Black, match.GetColor(0, 1));
            Assert.AreEqual(Color.Black, match.GetColor(1, 1));
            Assert.AreEqual(Color.Empty,match.GetColor(2, 1));
            Assert.AreEqual(Color.Empty,match.GetColor(3, 1));
            Assert.AreEqual(Color.White, match.GetColor(4, 1));
            Assert.AreEqual(Color.White, match.GetColor(5, 1));

            Assert.AreEqual(Color.Black, match.GetColor(2, 2));
            Assert.AreEqual(Color.Black, match.GetColor(3, 2));
            Assert.AreEqual(Color.Empty,match.GetColor(4, 2));
            Assert.AreEqual(Color.Empty,match.GetColor(5, 2));
            Assert.AreEqual(Color.White, match.GetColor(6, 2));
            Assert.AreEqual(Color.White, match.GetColor(7, 2));
        }

        [Test]
        public void BuildMatchTest5()
        {
            MatchBuilder mb = new MatchBuilder();

            mb.MakeMatch("+-----\n|R..OO\n|.YY..BB\n");

            Match match = mb.GetMatch();

            Assert.AreEqual(Color.Border, match.GetColor(0, 0));
            Assert.AreEqual(Color.Border, match.GetColor(1, 0));
            Assert.AreEqual(Color.Border, match.GetColor(2, 0));
            Assert.AreEqual(Color.Border, match.GetColor(3, 0));
            Assert.AreEqual(Color.Border, match.GetColor(4, 0));
            Assert.AreEqual(Color.Border, match.GetColor(5, 0));

            Assert.AreEqual(Color.Border, match.GetColor(0, 1));
            Assert.AreEqual(Color.Red, match.GetColor(1, 1));
            Assert.AreEqual(Color.Empty, match.GetColor(2, 1));
            Assert.AreEqual(Color.Empty, match.GetColor(3, 1));
            Assert.AreEqual(Color.White, match.GetColor(4, 1));
            Assert.AreEqual(Color.White, match.GetColor(5, 1));

            Assert.AreEqual(Color.Border, match.GetColor(0, 2));
            Assert.AreEqual(Color.Yellow, match.GetColor(2, 2));
            Assert.AreEqual(Color.Yellow, match.GetColor(3, 2));
            Assert.AreEqual(Color.Empty, match.GetColor(4, 2));
            Assert.AreEqual(Color.Empty, match.GetColor(5, 2));
            Assert.AreEqual(Color.Blue, match.GetColor(6, 2));
            Assert.AreEqual(Color.Blue, match.GetColor(7, 2));
        }
    }
}

