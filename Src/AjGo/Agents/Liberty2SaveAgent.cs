using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Agents
{
    public class Liberty2SaveAgent
    {
        private Game game;
        private short xtosave;
        private short ytosave;
        private Color enemycolor;

        public Liberty2SaveAgent(Game game, short xtosave, short ytosave)
        {
            this.game = game;
            this.xtosave = xtosave;
            this.ytosave = ytosave;

            if (game.GetColor(xtosave, ytosave) == Color.Black)
                enemycolor = Color.White;
            else
                enemycolor = Color.Black;
        }

        private bool CanSave(Game game, Move move, short level)
        {
            if (!game.IsValid(move))
                return false;

            Game newgame = game.Clone();
            newgame.Play(move);

            Group gp1 = game.GetGroup(xtosave, ytosave);
            Group gp2 = newgame.GetGroup(xtosave, ytosave);

            if (gp1.Liberties.Count == 1 && gp2.Liberties.Count>=3)
                return true;

            if (gp2.Liberties.Count > 2)
                level++;

            if (level > 6)
                return true;

            if (gp2.Liberties.Count >= 4)
                return true;
            if (gp2.Liberties.Count - gp1.Liberties.Count >= 2)
                return true;

            return KillStrategy.Kill(newgame, xtosave, ytosave, 1, level).Count == 0;
        }

        public List<Move> Process(short nmoves, short level)
        {
            List<Move> moves = new List<Move>();

            if (nmoves <= 0)
                return moves;

            List<Move> tried = new List<Move>();

            Group group = game.GetGroup(xtosave, ytosave);

            PointSet frontier = group.CalculateFrontier(game.Position);

            level++;

            foreach (Point pt in frontier.Points)
            {
                if (game.IsEmpty(pt.X, pt.Y))
                {
                    Move move = new Move(pt.X, pt.Y, group.Color);

                    if (tried.Contains(move))
                        continue;

                    if (CanSave(game, move, level))
                    {
                        moves.Add(move);

                        if (moves.Count >= nmoves)
                            return moves;
                    }
                }
            }

            PointSet liberties2 = group.Liberties.CalculateLiberties(game.Position);

            foreach (Point pt in liberties2.Points)
            {
                Move mv = new Move(pt.X, pt.Y, group.Color);

                if (tried.Contains(mv))
                    continue;

                tried.Add(mv);

                if (CanSave(game, mv,level))
                {
                    moves.Add(mv);
                    if (moves.Count >= nmoves)
                        return moves;
                }
            }

            List<Group> enemies = new List<Group>();

            foreach (Point pt in group.CalculateFrontier(game.Position).Points)
            {
                Group gr = game.GetGroup(pt.X, pt.Y);

                if (gr != null && gr.Color == enemycolor && gr.Liberties.Count <= group.Liberties.Count && !enemies.Contains(gr))
                    enemies.Add(gr);
            }

            foreach (Group enemy in enemies)
            {
                if (enemy.Liberties.Count != 1)
                    continue;

                foreach (Point pt in enemy.Liberties.Points)
                {
                    Move mv = new Move(pt.X, pt.Y, group.Color);

                    if (tried.Contains(mv))
                        continue;

                    tried.Add(mv);

                    if (CanSave(game, mv, (short)(level + 1)))
                    {
                        moves.Add(mv);
                        if (moves.Count >= nmoves)
                            return moves;
                    }
                }
            }

            foreach (Point pt in group.Liberties.Points)
            {
                Move move = new Move(pt.X, pt.Y, group.Color);

                if (tried.Contains(move))
                    continue;

                if (CanSave(game, move, level))
                {
                    moves.Add(move);

                    if (moves.Count >= nmoves)
                        return moves;
                }
            }

            if (enemies.Count > 0)
                level++;

            for (short lb=2; lb<=group.Liberties.Count; lb++)
                foreach (Group enemy in enemies)   
                {
                    if (enemy.Liberties.Count != lb)
                        continue;

                    foreach (Point pt in enemy.Liberties.Points)
                    {
                        Move mv = new Move(pt.X, pt.Y, group.Color);

                        if (tried.Contains(mv))
                            continue;

                        tried.Add(mv);

                        if (CanSave(game, mv, (short)(level + 1)))
                        {
                            moves.Add(mv);
                            if (moves.Count >= nmoves)
                                return moves;
                        }
                    }
                }

            if (group.Liberties.Count <= 2)
                return moves;


            return moves;
        }
    }
}
