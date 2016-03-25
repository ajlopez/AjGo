using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AjGo.Agents
{
    public class GenKillerAgent : KillerAgent
    {
        private int initialliberties;
        private int initialsize;

        public GenKillerAgent(Game g, short x, short y)
            : base(g, x, y)
        {
        }

        private bool CanSave(Game game, Move move)
        {
            if (!game.IsValid(move))
                return false;
            if (game.IsRepeated(move))
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
                if (gp.Color == color && gp.CountLiberties == 1)
                {
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

                newp = new Point((short)(p.X - 1), (short)(p.Y - 1));
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }

                newp = new Point(p.X, (short)(p.Y - 2));
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

                newp = new Point((short)(p.X + 1), (short)(p.Y + 1));
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }

                newp = new Point(p.X, (short)(p.Y + 2));
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

            if (game.IsRepeated(m))
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

            if (group.CountLiberties >= initialliberties+2)
                return false;
            if (group.Count >= initialsize * 2 && group.Count>6)
                return false;
            if (group.Count>4 && group.CountLiberties > group.Count / 2 + 2)
                return false;

            PointSet tried = new PointSet();

            // TODO explore only neighbour groups

            foreach (Group gp in game.Groups)
                if (gp.Color == colortokill && gp.CountLiberties == 1)
                {
                    Point p = gp.Liberties.Points[0];

                    tried.Add(p);

                    Move move = new Move(p.X, p.Y, color);

                    if (CanKill(game, move))
                        return true;
                }

            // Visit liberties

            foreach (Point p in group.Liberties.Points)
                if (CanKill(game, new Move(p.X, p.Y, color)))
                    return true;
                else
                    tried.Add(p);

            // Visit liberties2

            PointSet liberties2 = new PointSet();
            PointSet liberties3 = new PointSet();

            foreach (Point p in group.Points)
            {
                Point newp;

                newp = new Point((short)(p.X - 2), p.Y);
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }
                else if (game.GetColor(newp.X, newp.Y) == colortokill)
                {
                    Group other = game.GetGroup(newp.X, newp.Y);
                    if (other != group)
                        foreach (Point l in other.Liberties.Points)
                            if (!tried.Points.Contains(l))
                                liberties3.Add(l);
                }

                newp = new Point((short)(p.X - 1), (short)(p.Y - 1));
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }
                else if (game.GetColor(newp.X, newp.Y) == colortokill)
                {
                    Group other = game.GetGroup(newp.X, newp.Y);
                    if (other != group)
                        foreach (Point l in other.Liberties.Points)
                            if (!tried.Points.Contains(l))
                                liberties3.Add(l);
                }

                newp = new Point(p.X, (short)(p.Y - 2));
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }
                else if (game.GetColor(newp.X, newp.Y) == colortokill)
                {
                    Group other = game.GetGroup(newp.X, newp.Y);
                    if (other != group)
                        foreach (Point l in other.Liberties.Points)
                            if (!tried.Points.Contains(l))
                                liberties3.Add(l);
                }

                newp = new Point((short)(p.X + 2), p.Y);
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }
                else if (game.GetColor(newp.X, newp.Y) == colortokill)
                {
                    Group other = game.GetGroup(newp.X, newp.Y);
                    if (other != group)
                        foreach (Point l in other.Liberties.Points)
                            if (!tried.Points.Contains(l))
                                liberties3.Add(l);
                }

                newp = new Point((short)(p.X + 1), (short)(p.Y + 1));
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }
                else if (game.GetColor(newp.X, newp.Y) == colortokill)
                {
                    Group other = game.GetGroup(newp.X, newp.Y);
                    if (other != group)
                        foreach (Point l in other.Liberties.Points)
                            if (!tried.Points.Contains(l))
                                liberties3.Add(l);
                }

                newp = new Point(p.X, (short)(p.Y + 2));
                if (game.IsEmpty(newp.X, newp.Y))
                {
                    if (!tried.Points.Contains(newp))
                        liberties2.Add(newp);
                }
                else if (game.GetColor(newp.X, newp.Y) == colortokill)
                {
                    Group other = game.GetGroup(newp.X, newp.Y);
                    if (other != group)
                        foreach (Point l in other.Liberties.Points)
                            if (!tried.Points.Contains(l))
                                liberties3.Add(l);
                }
            }

            foreach (Point p in liberties2.Points)
            {
                if (tried.Points.Contains(p))
                    continue;

                if (CanKill(game, new Move(p.X, p.Y, color)))
                    return true;

                tried.Add(p);
            }

            //foreach (Point p in liberties3.Points)
            //{
            //    if (tried.Points.Contains(p))
            //        continue;

            //    if (CanKill(game, new Move(p.X, p.Y, color)))
            //        return true;

            //    tried.Add(p);
            //}

            // TODO Test kill neighbour enemy groups

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

                if (CanKill(game, m))
                    moves.Add(m);
            }

            PointSet liberties2 = group.Liberties.CalculateLiberties(game.Position);

            foreach (Point p in liberties2.Points)
            {
                Move m = new Move(p.X, p.Y, color);

                if (CanKill(game, m))
                    moves.Add(m);
            }

            return moves;
        }

        private void CanKill(object m) {
            Move move = (Move)m;
            if (CanKill(game, move))
            {
                lock (moves)
                {
                    moves.Add(move);
                }
                resolved = true;
                return;
            }
        }

        public override List<Move> ParallelProcess()
        {
            moves = new List<Move>();
            waithandle = new AutoResetEvent(false);

            Group group = game.GetGroup(xtokill, ytokill);

            initialliberties = group.CountLiberties;
            initialsize = group.Count;

            foreach (Point p in group.Liberties.Points)
            {
                Move m = new Move(p.X, p.Y, color);

                Thread t = new Thread(CanKill);
                t.Start(m);
            }

            PointSet liberties2 = group.Liberties.CalculateLiberties(game.Position);

            foreach (Point p in liberties2.Points)
            {
                Move m = new Move(p.X, p.Y, color);

                Thread t = new Thread(CanKill);
                t.Start(m);
            }

            waithandle.WaitOne();

            return moves;
        }
    }
}
