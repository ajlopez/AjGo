using System;
using System.Collections.Generic;
using System.Text;

using AjGo.Evaluators;

namespace AjGo.Agents
{
    public class NazgulAgent
    {
        private Dictionary<Position, bool> killed = new Dictionary<Position, bool>();
        private int initialsize;
        private Move lastmove;

        private List<Point> GetPointsToTryKill(short xtokill, short ytokill, Game game)
        {
            GroupSet zone = game.GetZone(xtokill, ytokill);
            Color color = game.GetColor(xtokill, ytokill);
            Color enemycolor;

            if (color == Color.White)
                enemycolor = Color.Black;
            else if (color == Color.Black)
                enemycolor = Color.White;
            else
                throw new InvalidOperationException();

            PointSet killpoints = new PointSet();
            PointSet killpoints2 = new PointSet();
            PointSet liberties2 = new PointSet();
            PointSet liberties = new PointSet();

            PointSet ps = new PointSet();

            foreach (Group group in zone.Groups)
                foreach (Group group2 in group.Neighbours.Groups)
                    if (group2.Color == enemycolor)
                    {
                        foreach (Group n in group2.Neighbours.Groups)
                            if (n.Color == color && n.CountLiberties <= 2)
                                killpoints2.Add(n.Liberties);

                        if (group2.CountLiberties <= 2)
                            killpoints.Add(group2.Liberties);
                    }

            foreach (Group group in zone.Groups)
                if (group.Color == color)
                    liberties2.Add(group.Liberties.CalculateLiberties(game.Position));

            foreach (Group group in zone.Groups)
                if (group.Color == color)
                    liberties.Add(group.Liberties);

            // Liberties in red

            //foreach (Point pt in liberties.Points)
            //    if (game.ColoredPosition.GetColor(pt.X, pt.Y) == Color.Red)
            //        ps.Add(pt);

            // First, liberties of the last move

            if (lastmove != null)
            {
                PointSet pt = new PointSet();
                pt.Add(lastmove.Point);
                PointSet frontier = pt.CalculateFrontier(game.Position);
                ps.Add(PointSet.Intersect(frontier, liberties));
            }

            // First, liberties of group in danger

            GroupSet tokill = new GroupSet();

            foreach (Group group in zone.Groups)
            {
                if (group.Color == color)
                {
                    tokill.Add(group);
                    foreach (Group n in group.Neighbours.Groups)
                        if (n.Color == enemycolor)
                            foreach (Group n2 in n.Neighbours.Groups)
                                if (n2.Color == color)
                                    tokill.Add(n2);
                }
            }

            foreach (Group group in tokill.Groups)
                if (group.CountLiberties == 1)
                    ps.Add(group.Liberties);

            foreach (Group group in tokill.Groups)
                if (group.CountLiberties == 2)
                    ps.Add(group.Liberties);

            // First, save group


            //// First, liberties of group in danger 2

            //foreach (Group group in zone.Groups)
            //    if (group.Color == color && group.CountLiberties == 2)
            //        ps.Add(group.Liberties);


            // First, try first liberties

            ps.Add(liberties);

            //ps.Add(killpoints);
            //ps.Add(killpoints2);

            // Then, second liberties

            ps.Add(liberties2);

            // Then, kill points

            ps.Add(killpoints);
            ps.Add(killpoints2);

            return ps.Points;
        }

        private List<Point> GetPointsToTrySave(short xtosave, short ytosave, Game game)
        {
            GroupSet zone = game.GetZone(xtosave, ytosave);
            Color color = game.GetColor(xtosave, ytosave);
            Color enemycolor;

            if (color == Color.White)
                enemycolor = Color.Black;
            else if (color == Color.Black)
                enemycolor = Color.White;
            else
                throw new InvalidOperationException();

            PointSet killpoints = new PointSet();
            PointSet liberties2 = new PointSet();
            PointSet liberties = new PointSet();

            foreach (Group group in zone.Groups)
                foreach (Group group2 in group.Neighbours.Groups)
                    if (group2.Color == enemycolor && group2.CountLiberties <= 2)
                        killpoints.Add(group2.Liberties);

            PointSet ps = new PointSet();

            foreach (Group group in zone.Groups)
                if (group.Color == color)
                    liberties2.Add(group.Liberties.CalculateLiberties(game.Position));

            foreach (Group group in zone.Groups)
                if (group.Color == color)
                    liberties.Add(group.Liberties);

            // First, try kill points

            ps.Add(killpoints);

            // Then, try pure second liberties

            ps.Add(PointSet.Difference(liberties2, liberties));

            // Then liberties

            ps.Add(liberties);

            return ps.Points;
        }

        public bool MoveCanSave(Game game, short xtosave, short ytosave, Move move)
        {
            if (!game.IsValid(move))
                return false;

            Game newgame = game.Clone();
            newgame.Play(move);
            lastmove = move;

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

            killed[game.Position] = true;

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

            killed.Remove(game.Position);

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
            lastmove = move;

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
