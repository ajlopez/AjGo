using System;
using System.Collections.Generic;
using System.Text;

using AjGo.Evaluators;

namespace AjGo.Agents
{
    public class ZoneEvaluatorAgent
    {
        private Game game;
        private Color color;
        private short xtoprocess;
        private short ytoprocess;
        private Goal goal;
        private GroupSet zone;
        private ZoneEvaluation evaluation;

        private bool IsGoal(Game newgame)
        {
            GroupSet newzone = newgame.GetZone(xtoprocess, ytoprocess);

            if (newzone == null)
                return false;

            ZoneEvaluation neweval = (new ZoneEvaluator()).Evaluate(newzone, newgame.ColoredPosition);

            if (goal==Goal.Extend)
                return (neweval.Size > evaluation.Size);

            if (goal == Goal.Connect)
                return (neweval.StoneSize > evaluation.StoneSize + 1);

            if (goal == Goal.Cut)
                return (neweval.StoneSize < evaluation.StoneSize);

            if (goal == Goal.Escape)
                return (neweval.GreenLife > evaluation.GreenLife);

            if (goal == Goal.Surrender)
                return (neweval.GreenLife < evaluation.GreenLife);

            if (goal == Goal.Eyes)
                return (neweval.IsSafe);

            return false;
        }

        public ZoneEvaluatorAgent(Game g, short x, short y, Goal goal)
        {
            game = g;
            color = game.GetColor(x,y);

            if (goal == Goal.Surrender || goal == Goal.Cut)
                color = Utilities.EnemyColor(color);

            xtoprocess = x;
            ytoprocess = y;
            this.goal = goal;

            zone = game.GetZone(x, y);

            evaluation = (new ZoneEvaluator()).Evaluate(zone, game.ColoredPosition);
        }

        public List<Move> Process()
        {
            List<Move> moves = new List<Move>();

            for (short x=0; x<game.Position.Width; x++)
                for (short y=0; y<game.Position.Height; y++)
                    if (game.IsEmpty(x, y) && game.IsValid(x, y, color))
                    {
                        Game newgame = game.Clone();
                        newgame.Play(x, y, color);

                        if (IsGoal(newgame))
                            moves.Add(new Move(x, y, color));
                    }

            return moves;
        }
    }
}

