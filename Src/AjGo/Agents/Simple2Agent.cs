using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Agents
{
    public class Simple2Agent
    {
        private Dictionary<Position, bool> saved = new Dictionary<Position,bool>();
        private Dictionary<Position, bool> killed = new Dictionary<Position, bool>();
        private SimpleKillSaveAgent agent2 = new SimpleKillSaveAgent();

        int initialsize;

        private List<Point> GetPointsToTryKill(Group group, Game game)
        {
            PointSet ps = new PointSet();

            ps.Add(group.Liberties);
            ps.Add(group.Liberties.CalculateLiberties(game.Position));

            //PointSet frontier = group.Liberties.CalculateFrontier(game.Position);

            //ps.Add(frontier.CalculateLiberties(game.Position));

            return ps.Points;
        }

        public bool MoveCanSave(Game game, short xtosave, short ytosave, int maxliberties, Move move)
        {
            return agent2.MoveCanSave(game, xtosave, ytosave, maxliberties, move);
        }

        public bool CanSave(Game game, short xtosave, short ytosave, int maxliberties)
        {
            return agent2.CanSave(game, xtosave, ytosave, maxliberties);
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
            return agent2.GetSaveMoves(game, xtosave, ytosave, maxliberties);
        }
    }
}
