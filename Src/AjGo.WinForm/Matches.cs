using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AjGo.WinForm
{
    public class Matches
    {
        private static List<Match> matches;

        public static void LoadMatches()
        {
            DirectoryInfo di = null;

            if (Directory.Exists("Matches"))
                di = new DirectoryInfo("Matches");
            else if (Directory.Exists("../../Matches"))
                di = new DirectoryInfo("../../Matches");

            if (di == null)
                return;

            matches = new List<Match>();

            foreach (FileInfo fi in di.GetFiles("*.txt"))
            {
                MatchBuilder mb = new MatchBuilder();
                TextReader reader = new StreamReader(fi.FullName);
                mb.MakeMatch(reader);
                reader.Close();
                matches.Add(mb.GetMatch(fi.Name));
            }
        }

        public static List<MatchResult> GetResults(Position pos, string name, Point lastpoint)
        {            
            List<MatchResult> results = new List<MatchResult>();

            foreach (Match match in matches)
            {
                if (name != null && !match.IsName(name))
                    continue;

                List<MatchResult> rs = match.MatchMoves(pos,lastpoint);

                foreach (MatchResult result in rs)
                    if (lastpoint==null || result.HasLast)
                        results.Add(result);
            }

            return results;
        }
    }
}
