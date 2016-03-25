using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AjGo.Agents
{
    public class KillerAgent
    {
        protected Game game;
        protected short xtokill;
        protected short ytokill;
        protected Color colortokill;
        protected Color color;
        protected List<Move> moves;
        protected AutoResetEvent waithandle;
        protected bool resolved;

        public delegate void NewGameEventHandler(object sender, Game g);

        public event NewGameEventHandler NewGame;

        public KillerAgent(Game g, short x, short y)
        {
            game = g;
            xtokill = x;
            ytokill = y;
            colortokill = g.GetColor(x, y);

            if (colortokill==Color.White)
                color = Color.Black;
            else if (colortokill==Color.Black)
                color = Color.White;
            else
                throw new InvalidOperationException();
        }

        public void RaiseNewGame(Game g)
        {
            if (NewGame != null)
            {
                NewGame(this, g);
                //System.Threading.Thread.Sleep(400);
            }
        }

        private bool CanKill(Move m)
        {
            if (!game.IsValid(m))
                return false;

            Game gametest = game.Clone();

            gametest.Play(m);

            RaiseNewGame(gametest);

            if (gametest.GetColor(xtokill, ytokill) != colortokill)
                return true;

            return false;
        }

        public virtual List<Move> Process()
        {
            moves = new List<Move>();

            Group group = game.GetGroup(xtokill, ytokill);

            foreach (Point p in group.Liberties.Points) {
                Move m = new Move(p.X, p.Y, color);

                if (CanKill(m))
                    moves.Add(m);
            }

            return moves;
        }

        public virtual List<Move> ParallelProcess()
        {
            moves = new List<Move>();

            Group group = game.GetGroup(xtokill, ytokill);

            foreach (Point p in group.Liberties.Points)
            {
                Move m = new Move(p.X, p.Y, color);

                if (CanKill(m))
                    moves.Add(m);
            }

            return moves;
        }
    }
}

