using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class MatchTests
    {
        [Test]
        public void MatchTest1()
        {
            Position position = new Position();

            Match match = new Match(2, 2);

            match.SetColor(0, 0, Color.Border);
            match.SetColor(1, 0, Color.Border);
            match.SetColor(0, 1, Color.Border);
            match.SetColor(1, 1, Color.Red);

            List<MatchResult> results = match.MatchMoves(position);

            Assert.IsNotNull(results);
            Assert.AreEqual(4, results.Count);
        }

        [Test]
        public void MatchTest2()
        {
            Position position = new Position();

            Match match = new Match(3, 3);

            match.SetColor(0, 0, Color.Border);
            match.SetColor(1, 0, Color.Border);
            match.SetColor(2, 0, Color.Border);
            match.SetColor(0, 1, Color.Border);
            match.SetColor(0, 2, Color.Border);
            match.SetColor(1, 1, Color.Red);
            match.SetColor(2, 2, Color.Red);

            List<MatchResult> results = match.MatchMoves(position);

            Assert.IsNotNull(results);
            Assert.AreEqual(8, results.Count);
        }

        [Test]
        public void MatchTest3()
        {
            Position position = new Position();

            Match match = new Match(3, 3);

            match.SetColor(0, 0, Color.Border);
            match.SetColor(1, 0, Color.Border);
            match.SetColor(2, 0, Color.Border);
            match.SetColor(0, 1, Color.Border);
            match.SetColor(0, 2, Color.Border);
            match.SetColor(1, 1, Color.Black);

            List<MatchResult> results = match.MatchMoves(position);

            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void MatchTest4()
        {
            Position position = new Position();
            position.SetColor(0, 0, Color.Black);

            Match match = new Match(3, 3);

            match.SetColor(0, 0, Color.Border);
            match.SetColor(1, 0, Color.Border);
            match.SetColor(2, 0, Color.Border);
            match.SetColor(0, 1, Color.Border);
            match.SetColor(0, 2, Color.Border);
            match.SetColor(1, 1, Color.Black);
            match.SetColor(2, 2, Color.Red);

            List<MatchResult> results = match.MatchMoves(position);

            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void MatchTest5()
        {
            Position position = new Position();
            position.SetColor(18, 18, Color.Black);

            Match match = new Match(3, 3);

            match.SetColor(0, 0, Color.Border);
            match.SetColor(1, 0, Color.Border);
            match.SetColor(2, 0, Color.Border);
            match.SetColor(0, 1, Color.Border);
            match.SetColor(0, 2, Color.Border);
            match.SetColor(1, 1, Color.Blue);
            match.SetColor(2, 2, Color.Red);

            List<MatchResult> results = match.MatchMoves(position);

            Assert.IsNotNull(results);
            Assert.AreEqual(4, results.Count);
        }

        [Test]
        public void MatchTest6()
        {
            Position position = new Position();
            position.SetColor(0, 0, Color.Black);
            position.SetColor(18, 18, Color.Black);

            Match match = new Match(3, 3);

            match.SetColor(0, 0, Color.Border);
            match.SetColor(1, 0, Color.Border);
            match.SetColor(2, 0, Color.Border);
            match.SetColor(0, 1, Color.Border);
            match.SetColor(0, 2, Color.Border);
            match.SetColor(1, 1, Color.Black);
            match.SetColor(2, 2, Color.Red);

            List<MatchResult> results = match.MatchMoves(position);

            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public void MatchTest7()
        {
            Position position = new Position();
            position.SetColor(0, 0, Color.Black);
            position.SetColor(18, 18, Color.Black);

            Match match = new Match(3, 3);

            match.SetColor(0, 0, Color.Border);
            match.SetColor(1, 0, Color.Border);
            match.SetColor(2, 0, Color.Border);
            match.SetColor(0, 1, Color.Border);
            match.SetColor(0, 2, Color.Border);
            match.SetColor(1, 1, Color.Blue);
            match.SetColor(2, 2, Color.Red);

            List<MatchResult> results = match.MatchMoves(position);

            Assert.IsNotNull(results);
            Assert.AreEqual(4, results.Count);
        }

        [Test]
        public void MatchTest8()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);

            Match match = new Match(2, 1);

            match.SetColor(0, 0, Color.Black);
            match.SetColor(1, 0, Color.Red);

            List<MatchResult> results = match.MatchMoves(position);

            Assert.IsNotNull(results);
            Assert.AreEqual(4, results.Count);
        }

        [Test]
        public void MatchTest9()
        {
            Position position = new Position();
            position.SetColor(3, 3, Color.Black);

            Match match = new Match(5, 1);

            match.SetColor(0, 0, Color.Border);
            match.SetColor(3, 0, Color.Red);
            match.SetColor(4, 0, Color.Black);

            List<MatchResult> results = match.MatchMoves(position);

            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public void MatchTest10()
        {
            Position position = new Position();
            position.SetColor(5, 3, Color.Black);
            position.SetColor(6, 3, Color.Black);
            position.SetColor(7, 3, Color.Black);

            Match match = new Match(5, 1);

            match.SetColor(0, 0, Color.Border);
            match.SetColor(3, 0, Color.Red);
            match.SetColor(4, 0, Color.Black);

            List<MatchResult> results = match.MatchMoves(position);

            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
        }

        [Test]
        public void MatchTest11()
        {
            Position position = new Position();
            position.SetColor(5, 5, Color.White);
            position.SetColor(6, 6, Color.White);
            position.SetColor(5, 6, Color.Black);

            Match match = new Match(2, 2);

            match.SetColor(0, 0, Color.Red);
            match.SetColor(1, 0, Color.White);
            match.SetColor(0, 1, Color.White);
            match.SetColor(1, 1, Color.Black);

            List<MatchResult> results = match.MatchMoves(position);

            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
        }
    }
}

