using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using AjGo.Agents;

using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class SimpleKillSaveAgentTests
    {
        [Test]
        public void CanKillTest1()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);

            SimpleKillSaveAgent agent = new SimpleKillSaveAgent();

            Assert.IsFalse(agent.CanKill(game, 3, 3, 3));
        }

        [Test]
        public void CanKillTest2()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);
            game.Play(2, 3, Color.White);
            game.Play(4, 3, Color.White);
            game.Play(3, 4, Color.White);

            SimpleKillSaveAgent agent = new SimpleKillSaveAgent();

            Assert.IsTrue(agent.CanKill(game, 3, 3, 3));
        }

        [Test]
        public void CanSaveTest1()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);

            SimpleKillSaveAgent agent = new SimpleKillSaveAgent();

            Assert.IsTrue(agent.CanSave(game, 3, 3, 3));
        }

        [Test]
        public void CanSaveTest2()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);
            game.Play(2, 3, Color.White);
            game.Play(4, 3, Color.White);
            game.Play(3, 2, Color.White);

            SimpleKillSaveAgent agent = new SimpleKillSaveAgent();

            Assert.IsTrue(agent.CanSave(game, 3, 3, 3));
        }
    }
}
