using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Agents
{
    public class SimpleKillSaveAgent
    {
        private Dictionary<Position, bool> saved = new Dictionary<Position,bool>();
        private Dictionary<Position, bool> killed = new Dictionary<Position, bool>();

        int initialsize;

        private List<Point> GetPointsToTryKill(Group group, Game game)
        {
            PointSet ps = new PointSet();

            ps.Add(group.Liberties);

            //PointSet frontier = group.CalculateFrontier(game.Position);

            //Color enemycolor;

            //if (group.Color == Color.White)
            //    enemycolor = Color.Black;
            //else if (group.Color == Color.Black)
            //    enemycolor = Color.White;
            //else
            //    throw new InvalidOperationException();

            //if (group.Count > initialsize + 10)
            //    return ps.Points;

            //foreach (Point pt in frontier.Points)
            //{
            //    Group gr = game.GetGroup(pt.X, pt.Y);

            //    if (gr == null || gr.Color!=enemycolor || gr.CountLiberties!=1)
            //        continue;

            //    ps.Add(gr.Liberties);
            //}

            return ps.Points;
        }

        private List<Point> GetPointsToTrySave(Group group, Game game, int maxliberties)
        {
            PointSet ps = new PointSet();

            Color enemycolor;

            if (group.Color == Color.White)
                enemycolor = Color.Black;
            else if (group.Color == Color.Black)
                enemycolor = Color.White;
            else
                throw new InvalidOperationException();

            //if (group.Count <= initialsize + 10)
            //{

                PointSet frontier = group.CalculateFrontier(game.Position);

                foreach (Point pt in frontier.Points)
                {
                    Group gr = game.GetGroup(pt.X, pt.Y);

                    if (gr != null && gr.Color == enemycolor && gr.CountLiberties <= group.CountLiberties)
                        ps.Add(gr.Liberties);
                }
            //}

            ps.Add(group.Liberties);

            return ps.Points;
        }

        public bool MoveCanSave(Game game, short xtosave, short ytosave, int maxliberties, Move move)
        {
            if (!game.IsValid(move))
                return false;

            Game newgame = game.Clone();
            newgame.Play(move);

            Group group = newgame.GetGroup(xtosave, ytosave);

            if (group.CountLiberties > maxliberties)
                return true;

            if (group.CountLiberties > 2 && group.Count > initialsize + 10)
                return true;

            return !CanKill(newgame, xtosave, ytosave, maxliberties);
        }

        public bool CanSave(Game game, short xtosave, short ytosave, int maxliberties)
        {
            if (saved.ContainsKey(game.Position))
                return saved[game.Position];

            Group group = game.GetGroup(xtosave, ytosave);

            if (group == null)
            {
                saved[game.Position] = false;
                return false;
            }

            if (initialsize == 0)
                initialsize = group.Count;

            if (group.CountLiberties > maxliberties)
            {
                saved[game.Position] = true;
                return true;
            }

            foreach (Point pt in GetPointsToTrySave(group,game,maxliberties))
            {
                Move move = new Move(pt.X, pt.Y, group.Color);

                if (MoveCanSave(game, xtosave, ytosave, maxliberties, move))
                {
                    saved[game.Position] = true;
                    return true;
                }
            }

            bool result = !CanKill(game,xtosave,ytosave,maxliberties);
            saved[game.Position] = result;
            return result;
        }

        public bool MoveCanKill(Game game, short xtokill, short ytokill, int maxliberties, Move move)
        {
            if (!game.IsValid(move))
                return false;

            Game newgame = game.Clone();
            newgame.Play(move);

            Group group = newgame.GetGroup(xtokill, ytokill);

            if (group == null)
                return true;

            if (group.CountLiberties > maxliberties)
                return false;

            if (group.CountLiberties > 2 && group.Count > initialsize + 10)
                return false;

            return !CanSave(newgame, xtokill, ytokill, maxliberties);
        }

        public bool CanKill(Game game, short xtokill, short ytokill, int maxliberties)
        {
            if (killed.ContainsKey(game.Position))
                return killed[game.Position];

            Group group = game.GetGroup(xtokill, ytokill);

            if (group == null)
            {
                killed[game.Position] = true;
                return true;
            }

            if (initialsize == 0)
                initialsize = group.Count;

            Color color;

            if (group.Color == Color.Black)
                color = Color.White;
            else if (group.Color == Color.White)
                color = Color.Black;
            else
                throw new InvalidOperationException();

            foreach (Point pt in GetPointsToTryKill(group,game))
            {
                Move move = new Move(pt.X, pt.Y, color);

                if (MoveCanKill(game, xtokill, ytokill, maxliberties, move))
                {
                    killed[game.Position] = true;
                    return true;
                }
            }

            killed[game.Position] = false;
            return false;
        }

        public List<Move> GetKillMoves(Game game, short xtokill, short ytokill, int maxliberties)
        {
            List<Move> moves = new List<Move>();

            Group group = game.GetGroup(xtokill, ytokill);

            if (group == null)
                throw new InvalidOperationException();

            Color color;

            if (group.Color==Color.Black)
                color = Color.White;
            else if (group.Color == Color.White)
                color = Color.Black;
            else
                throw new InvalidOperationException();

            foreach (Point pt in GetPointsToTryKill(group,game))
            {
                Move move = new Move(pt.X, pt.Y, color);

                if (MoveCanKill(game, xtokill, ytokill, maxliberties, move))
                    moves.Add(move);
            }

            return moves;
        }

        public List<Move> GetSaveMoves(Game game, short xtosave, short ytosave, int maxliberties)
        {
            List<Move> moves = new List<Move>();

            Group group = game.GetGroup(xtosave, ytosave);

            if (group == null)
                throw new InvalidOperationException();

            foreach (Point pt in GetPointsToTrySave(group,game,maxliberties))
            {
                Move move = new Move(pt.X, pt.Y, group.Color);

                if (MoveCanSave(game, xtosave, ytosave, maxliberties, move))
                    moves.Add(move);
            }

            return moves;
        }
    }
}
