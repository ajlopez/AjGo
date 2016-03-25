using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using AjGo.Agents;

using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class Liberty2KillAgentTests
    {
        [Test]
        public void KillTest1()
        {
            Game game = new Game();
            game.Play(3, 3, Color.Black);
            Liberty2KillAgent agent = new Liberty2KillAgent(game, 3, 3);

            SaveStrategy.Initialize();
            KillStrategy.Initialize();
            List<Move> moves = agent.Process(1, 0);

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

            Liberty2KillAgent agent = new Liberty2KillAgent(game, 3, 3);

            SaveStrategy.Initialize();
            KillStrategy.Initialize();
            List<Move> moves = agent.Process(1, 0);

            Assert.IsNotNull(moves);
            Assert.AreEqual(1, moves.Count);
        }


        [Test]
        public void KillTest3()
        {
            Game game = new Game();

            game.Play(0, 0, Color.Black);
            game.Play(1, 0, Color.White);

            Liberty2KillAgent agent = new Liberty2KillAgent(game, 0, 0);

            Assert.IsNotNull(agent);

            SaveStrategy.Initialize();
            KillStrategy.Initialize();
            List<Move> moves = agent.Process(1, 0);

            Assert.IsNotNull(agent);
            Assert.AreEqual(1, moves.Count);
            Assert.AreEqual(0, moves[0].Point.X);
            Assert.AreEqual(1, moves[0].Point.Y);
            Assert.AreEqual(Color.White, moves[0].Color);
        }

        [Test]
        public void KillTest4()
        {
            Game game = new Game();

            game.Play(0, 1, Color.Black);
            game.Play(0, 2, Color.White);
            game.Play(1, 1, Color.White);

            Liberty2KillAgent agent = new Liberty2KillAgent(game, 0, 1);

            Assert.IsNotNull(agent);

            SaveStrategy.Initialize();
            KillStrategy.Initialize();
            List<Move> moves = agent.Process(1, 0);

            Assert.IsNotNull(agent);
            Assert.AreEqual(1, moves.Count);
            Assert.AreEqual(0, moves[0].Point.X);
            Assert.AreEqual(0, moves[0].Point.Y);
            Assert.AreEqual(Color.White, moves[0].Color);
        }

        [Test]
        public void KillTest5()
        {
            Game game = new Game();

            game.Play(0, 1, Color.Black);
            game.Play(0, 2, Color.Black);
            game.Play(0, 3, Color.White);
            game.Play(1, 1, Color.White);
            game.Play(1, 2, Color.White);

            Liberty2KillAgent agent = new Liberty2KillAgent(game, 0, 1);

            Assert.IsNotNull(agent);

            SaveStrategy.Initialize();
            KillStrategy.Initialize();
            List<Move> moves = agent.Process(1, 0);

            Assert.IsNotNull(agent);
            Assert.AreEqual(1, moves.Count);
            Assert.AreEqual(0, moves[0].Point.X);
            Assert.AreEqual(0, moves[0].Point.Y);
            Assert.AreEqual(Color.White, moves[0].Color);
        }

        [Test]
        public void KillTest6()
        {
            PositionBuilder pb = new PositionBuilder();

            pb.MakePosition("....\n..X.\n.XO.\n.XO.\n..X.\n");

            Position position = pb.GetPosition();

            Game game = new Game(position);

            Liberty2KillAgent agent = new Liberty2KillAgent(game, 2, 2);

            SaveStrategy.Initialize();
            KillStrategy.Initialize();
            List<Move> moves = agent.Process(2, 0);

            Assert.IsNotNull(agent);
            Assert.AreEqual(2, moves.Count);
        }

        [Test]
        public void KillTest7()
        {
            PositionBuilder pb = new PositionBuilder();

            pb.MakePosition("....\n..X.\n.XO.O\n.XO.\n..X.\n");

            Position position = pb.GetPosition();

            Game game = new Game(position);

            Liberty2KillAgent agent = new Liberty2KillAgent(game, 2, 2);

            SaveStrategy.Initialize();
            KillStrategy.Initialize();
            List<Move> moves = agent.Process(5, 0);

            Assert.IsNotNull(agent);
            Assert.AreEqual(0, moves.Count);
        }

        [Test]
        public void KillTest8()
        {
            PositionBuilder pb = new PositionBuilder();

            pb.MakePosition("....\n..X.\n.XO.\n.XO.\n..X.\n\n.....O\n");

            Position position = pb.GetPosition();

            Game game = new Game(position);

            Liberty2KillAgent agent = new Liberty2KillAgent(game, 2, 2);

            SaveStrategy.Initialize();
            KillStrategy.Initialize();
            List<Move> moves = agent.Process(2, 0);

            Assert.IsNotNull(agent);
            Assert.AreEqual(1, moves.Count);
        }
    }
}
