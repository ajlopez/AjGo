using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AjGo.Agents
{
    public class LibertyKillerAgent : KillerAgent
    {
        private int initialliberties;
        private int initialsize;
        private int maxlevel = 10;

        public LibertyKillerAgent(Game g, short x, short y)
            : base(g, x, y)
        {
        }

        private bool CanSave(Game game, Move move, short level)
        {
            if (!game.IsValid(move))
                return false;
            if (game.IsRepeated(move))
                return false;

            Game gametest = game.Clone();

            gametest.Play(move);

            RaiseNewGame(gametest);

            if (CanKill(gametest,level))
                return false;

            return true;
        }

        private bool CanSave(Game game, short level)
        {
            PointSet tried = new PointSet();

            // Visit liberties

            Group group = game.GetGroup(xtokill, ytokill);

            if (group.CountLiberties > 2)
                level++;

            if (level > maxlevel)
                return false;

            foreach (Point p in group.Liberties.Points)
                if (CanSave(game, new Move(p.X, p.Y, colortokill),level))
                    return true;
                else
                    tried.Add(p);

            return false;
        }

        private bool CanKill(Game game, Move m, short level)
        {
            if (!game.IsValid(m))
                return false;

            if (game.IsRepeated(m))
                return false;

            Game gametest = game.Clone();

            gametest.Play(m);

            RaiseNewGame(gametest);

            if (gametest.GetColor(xtokill, ytokill) != colortokill)
                return true;

            return !CanSave(gametest,level);
        }

        private bool CanKill(Game game, short level)
        {
            Group group = game.GetGroup(xtokill, ytokill);

            // TODO Review this strategy

            if (group.CountLiberties >= initialliberties + 2)
                return false;
            if (group.Count >= initialsize * 2 && group.Count > 6)
                return false;
            if (group.Count > 4 && group.CountLiberties > group.Count / 2 + 2)
                return false;

            if (group.CountLiberties > 2)
                level++;

            if (level > maxlevel)
                return false;

            PointSet tried = new PointSet();

            // Visit liberties

            foreach (Point p in group.Liberties.Points)
                if (CanKill(game, new Move(p.X, p.Y, color),level))
                    return true;
                else
                    tried.Add(p);

            return false;
        }

        public override List<Move> Process()
        {
            moves = new List<Move>();

            Group group = game.GetGroup(xtokill, ytokill);

            initialliberties = group.CountLiberties;
            initialsize = group.Count;

            foreach (Point p in group.Liberties.Points)
            {
                Move m = new Move(p.X, p.Y, color);

                if (CanKill(game, m, 0))
                    moves.Add(m);
            }

            PointSet liberties2 = group.Liberties.CalculateLiberties(game.Position);

            foreach (Point p in liberties2.Points)
            {
                Move m = new Move(p.X, p.Y, color);

                if (CanKill(game, m, 0))
                {
                    moves.Add(m);
                    break;
                }
            }

            return moves;
        }
    }
}
