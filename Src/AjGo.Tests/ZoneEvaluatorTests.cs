using System;
using System.Collections.Generic;
using System.Text;

using AjGo;
using AjGo.Evaluators;

using NUnit.Framework;

namespace AjGo.Tests
{
    [TestFixture]
    public class ZoneEvaluatorTests
    {
        [Test]
        public void EvaluationTest1()
        {
            Game game = new Game();
            GroupSet zone = game.GetZone(3, 3);

            ZoneEvaluation evaluation = (new ZoneEvaluator()).Evaluate(zone, game.ColoredPosition);

            Assert.IsNotNull(evaluation);
            Assert.AreEqual(Color.Green,evaluation.Color);
            Assert.AreEqual(0, evaluation.InternalCount);
            Assert.AreEqual(0, evaluation.BlueEyes);
            Assert.AreEqual(game.Position.Size, evaluation.Size);
        }

        [Test]
        public void EvaluationTest2()
        {
            Game game = new Game();

            game.Play(3,3,Color.Black);
            game.Play(15,15,Color.White);
            game.Play(5,2,Color.Black);
            game.Play(2,5,Color.Black);

            GroupSet zone = game.GetZone(3, 3);

            ZoneEvaluation evaluation = (new ZoneEvaluator()).Evaluate(zone, game.ColoredPosition);

            Assert.IsNotNull(evaluation);
            Assert.AreEqual(Color.Black,evaluation.Color);
            Assert.AreEqual(25, evaluation.InternalCount);
            Assert.AreEqual(0, evaluation.BlueEyes);
            Assert.AreEqual(1, evaluation.GreenEyes);
        }

    }
}
