using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Agents
{
    public class OldSichoAgent : KillerAgent
    {
        public OldSichoAgent(Game g, short x, short y)
            : base(g, x, y)
        {
        }

        private bool CanSave(Game game, Move move)
        {
            if (!game.IsValid(move))
                return false;

            Game gametest = game.Clone();

            gametest.Play(move);

            RaiseNewGame(gametest);

            if (CanKill(gametest))
                return false;

            return true;
        }

        private bool CanSave(Game game)
        {
            PointSet tried = new PointSet();

            // TODO explore only neighbour groups

            foreach (Group gp in game.Groups)
                if (gp.Color == color && gp.CountLiberties == 1) {
                    Point p = gp.Liberties.Points[0];

                    tried.Add(p);

                    Move move = new Move(p.X, p.Y, colortokill);

                    if (CanSave(game, move))
                        return true;
                }

            // Visit liberties
            
            Group group = game.GetGroup(xtokill, ytokill);

            foreach (Point p in group.Liberties.Points)
                if (CanSave(game, new Move(p.X, p.Y, colortokill)))
                    return true;
                else
                    tried.Add(p);

            // Visit liberties2

            PointSet liberties2 = new PointSet();

            foreach (Point p in group.Points)
            {
                Point newp;

                newp = new Point((short)(p.X - 2), p.Y);
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }

                newp = new Point((short)(p.X - 1), (short) (p.Y-1));
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }

                newp = new Point(p.X, (short) (p.Y-2));
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }

                newp = new Point((short)(p.X + 2), p.Y);
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }

                newp = new Point((short)(p.X + 1), (short) (p.Y + 1));
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }

                newp = new Point(p.X, (short) (p.Y + 2));
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }
            }

            foreach (Point p in liberties2.Points)
                if (CanSave(game, new Move(p.X, p.Y, colortokill)))
                    return true;

            // TODO Test kill neighbour enemy groups

            return false;
        }

        private bool CanKill(Game game, Move m)
        {
            if (!game.IsValid(m))
                return false;

            Game gametest = game.Clone();

            gametest.Play(m);

            RaiseNewGame(gametest);

            if (gametest.GetColor(xtokill, ytokill) != colortokill)
                return true;

            return !CanSave(gametest);
        }

        private bool CanKill(Game game)
        {
            Group group = game.GetGroup(xtokill, ytokill);

            // TODO Review this strategy

            if (group.CountLiberties > 2)
                return false;

            foreach (Point p in group.Liberties.Points)
            {
                Move m = new Move(p.X, p.Y, color);

                if (CanKill(game, m))
                    return true;
            }

            return false;
        }

        public override List<Move> Process()
        {
            List<Move> moves = new List<Move>();

            Group group = game.GetGroup(xtokill, ytokill);

            foreach (Point p in group.Liberties.Points)
            {
                Move m = new Move(p.X, p.Y, color);

                if (CanKill(game, m))
                    moves.Add(m);
            }

            return moves;
        }
    }
}
