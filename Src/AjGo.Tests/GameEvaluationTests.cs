using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using AjGo.Evaluators;

using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class GameEvaluationTests
    {
        [Test]
        public void EvalTest1()
        {
            Game game = new Game();

            GameEvaluation eval = new GameEvaluation(game, null);

            Assert.AreEqual(game, eval.Game);
            Assert.IsNull(eval.Move);
            Assert.IsNotNull(eval.ZoneEvaluations);
            Assert.AreEqual(1, eval.ZoneEvaluations.Count);
        }

        [Test]
        public void EvalTest2()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);

            GameEvaluation eval = new GameEvaluation(game, null);

            Assert.AreEqual(game, eval.Game);
            Assert.IsNull(eval.Move);
            Assert.IsNotNull(eval.ZoneEvaluations);
            Assert.AreEqual(1, eval.ZoneEvaluations.Count);
        }

        [Test]
        public void EvalTest3()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);
            game.Play(15, 15, Color.White);

            GameEvaluation eval = new GameEvaluation(game, null);

            Assert.AreEqual(game, eval.Game);
            Assert.IsNull(eval.Move);
            Assert.IsNotNull(eval.ZoneEvaluations);
            Assert.AreEqual(3, eval.ZoneEvaluations.Count);
        }
    }
}

