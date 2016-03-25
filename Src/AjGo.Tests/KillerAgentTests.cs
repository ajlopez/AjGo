using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using AjGo.Agents;

using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class KillerAgentTests
    {
        [Test]
        public void ShouldCreate()
        {
            Game game = new Game();

            game.Play(0,0,Color.Black);

            KillerAgent agent = new KillerAgent(game,0,0);

            Assert.IsNotNull(agent);

            List<Move> moves = agent.Process();

            Assert.IsNotNull(agent);
            Assert.AreEqual(0, moves.Count);
        }

        [Test]
        public void KillTest1()
        {
            Game game = new Game();

            game.Play(0, 0, Color.Black);
            game.Play(1, 0, Color.White);

            KillerAgent agent = new KillerAgent(game, 0, 0);

            Assert.IsNotNull(agent);

            List<Move> moves = agent.Process();

            Assert.IsNotNull(agent);
            Assert.AreEqual(1, moves.Count);
            Assert.AreEqual(0, moves[0].Point.X);
            Assert.AreEqual(1, moves[0].Point.Y);
            Assert.AreEqual(Color.White, moves[0].Color);
        }

        [Test]
        public void KillTest2()
        {
            Game game = new Game();

            game.Play(0, 1, Color.Black);
            game.Play(0, 2, Color.White);
            game.Play(1, 1, Color.White);

            KillerAgent agent = new KillerAgent(game, 0, 1);

            Assert.IsNotNull(agent);

            List<Move> moves = agent.Process();

            Assert.IsNotNull(agent);
            Assert.AreEqual(1, moves.Count);
            Assert.AreEqual(0, moves[0].Point.X);
            Assert.AreEqual(0, moves[0].Point.Y);
            Assert.AreEqual(Color.White, moves[0].Color);
        }

        [Test]
        public void KillTest3()
        {
            Game game = new Game();

            game.Play(0, 1, Color.Black);
            game.Play(0, 2, Color.Black);
            game.Play(0, 3, Color.White);
            game.Play(1, 1, Color.White);
            game.Play(1, 2, Color.White);

            KillerAgent agent = new KillerAgent(game, 0, 2);

            Assert.IsNotNull(agent);

            List<Move> moves = agent.Process();

            Assert.IsNotNull(agent);
            Assert.AreEqual(1, moves.Count);
            Assert.AreEqual(0, moves[0].Point.X);
            Assert.AreEqual(0, moves[0].Point.Y);
            Assert.AreEqual(Color.White, moves[0].Color);
        }
    }
}

