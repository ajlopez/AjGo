using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Evaluators
{
    public class GamesEvaluator
    {
        public List<EvaluatedGame> Evaluate(List<Game> games, IEvaluator evaluator)
        {
            List<EvaluatedGame> evals = new List<EvaluatedGame>();

            foreach (Game game in games)
                evals.Add(evaluator.Evaluate(game));

            return evals;
        }

        public List<EvaluatedGame> Evaluate(Game game, Color color, IEvaluator evaluator)
        {
            List<EvaluatedGame> evals = new List<EvaluatedGame>();

            for (short x=0; x<game.Position.Width; x++)
                for (short y = 0; y < game.Position.Height; y++)
                {
                    Move move = new Move(x, y, color);

                    if (game.IsValid(move))
                    {
                        Game newgame = game.Clone();
                        newgame.Play(move);
                        evals.Add(evaluator.Evaluate(newgame));
                    }
                }

            if (color == Color.White)
                evals.Sort(new WhiteValueComparer());
            else
                evals.Sort(new BlackValueComparer());

            return evals;
        }

        public List<EvaluatedGame> Evaluate(Game game, Color color, IEvaluator evaluator, PointSet points)
        {
            List<EvaluatedGame> evals = new List<EvaluatedGame>();

            foreach (Point point in points.Points)
                {
                    Move move = new Move(point.X, point.Y, color);

                    if (game.IsValid(move))
                    {
                        Game newgame = game.Clone();
                        newgame.Play(move);
                        evals.Add(evaluator.Evaluate(newgame));
                    }
                }

            if (color == Color.White)
                evals.Sort(new WhiteValueComparer());
            else
                evals.Sort(new BlackValueComparer());

            return evals;
        }
    }
}
