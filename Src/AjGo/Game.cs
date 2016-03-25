using System;
using System.Collections.Generic;
using System.Text;

namespace AjGo
{
    public class Game
    {
        private Position position;
        private List<Move> moves;
        private GroupPosition groupposition;
        private Position coloredposition;
        private GroupPosition coloredgroupposition;
        private ColorCount colorcount;
        private int deadblacks;
        private int deadwhites;

        public delegate void NewGameEventHandler(object sender, Game g);

        public event NewGameEventHandler NewGame;

        public Game() : this(19, 19) { }

        public Game(short width, short height)
        {
            position = new Position(width, height);
            moves = new List<Move>();
            groupposition = new GroupPosition(position);
            groupposition.CalculateGroups();
            colorcount = position.CountColors();
        }

        public Game(Position pos)
        {
            position = pos;
            moves = new List<Move>();
            colorcount = position.CountColors();
        }

        public Game(Game g)
        {
            position = g.position.Clone();
            colorcount = position.CountColors();
            deadblacks = g.deadblacks;
            deadwhites = g.deadwhites;
            moves = new List<Move>();
            moves.AddRange(g.moves);
            this.NewGame += g.NewGameEvent;
        }

        public int Blacks
        {
            get { return colorcount.Blacks; }
        }

        public int Whites
        {
            get { return colorcount.Whites; }
        }

        public int DeadBlacks
        {
            get { return deadblacks; }
        }

        public int DeadWhites
        {
            get { return deadwhites; }
        }

        private void CalculateGroupPosition()
        {
            groupposition = new GroupPosition(position);
            groupposition.CalculateGroups();
        }

        private void CalculateColoredGroupPosition()
        {
            coloredposition = position.Clone();
            coloredposition.CalculateColors();
            coloredgroupposition = new GroupPosition(coloredposition);
            coloredgroupposition.CalculateGroups();
            coloredgroupposition.CalculateNeighbours();
        }

        private void MarkModified()
        {
            groupposition = null;
            coloredposition = null;
            coloredgroupposition = null;
        }

        public void NewGameEvent(object sender, Game game)
        {
            if (NewGame != null)
                NewGame(this, game);
        }

        public List<Group> Groups
        {
            get {
                if (groupposition == null)
                    CalculateGroupPosition();

                return groupposition.Groups;
            }
        }

        public List<Group> ColoredGroups
        {
            get
            {
                if (coloredgroupposition == null)
                    CalculateColoredGroupPosition();

                return coloredgroupposition.Groups;
            }
        }

        public List<Move> Moves
        {
            get { return moves; }
        }

        public Move GetLastMove()
        {
            if (moves == null)
                return null;
            if (moves.Count == 0)
                return null;
            return moves[moves.Count - 1];
        }

        public Position Position
        {
            get { return position; }
        }

        public Position ColoredPosition
        {
            get
            {
                if (coloredposition == null)
                    CalculateColoredGroupPosition();

                return coloredposition;
            }
        }

        public Group GetGroup(short x, short y)
        {
            if (groupposition == null)
                CalculateGroupPosition();

            return groupposition.GetGroup(x, y);
        }

        public GroupSet GetZone(short x, short y)
        {
            if (coloredgroupposition == null)
                CalculateColoredGroupPosition();

            return coloredgroupposition.GetZone(x,y);
        }

        public List<GroupSet> Zones
        {
            get
            {
                if (coloredgroupposition == null)
                    CalculateColoredGroupPosition();

                return coloredgroupposition.Zones;
            }
        }

        public Group GetZoneGroup(short x, short y)
        {
            if (coloredgroupposition == null)
                CalculateColoredGroupPosition();

            return coloredgroupposition.GetZoneGroup(GetColoredGroup(x, y));
        }

        public void Play(short x, short y, Color c)
        {
            Play(new Move(x, y, c));
        }

        public void Play(Move m)
        {
            position.Play(m);

            if (groupposition == null)
                CalculateGroupPosition();
            else
            {
                groupposition.ReviewCell(m.Point.X, m.Point.Y);

                // TODO Calculate liberties on reviewed cell
                groupposition.CalculateLiberties();
            }
    
            List<Group> killed = new List<Group>();

            foreach (Group gr in groupposition.Groups)
                if (gr.Color != m.Color && gr.CountLiberties == 0)
                    killed.Add(gr);

            foreach (Group gr in killed)
            {
                if (gr.Color == Color.Black)
                    deadblacks += gr.Count;

                if (gr.Color == Color.White)
                    deadwhites += gr.Count;

                groupposition.KillGroup(gr);
            }

            if (m.Color==Color.White || m.Color==Color.Black)
                moves.Add(m);

            MarkModified();

            colorcount = position.CountColors();

            if (NewGame != null)
                NewGame(this, this);
        }

        public Color GetColor(short x, short y)
        {
            return position.GetColor(x, y);
        }

        public bool IsEmpty(short x, short y)
        {
            return position.IsEmpty(x, y);
        }

        public bool IsRepeated(Move m)
        {
            foreach (Move move in moves)
                if (move.Color == m.Color && move.Point.Equals(m.Point))
                    return true;

            return false;
        }

        public Game Clone()
        {
            return new Game(this);
        }

        private bool IsCompatibleGroup(short x, short y, Color c)
        {
            Color color = position.GetColor(x, y);

            if (color == Color.Empty)
                return true;

            if (color == Color.Border)
                return false;

            if (groupposition == null)
                CalculateGroupPosition();

            Group gr = groupposition.GetGroup(x, y);

            if (gr == null)
                return true;

            if (gr.Color == c)
                return (gr.CountLiberties > 1);

            if (gr.CountLiberties > 1)
                return false;

            if (gr.Count > 1)
                return true;

            if (moves.Count==0)
                return true;

            Move lastmove = moves[moves.Count-1];

            if (lastmove.Color == c)
                return true;

            if (lastmove.Point.X != x || lastmove.Point.Y != y)
                return true;

            // it was ko

            return false;
        }

        public bool IsValid(short x, short y, Color mcolor)
        {
            Color color = position.GetColor(x, y);

            if (color != Color.Empty)
                return false;

            if (IsCompatibleGroup((short) (x - 1), y, mcolor))
                return true;
            if (IsCompatibleGroup((short) (x + 1), y, mcolor))
                return true;
            if (IsCompatibleGroup(x, (short) (y - 1), mcolor))
                return true;
            if (IsCompatibleGroup(x, (short) (y + 1), mcolor))
                return true;

            return false;
        }

        public bool IsValid(Move m)
        {
            return IsValid(m.Point.X, m.Point.Y, m.Color);
        }

        public Group GetColoredGroup(short x, short y)
        {
            if (coloredgroupposition == null)
                CalculateColoredGroupPosition();

            return coloredgroupposition.GetGroup(x, y);
        }
    }
}

