using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using AjGo.Evaluators;

using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class EvaluatorsTests
    {
        [Test]
        public void StoneEvaluatorTest1()
        {
            Game game = new Game();
            StoneEvaluator evaluator = new StoneEvaluator();
            EvaluatedGame eg = evaluator.Evaluate(game);

            Assert.IsNotNull(eg);
            Assert.AreEqual(game,eg.Game);
            Assert.AreEqual(0,eg.Value);
        }

        [Test]
        public void StoneEvaluatorTest2()
        {
            Game game = new Game();
            game.Play(3,3,Color.Black);

            StoneEvaluator evaluator = new StoneEvaluator();
            EvaluatedGame eg = evaluator.Evaluate(game);

            Assert.IsNotNull(eg);
            Assert.AreEqual(game,eg.Game);
            Assert.AreEqual(1,eg.Value);
        }

        [Test]
        public void StoneEvaluatorTest3()
        {
            Game game = new Game();

            game.Play(3, 3, Color.Black);
            game.Play(2, 3, Color.White);
            game.Play(4, 3, Color.White);
            game.Play(3, 2, Color.White);
            game.Play(3, 4, Color.White);

            StoneEvaluator evaluator = new StoneEvaluator();
            EvaluatedGame eg = evaluator.Evaluate(game);

            Assert.IsNotNull(eg);
            Assert.AreEqual(game,eg.Game);
            Assert.AreEqual(-5,eg.Value);
        }
    }
}
