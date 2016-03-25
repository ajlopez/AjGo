using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Agents
{
    public class Liberty3KillAgent
    {
        short xtokill;
        short ytokill;
        Color colortokill;
        Color color;
        Game game;

        public Liberty3KillAgent(Game game, short xtokill, short ytokill) {
            this.game = game;
            this.xtokill = xtokill;
            this.ytokill = ytokill;

            colortokill = game.GetColor(xtokill, ytokill);

            if (colortokill == Color.Black)
                color = Color.White;
            if (colortokill == Color.White)
                color = Color.Black;
        }

        private bool CanSave(Game game, short level)
        {
            return SaveStrategy.Save(game, xtokill, ytokill, 1, level).Count>0;
        }

        private bool CanKill(Game game, Move move, short level)
        {
            if (!game.IsValid(move))
                return false;

            Game newgame = game.Clone();
            newgame.Play(move);

            if (newgame.GetColor(xtokill, ytokill) == Color.Empty)
                return true;

            return !CanSave(newgame, level);
        }

        public List<Move> Process(short nmoves, short level)
        {
            List<Move> moves = new List<Move>();

            if (nmoves <= 0)
                return moves;

            List<Move> tried = new List<Move>();

            Group group = game.GetGroup(xtokill, ytokill);

            foreach (Point p in group.Liberties.Points) {
                Move move = new Move(p.X, p.Y, color);

                if (tried.Contains(move))
                    continue;

                tried.Add(move);

                if (CanKill(game, move, level))
                {
                    moves.Add(move);
                    if (moves.Count >= nmoves)
                        return moves;
                }
            }

            return moves;
        }
    }
}

