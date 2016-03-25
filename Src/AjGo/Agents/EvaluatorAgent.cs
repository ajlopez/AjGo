using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Agents
{
    public class EvaluatorAgent
    {
        private Game game;
        private Color color;

        public EvaluatorAgent(Game g, Color c)
        {
            game = g;
            color = c;
        }

        private int Evaluate(Game game)
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
                        if (color == Color.Black)
                            greens += (2 * gblues - greds - 2 * gyellows) / (2 * gblues + greds + 2 * gyellows);
                        else
                            greens -= (2 * gyellows - greds - 2 * gblues) / (2 * gblues + greds + 2 * gyellows);
                }

            if (color == Color.Black)
                reds = (short) (-reds);

            return (blacks + blues * 3) - (whites + yellows * 3) + reds*2 + greens;
        }

        public List<Move> Process()
        {
            List<Move> moves = null;
            int bestvalue;

            if (color==Color.Black)
                bestvalue = -10000;
            else
                bestvalue = 10000;

            for (short x=0; x<game.Position.Width; x++)
                for (short y=0; y<game.Position.Height; y++)
                    if (game.IsEmpty(x,y) && game.IsValid(x,y,color)) {
                        Color cellcolor = game.GetColor(x, y);

                        if (cellcolor != Color.Green && ((color == Color.White && cellcolor != Color.Yellow) || (color == Color.Black && cellcolor != Color.Blue)))
                        {
                            ColorCount cc = game.ColoredPosition.CountFrontierColors(new Point(x, y));

                            if (cc.Count[(int)Color.Green] == 0)
                            {
                                if (color == Color.Black && cc.Count[(int)Color.Blue] == 0 && cc.Count[(int)Color.Black] == 0)
                                    continue;
                                if (color == Color.White && cc.Count[(int)Color.Yellow] == 0 && cc.Count[(int)Color.White] == 0)
                                    continue;
                            }
                        }

                        Game gametest = game.Clone();
                        gametest.Play(x,y,color);
                        int value = Evaluate(gametest);

                        if (color == Color.Black && value > bestvalue)
                        {
                            moves = new List<Move>();
                            bestvalue = value;
                        }
                        if (color == Color.Black && value== bestvalue)
                        {
                            moves.Add(new Move(x, y, color));
                        }
                        if (color == Color.White && value < bestvalue)
                        {
                            moves = new List<Move>();
                            bestvalue = value;
                        }
                        if (color == Color.White && value == bestvalue)
                        {
                            moves.Add(new Move(x, y, color));
                        }
                    }

            return moves;
        }
    }
}
