using System;
using System.Collections.Generic;
using System.Text;

using AjGo.Evaluators;

namespace AjGo.Agents
{
    public class RipperAgent
    {
        private Dictionary<Position, bool> killed = new Dictionary<Position, bool>();
        private int initialsize;

        private List<Point> GetPointsToTryKill(short xtokill, short ytokill, Game game)
        {
            GroupSet zone = game.GetZone(xtokill, ytokill);
            Color color = game.GetColor(xtokill, ytokill);

            PointSet liberties = new PointSet();
            PointSet liberties2 = new PointSet();
            PointSet danger = new PointSet();

            foreach (Group group in zone.Groups)
            {
                if (group.Color == color && group.CountLiberties <= 2)
                    danger.Add(group.Liberties);
            }

            foreach (Group group in zone.Groups)
                if (group.Color == color)
                    liberties2.Add(group.Liberties.CalculateLiberties(game.Position));

            foreach (Group group in zone.Groups)
                if (group.Color == color)
                    liberties.Add(group.Liberties);

            PointSet ps = new PointSet();

            ps.Add(danger);
            ps.Add(liberties);
            ps.Add(liberties2);

            return ps.Points;
        }

        private List<Point> GetPointsToTrySave(short xtosave, short ytosave, Game game)
        {
            GroupSet zone = game.GetZone(xtosave, ytosave);
            Color color = game.GetColor(xtosave, ytosave);
            Color enemycolor = Utilities.EnemyColor(color);

            PointSet liberties = new PointSet();
            PointSet liberties2 = new PointSet();
            PointSet danger = new PointSet();

            foreach (Group group in zone.Groups)
            {
                if (group.Color == color)
                    liberties2.Add(group.Liberties.CalculateLiberties(game.Position));
                foreach (Group n in group.Neighbours.Groups)
                    if (n.Color == enemycolor && n.CountLiberties <= 2)
                        danger.Add(n.Liberties);
            }

            foreach (Group group in zone.Groups)
                if (group.Color == color)
                    liberties.Add(group.Liberties);

            PointSet ps = new PointSet();

            ps.Add(danger);
            ps.Add(liberties);
            ps.Add(liberties2);

            return ps.Points;
        }

        public bool MoveCanSave(Game game, short xtosave, short ytosave, Move move)
        {
            if (!game.IsValid(move))
                return false;

            Game newgame = game.Clone();
            newgame.Play(move);

            GroupSet zone = newgame.GetZone(xtosave, ytosave);

            ZoneEvaluation evaluation = (new ZoneEvaluator()).Evaluate(zone, game.Position);

            if (evaluation.IsSafe)
            {
                killed[newgame.Position] = false;
                return true;
            }

            return !CanKill(newgame, xtosave, ytosave);
        }

        public bool CanSave(Game game, short xtosave, short ytosave)
        {
            if (killed.ContainsKey(game.Position))
                return !killed[game.Position];

            // Provisory

            //killed[game.Position] = false;

            Group group = game.GetGroup(xtosave, ytosave);

            if (group == null)
            {
                killed[game.Position] = true;
                return false;
            }

            foreach (Point pt in GetPointsToTrySave(xtosave,ytosave,game))
            {
                Move move = new Move(pt.X, pt.Y, group.Color);

                if (MoveCanSave(game, xtosave, ytosave, move))
                {
                    killed[game.Position] = false;
                    return true;
                }
            }

            bool result = !CanKill(game,xtosave,ytosave);
            killed[game.Position] = !result;

            if (result)
                return true;

            return result;
        }

        public bool MoveCanKill(Game game, short xtokill, short ytokill, Move move)
        {
            if (!game.IsValid(move))
                return false;

            Game newgame = game.Clone();
            newgame.Play(move);

            if (killed.ContainsKey(newgame.Position))
                return killed[newgame.Position];

            Group group = newgame.GetGroup(xtokill, ytokill);

            if (group == null)
            {
                killed[newgame.Position] = true;
                return true;
            }

            GroupSet zone = newgame.GetZone(xtokill, ytokill);

            ZoneEvaluation evaluation = (new ZoneEvaluator()).Evaluate(zone,game.Position);

            if (initialsize == 0)
                initialsize = evaluation.StoneSize;

            if (evaluation.GreenLife > 0 || evaluation.IsSafe)
            {
                killed[newgame.Position] = false;
                return false;
            }

            return !CanSave(newgame, xtokill, ytokill);
        }

        public bool CanKill(Game game, short xtokill, short ytokill)
        {
            if (killed.ContainsKey(game.Position))
                return killed[game.Position];

            // Provisory

            killed[game.Position] = false;

            Group group = game.GetGroup(xtokill, ytokill);

            if (group == null)
            {
                killed[game.Position] = true;
                return true;
            }

            GroupSet zone = game.GetZone(xtokill, ytokill);

            ZoneEvaluation evaluation = (new ZoneEvaluator()).Evaluate(zone, game.Position);

            if (initialsize == 0)
                initialsize = evaluation.StoneSize;

            //if (evaluation.IsSafe || evaluation.StoneSize>initialsize*2)
            if (evaluation.IsSafe)
            {
                killed[game.Position] = false;
                return false;
            }

            Color color;

            if (group.Color == Color.Black)
                color = Color.White;
            else if (group.Color == Color.White)
                color = Color.Black;
            else
                throw new InvalidOperationException();

            foreach (Point pt in GetPointsToTryKill(xtokill,ytokill,game))
            {
                Move move = new Move(pt.X, pt.Y, color);

                if (MoveCanKill(game, xtokill, ytokill, move))
                {
                    killed[game.Position] = true;
                    return true;
                }
            }

            killed[game.Position] = false;
            return false;
        }

        public List<Move> GetKillMoves(Game game, short xtokill, short ytokill)
        {
            List<Move> moves = new List<Move>();

            Group group = game.GetGroup(xtokill, ytokill);

            if (group == null)
                throw new InvalidOperationException();

            GroupSet zone = game.GetZone(xtokill, ytokill);

            ZoneEvaluation evaluation = (new ZoneEvaluator()).Evaluate(zone, game.Position);

            if (initialsize == 0)
                initialsize = evaluation.StoneSize;

            Color color;

            if (group.Color==Color.Black)
                color = Color.White;
            else if (group.Color == Color.White)
                color = Color.Black;
            else
                throw new InvalidOperationException();

            foreach (Point pt in GetPointsToTryKill(xtokill,ytokill,game))
            {
                Move move = new Move(pt.X, pt.Y, color);

                if (MoveCanKill(game, xtokill, ytokill, move))
                {
                    moves.Add(move);
                    return moves;
                }
            }

            return moves;
        }

        public List<Move> GetSaveMoves(Game game, short xtosave, short ytosave)
        {
            List<Move> moves = new List<Move>();

            Group group = game.GetGroup(xtosave, ytosave);

            if (group == null)
                throw new InvalidOperationException();

            foreach (Point pt in GetPointsToTrySave(xtosave,ytosave,game))
            {
                Move move = new Move(pt.X, pt.Y, group.Color);

                if (MoveCanSave(game, xtosave, ytosave, move))
                    moves.Add(move);
            }

            return moves;
        }
    }
}
