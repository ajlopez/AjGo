using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo.Agents
{
    public class SaveStrategy
    {
        private static Dictionary<Position, List<Move>> processed;
        private static int noprocessed;
        private static int nocached;

        public static void Initialize() {
            processed = new Dictionary<Position, List<Move>>();
            noprocessed = 0;
            nocached = 0;
        }

        public static int Processed { get { return noprocessed; } }
        public static int Cached { get { return nocached; } }

        public static List<Position> SavedPositions
        {
            get
            {
                List<Position> saved = new List<Position>();

                foreach (Position p in processed.Keys)
                    if (processed[p].Count > 0)
                        saved.Add(p);

                return saved;
            }
        }

        public static List<Move> Save(Game game, short xtosave, short ytosave, short nmoves, short level)
        {
            noprocessed++;

            if (processed.ContainsKey(game.Position))
            {
                nocached++;
                return processed[game.Position];
            }

            Group group = game.GetGroup(xtosave, ytosave);
            List<Move> moves;

            if (group.CountLiberties >= 2)
            {
                Liberty2SaveAgent agent = new Liberty2SaveAgent(game, xtosave, ytosave);
                moves = agent.Process(nmoves, (short)(level + 1));
            }
            else
            {
                SaveAgent agent = new SaveAgent(game, xtosave, ytosave);
                moves = agent.Process(nmoves, level);
            }

            processed[game.Position] = moves;

            return moves;
        }
    }
}

