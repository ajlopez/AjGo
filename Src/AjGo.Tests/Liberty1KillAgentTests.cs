using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using AjGo.Agents;

using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class Liberty1KillAgentTests
    {
        [Test]
        public void KillTest1()
        {
            Game game = new Game();
            game.Play(3, 3, Color.Black);
            Liberty1KillAgent agent = new Liberty1KillAgent(game, 3, 3);
            List<Move> moves = agent.Process();

            Assert.IsNotNull(moves);
            Assert.AreEqual(0, moves.Count);
        }

        [Test]
        public void KillTest2()
        {
            Game game = new Game();
            game.Play(3, 3, Color.Black);
            game.Play(3, 2, Color.White);
            game.Play(2, 3, Color.White);
            game.Play(4, 3, Color.White);

            Liberty1KillAgent agent = new Liberty1KillAgent(game, 3, 3);
            List<Move> moves = agent.Process();

            Assert.IsNotNull(moves);
            Assert.AreEqual(1, moves.Count);
        }
    }
}
