using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Evaluators
{
    public class GameEvaluation
    {
        private Game game;
        private Move move;
        private List<ZoneEvaluation> zoneevals = new List<ZoneEvaluation>();

        public GameEvaluation(Game game, Move move)
        {
            this.game = game;
            this.move = move;

            ZoneEvaluator evaluator = new ZoneEvaluator();

            foreach (GroupSet zone in game.Zones)
                zoneevals.Add(evaluator.Evaluate(zone, game.ColoredPosition));
        }

        public Game Game
        {
            get { return game; }
        }

        public Move Move
        {
            get { return move; }
        }

        public List<ZoneEvaluation> ZoneEvaluations
        {
            get { return zoneevals; }
        }
    }
}
