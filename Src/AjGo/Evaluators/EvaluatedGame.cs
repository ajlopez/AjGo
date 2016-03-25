using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Evaluators
{
    public class EvaluatedGame
    {
        private Game game;
        private int value;

        public EvaluatedGame(Game game, int value)
        {
            this.game = game;
            this.value = value;
        }

        public Game Game
        {
            get { return game; }
        }

        public int Value
        {
            get { return value; }
        }
    }

    public class WhiteValueComparer : IComparer<EvaluatedGame>
    {
        public int Compare(EvaluatedGame x, EvaluatedGame y)
        {
            if (x.Value > y.Value)
                return 1;
            if (x.Value < y.Value)
                return -1;

            return 0;
        }
    }

    public class BlackValueComparer : IComparer<EvaluatedGame>
    {
        public int Compare(EvaluatedGame x, EvaluatedGame y)
        {
            if (x.Value < y.Value)
                return 1;
            if (x.Value > y.Value)
                return -1;

            return 0;
        }
    }

    public interface IEvaluator
    {
        EvaluatedGame Evaluate(Game game);
    }

    public class StoneEvaluator : IEvaluator
    {
        public EvaluatedGame Evaluate(Game game)
        {
            return new EvaluatedGame(game, game.Blacks + game.DeadWhites - game.Whites - game.DeadBlacks);
        }
    }

    public class ColorEvaluator : IEvaluator
    {
        private int CalculateValue(Game game)
        {
            int blacks = game.ColoredPosition.CountColor(Color.Black);
            int whites = game.ColoredPosition.CountColor(Color.White);
            int blues = game.ColoredPosition.CountColor(Color.Blue);
            int yellows = game.ColoredPosition.CountColor(Color.Yellow);
            int reds = game.ColoredPosition.CountColor(Color.Red);

            int greens = 0;

            foreach (Group group in game.ColoredGroups)
                if (group.Color == Color.Green)
                {
                    PointSet frontier = group.CalculateFrontier(game.ColoredPosition);
                    ColorCount cc = game.ColoredPosition.CountColors(frontier);

                    int gyellows = cc.Count[(int)Color.Yellow];
                    int gblues = cc.Count[(int)Color.Blue];
                    int greds = cc.Count[(int)Color.Red];

                    if (gyellows + greds + gblues > 0)
                        greens += (2 * gblues - 2 * gyellows) / (2 * gblues + greds + 2 * gyellows);
                }

            return (blacks + blues * 3) - (whites + yellows * 3) + greens;
        }

        public EvaluatedGame Evaluate(Game game)
        {
            return new EvaluatedGame(game, CalculateValue(game));
        }
    }

    public class ZoneGameEvaluator : IEvaluator
    {
        public EvaluatedGame Evaluate(Game game)
        {
            GameEvaluation gev = new GameEvaluation(game, null);

            int value = 0;

            foreach (ZoneEvaluation zev in gev.ZoneEvaluations)
                if (zev.IsSafe || zev.GreenLife > 4 || zev.InternalCount>=10 || zev.GreenEyes>0)
                    if (zev.Color == Color.Black)
                        //value += zev.InternalCount;
                        value += zev.PointValue;
                    else if (zev.Color == Color.White)
                        value -= zev.PointValue;
                        //value -= zev.InternalCount;
                else
                    if (zev.Color == Color.Black)
//                        value -= zev.Size + zev.StoneSize;
                        value -= zev.PointValue;
                    else if (zev.Color == Color.White)
//                        value += zev.Size + zev.StoneSize;
                        value += zev.PointValue;

            return new EvaluatedGame(game, value + game.DeadWhites * 2- game.DeadBlacks * 2);
        }
    }
}

