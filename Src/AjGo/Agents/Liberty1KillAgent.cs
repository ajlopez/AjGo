using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Agents
{
    public class Liberty1KillAgent
    {
        private Game game;
        private short xtokill;
        private short ytokill;

        public Liberty1KillAgent(Game game, short xtokill, short ytokill)
        {
            this.game = game;
            this.xtokill = xtokill;
            this.ytokill = ytokill;
        }

        public List<Move> Process()
        {
            List<Move> moves = new List<Move>();

            Group gp = game.GetGroup(xtokill, ytokill);

            if (gp.CountLiberties > 1)
                return moves;

            Point p = gp.Liberties.Points[0];

            Color color;

            if (game.GetColor(xtokill, ytokill) == Color.Black)
                color = Color.White;
            else
                color = Color.Black;

            Move move = new Move(p.X, p.Y, color);

            if (game.IsValid(move))
            {
                moves.Add(move);
                Game newgame = game.Clone();
                newgame.Play(move);
            }

            return moves;
        }
    }
}
