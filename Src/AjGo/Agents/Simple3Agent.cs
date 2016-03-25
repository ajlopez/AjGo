using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Agents
{
    public class TreeGameNode
    {
        public TreeGameNode parent;
        public Game game;
        public Move move;
        public List<TreeGameNode> children = new List<TreeGameNode>();
        public bool goal;
    }

    public class Simple3Agent
    {
        private List<TreeGameNode> pending = new List<TreeGameNode>();
        private List<Move> moves = new List<Move>();

        private List<Point> GetPointsToTryKill(Group group, Game game)
        {
            PointSet ps = new PointSet();

            ps.Add(group.Liberties);
            ps.Add(group.Liberties.CalculateLiberties(game.Position));

            return ps.Points;
        }

        private List<Point> GetPointsToTrySave(Group group, Game game)
        {
            PointSet ps = new PointSet();

            ps.Add(group.Liberties);
            ps.Add(group.Liberties.CalculateLiberties(game.Position));

            return ps.Points;
        }

        public TreeGameNode MoveCanKill(Game game, short xtokill, short ytokill, int maxliberties, Move move)
        {
            if (!game.IsValid(move))
                return null;

            Game newgame = game.Clone();
            newgame.Play(move);

            TreeGameNode node = new TreeGameNode();
            node.game = game;
            node.move = move;

            Group group = newgame.GetGroup(xtokill, ytokill);

            if (group == null)
                node.goal = true;

            return node;
        }

        public TreeGameNode MoveCanSave(Game game, short xtosave, short ytosave, int maxliberties, Move move)
        {
            if (!game.IsValid(move))
                return null;

            Game newgame = game.Clone();
            newgame.Play(move);

            TreeGameNode node = new TreeGameNode();
            node.game = game;
            node.move = move;

            Group group = newgame.GetGroup(xtosave, ytosave);

            if (group.CountLiberties > maxliberties)
                node.goal = true;

            return node;
        }

        private void RemoveNode(TreeGameNode node)
        {
            pending.Remove(node);

            foreach (TreeGameNode child in node.children)
                RemoveNode(child);
        }

        private void FailNode(TreeGameNode node)
        {
            RemoveNode(node);

            if (node.parent != null)
            {
                node.parent.children.Remove(node);

                if (node.parent.children.Count == 0)
                    GoalNode(node.parent);
            }
        }

        private void GoalNode(TreeGameNode node)
        {
            RemoveNode(node);

            if (node.parent != null)
            {
                FailNode(node.parent);
                if (node.parent.parent == null)
                    moves.Add(node.move);
            }
            else
                moves.Add(node.move);
        }

        public void CanSave(TreeGameNode node, short xtosave, short ytosave, int maxliberties)
        {
            Group group = node.game.GetGroup(xtosave, ytosave);
            Color color = group.Color;

            foreach (Point pt in GetPointsToTrySave(group, node.game))
            {
                Move move = new Move(pt.X, pt.Y, color);

                if (node.game.IsValid(move))
                {
                    TreeGameNode child = new TreeGameNode();
                    Game newgame = node.game.Clone();
                    newgame.Play(move);

                    Group gr = newgame.GetGroup(xtosave, ytosave);

                    if (gr.CountLiberties > maxliberties)
                        FailNode(node);
                    else
                    {

                        child.parent = node;
                        child.game = newgame;
                        child.move = move;

                        node.children.Add(child);

                        pending.Add(child);
                    }
                }
            }
        }

        public void CanKill(TreeGameNode node, short xtokill, short ytokill, int maxliberties)
        {
            Group group = node.game.GetGroup(xtokill, ytokill);

            Color color = Utilities.EnemyColor(group.Color);

            foreach (Point pt in GetPointsToTryKill(group, node.game))
            {
                Move move = new Move(pt.X, pt.Y, color);

                if (node.game.IsValid(move))
                {
                    TreeGameNode child = new TreeGameNode();
                    Game newgame = node.game.Clone();
                    newgame.Play(move);

                    Group gr = newgame.GetGroup(xtokill, ytokill);

                    if (gr == null)
                    {
                        FailNode(node);
                        if (node.parent == null)
                            moves.Add(move);
                    }
                    else
                    {
                        child.parent = node;
                        child.game = newgame;
                        child.move = move;

                        node.children.Add(child);

                        pending.Add(child);
                    }
                }
            }
        }

        public List<Move> GetKillMoves(Game game, short xtokill, short ytokill, int maxliberties)
        {
            Group group = game.GetGroup(xtokill, ytokill);

            if (group == null)
                throw new InvalidOperationException();

            Color color = group.Color;

            TreeGameNode node = new TreeGameNode();
            node.game = game;

            CanKill(node, xtokill, ytokill, maxliberties);

            while (pending.Count > 0)
            {
                node = pending[0];
                pending.RemoveAt(0);

                if (node.move.Color != color)
                    CanSave(node, xtokill, ytokill, maxliberties);
                else
                    CanKill(node, xtokill, ytokill, maxliberties);
            }

            return moves;
        }
    }
}

