using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Agents
{
    public class KillStrategy
    {
        private static Dictionary<Position, List<Move>> processed;

        public static void Initialize()
        {
            processed = new Dictionary<Position, List<Move>>();
        }

        private static void AddMoves(List<Move> moves1, List<Move> moves2, short nmoves)
        {
            foreach (Move m in moves2)
            {
                if (moves1.Count >= nmoves)
                    return;

                if (!moves1.Contains(m))
                    moves1.Add(m);
            }
        }

        public static List<Move> Kill(Game game, short xtokill, short ytokill, short nmoves, short level)
        {
            if (processed.ContainsKey(game.Position))
                return processed[game.Position];

            List<Move> moves = new List<Move>();
            Group gp = game.GetGroup(xtokill, ytokill);

            if (gp.CountLiberties == 1)
            {
                Liberty1KillAgent agent = new Liberty1KillAgent(game, xtokill, ytokill);
                AddMoves(moves, agent.Process(), nmoves);
            }

            if (gp.CountLiberties == 2)
            {
                Liberty2KillAgent agent = new Liberty2KillAgent(game, xtokill, ytokill);
                AddMoves(moves, agent.Process((short) (nmoves - moves.Count), level), nmoves);
            }

            //if (gp.CountLiberties == 3)
            //{
            //    Liberty3KillAgent agent = new Liberty3KillAgent(game, xtokill, ytokill);
            //    AddMoves(moves, agent.Process((short)(nmoves - moves.Count), (short)(level + 2)), nmoves);
            //}

            //if (gp.CountLiberties > 3 && level == 0)
            //{
            //    Liberty2KillAgent agent = new Liberty2KillAgent(game, xtokill, ytokill);
            //    AddMoves(moves, agent.Process((short)(nmoves - moves.Count), level), nmoves);
            //}

            processed[game.Position] = moves;

            return moves;
        }
    }
}

