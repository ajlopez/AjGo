using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using AjGo.Agents;
using AjGo.Evaluators;

namespace AjGo.WinForm
{
    public partial class AjGoForm : Form
    {
        private BoardImage board;
        private Game game;
        private bool showadvance;
        private bool stepping;
        private Point selected;
        private Group selectedgroup;
        private Group selectedzone;

        enum Status {
            SelectGroup = 0,
            SetBlack = 1,
            SetWhite = 2,
            SetEmpty = 3,
            SelectZone = 4
        }

        private Status status = Status.SetBlack;

        public AjGoForm()
        {
            InitializeComponent();

            board = new BoardImage();

            ClearGame();
        }

        private void ClearGame()
        {
            game = new Game();

            board.DrawPosition(game);
            pbxBoard.Image = board.Image;

            game.NewGame += this.NewGame;
        }

        private void AjGoForm_Load(object sender, EventArgs e)
        {

        }

        private void pbxBoard_MouseDown(object sender, MouseEventArgs e)
        {
            int x;
            int y;

            x = (e.X - 5) / 20;
            y = (e.Y - 5) / 20;

            if (game.Position.GetColor(x, y) == Color.Border)
                return;

            if (status == Status.SetEmpty)
            {
                game.Play((short) x, (short) y, Color.Empty);
                //game.Position.SetColor(x, y, Color.Empty);
                //game = new Game(game.Position);
                //game.NewGame += this.NewGame;
                DrawBoard();
            }
            else if (status==Status.SetBlack || status==Status.SetWhite)
            {
                Color color = (status == Status.SetBlack) ? Color.Black : Color.White;

                Move move = new Move((short)x, (short)y, color);

                if (!game.IsValid(move))
                    return;

                game.Play(move);
                DrawBoard();
            }
            else if (status == Status.SelectGroup)
            {
                selected = new Point((short) x, (short) y);
                selectedgroup = game.GetGroup((short) x, (short) y);

                if (selectedgroup != null)
                {
                    board.DrawPosition(game);
                    board.DrawPointSet(selectedgroup, Brushes.Crimson, 5);
                    pbxBoard.Image = board.Image;
                    pbxBoard.Refresh();
                }
            }
            else if (status == Status.SelectZone)
            {
                selected = new Point((short)x, (short)y);

                selectedzone = game.GetZoneGroup((short)x,(short)y);

                if (selectedzone != null)
                {
                    board.DrawPosition(game);
                    board.DrawPointSet(selectedzone, Brushes.Crimson, 5);
                    pbxBoard.Image = board.Image;
                    pbxBoard.Refresh();
                }
            }
        }

        private void DrawBoard()
        {
            board.DrawPosition(game);        
            pbxBoard.Image = board.Image;
            pbxBoard.Refresh();
        }

        private void SetStatus(Status sta)
        {
            status = sta;
            switch (sta)
            {
                case Status.SetEmpty:
                    toolStatusLabel.Text = "Set Empty Cell";
                    break;
                case Status.SetBlack:
                    toolStatusLabel.Text = "Set Black Stone";
                    break;
                case Status.SetWhite:
                    toolStatusLabel.Text = "Set White Stone";
                    break;
                case Status.SelectGroup:
                    toolStatusLabel.Text = "Select Group";
                    break;
                case Status.SelectZone:
                    toolStatusLabel.Text = "Select Zone";
                    break;
            }
        }

        private void btnBlackStone_Click(object sender, EventArgs e)
        {
            SetStatus(Status.SetBlack);
        }

        private void btnWhiteStone_Click(object sender, EventArgs e)
        {
            SetStatus(Status.SetWhite);
        }

        private void btnEmptyStone_Click(object sender, EventArgs e)
        {
            SetStatus(Status.SetEmpty);
        }

        private int ngames;

        private void NewGame(object sender, Game game)
        {
            ngames++;

            if (ngames == 100)
            {
                ngames = 0;
                Application.DoEvents();
            }

            if (!showadvance)
                return;

            board.DrawPosition(game);
            pbxBoard.Image = board.Image;
            pbxBoard.Refresh();

            if (stepping)
                if (MessageBox.Show("Game in Process", "Exploring", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    throw new Exception();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            List<Move> moves = new List<Move>();

            try
            {

                foreach (Group gp in game.Groups)
                {
                    if (gp.Color == Color.White)
                        moves.AddRange(KillGroup(gp));
                }

                if (moves.Count > 0)
                    game.Play(moves[0]);

                DrawBoard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<Move> KillGroup(Group gp)
        {
            Point p = gp.Points[0];

            KillStrategy.Initialize();
            SaveStrategy.Initialize();

            List<Move> moves = KillStrategy.Kill(game, p.X, p.Y, 1, 0);

            return moves;
        }

        private void ShowPosition(Position p)
        {
            Game gameold = game;
            game = new Game(p);
            DrawBoard();
            MessageBox.Show("OK");
            game = gameold;
        }

        private void btnColors_Click(object sender, EventArgs e)
        {
            board.DrawPosition(game);
            board.DrawColors(game.ColoredPosition);
            pbxBoard.Image = board.Image;
            pbxBoard.Refresh();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            List<Move> moves = new List<Move>();

            try
            {
                foreach (Group gp in game.Groups)
                {
                    if (gp.Color == Color.Black)
                        moves.AddRange(KillGroup(gp));
                }

                if (moves.Count > 0)
                    game.Play(moves[0]);

                DrawBoard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static Random rnd = new Random();

        private void Button9_Click(object sender, EventArgs e)
        {
            EvaluatorAgent agent = new EvaluatorAgent(game, Color.Black);
            try
            {
                List<Move> moves = agent.Process();
                if (moves.Count > 0)
                    game.Play(moves[rnd.Next(moves.Count - 1)]);
                board.DrawPosition(game);
                board.DrawColors(game.ColoredPosition);
                pbxBoard.Image = board.Image;
                pbxBoard.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            EvaluatorAgent agent = new EvaluatorAgent(game, Color.White);
            try
            {
                List<Move> moves = agent.Process();
                if (moves.Count > 0)
                    game.Play(moves[rnd.Next(moves.Count - 1)]);
                board.DrawPosition(game);
                board.DrawColors(game.ColoredPosition);
                pbxBoard.Image = board.Image;
                pbxBoard.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkShowWork_CheckedChanged(object sender, EventArgs e)
        {
            showadvance = chkShowWork.Checked;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (LoadPositionDialog.ShowDialog() != DialogResult.OK)
                return;

            PositionBuilder pb = new PositionBuilder();
            StreamReader reader = new StreamReader(LoadPositionDialog.FileName);

            pb.MakePosition(reader);
            Position position = pb.GetPosition();

            reader.Close();

            game = new Game(position);

            DrawBoard();

            game.NewGame += this.NewGame;
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetStatus(Status.SelectGroup);
        }

        private void libertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            selectedgroup = game.GetGroup(selected.X, selected.Y);

            if (selectedgroup == null)
                return;

            board.DrawPosition(game);
            board.DrawPointSet(selectedgroup.Liberties, Brushes.Crimson, 5);
            pbxBoard.Image = board.Image;
            pbxBoard.Refresh();
        }

        private void frontierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            selectedgroup = game.GetGroup(selected.X, selected.Y);

            if (selectedgroup == null)
                return;

            board.DrawPosition(game);
            board.DrawPointSet(selectedgroup.CalculateFrontier(game.Position), Brushes.Crimson, 5);
            pbxBoard.Image = board.Image;
            pbxBoard.Refresh();
        }

        private void selectZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetStatus(Status.SelectZone);
        }

        private void libertiesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            selectedzone = game.GetZoneGroup(selected.X, selected.Y);

            if (selectedzone == null)
                return;

            board.DrawPosition(game);
            board.DrawPointSet(selectedzone.Liberties, Brushes.Crimson, 5);
            pbxBoard.Image = board.Image;
            pbxBoard.Refresh();
        }

        private void frontierToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            selectedzone = game.GetZoneGroup(selected.X, selected.Y);

            if (selectedzone == null)
                return;

            board.DrawPosition(game);
            board.DrawPointSet(selectedzone.CalculateFrontier(game.Position), Brushes.Crimson, 5);
            pbxBoard.Image = board.Image;
            pbxBoard.Refresh();
        }

        private void chkStep_CheckedChanged(object sender, EventArgs e)
        {
            stepping = chkStep.Checked;
        }

        private void MatchMoves(string name, Point lastpoint)
        {
            Move lastmove = game.GetLastMove();
            Position position;
            Color color;

            if (lastmove != null && lastmove.Color == Color.Black)
            {
                color = Color.White;
                position = game.Position.InvertedClone();
            }
            else
            {
                color = Color.Black;
                position = game.Position;
            }

            List<MatchResult> results = Matches.GetResults(position,name,lastpoint);

            PointSet points = new PointSet();

            foreach (MatchResult result in results)
                points.Add(result.Point);

            board.DrawPosition(game);
            board.DrawPointSet(points, Brushes.Crimson, 5);
            pbxBoard.Image = board.Image;
            pbxBoard.Refresh();

            if (color == Color.White)
                SetStatus(Status.SetWhite);
            else
                SetStatus(Status.SetBlack);
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            MatchMoves(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Matches.LoadMatches();
        }

        private void evaluateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            GroupSet zone = game.GetZone(selected.X, selected.Y);

            if (zone == null)
                return;

            ZoneEvaluation evaluation = (new ZoneEvaluator()).Evaluate(zone, game.ColoredPosition);

            ZoneEvaluationForm form = new ZoneEvaluationForm(evaluation);
            form.ShowDialog();
        }

        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            try
            {
                SimpleKillSaveAgent agent = new SimpleKillSaveAgent();
                Group group = game.GetGroup(selected.X, selected.Y);

                List<Move> moves = agent.GetKillMoves(game, selected.X, selected.Y, group.CountLiberties);

                PointSet points = new PointSet();

                foreach (Move move in moves)
                    points.Add(move.Point);

                board.DrawPosition(game);
                board.DrawPointSet(points, Brushes.Crimson, 5);
                pbxBoard.Image = board.Image;
                pbxBoard.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (SavePositionDialog.ShowDialog() != DialogResult.OK)
                    return;

                TextWriter writer = new StreamWriter(SavePositionDialog.FileName);

                PositionBuilder.SavePosition(writer, game.Position);

                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void newPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearGame();
        }

        private void nazgulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            try
            {
                NazgulAgent agent = new NazgulAgent();

                List<Move> moves = agent.GetKillMoves(game, selected.X, selected.Y);

                PointSet points = new PointSet();

                foreach (Move move in moves)
                    points.Add(move.Point);

                board.DrawPosition(game);
                board.DrawPointSet(points, Brushes.Crimson, 5);
                pbxBoard.Image = board.Image;
                pbxBoard.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ZoneGoal(Goal goal)
        {
            if (selected == null)
                return;

            ZoneEvaluatorAgent agent = new ZoneEvaluatorAgent(game, selected.X, selected.Y, goal);

            try
            {
                List<Move> moves = agent.Process();

                PointSet points = new PointSet();

                foreach (Move move in moves)
                    points.Add(move.Point);

                board.DrawPosition(game);
                board.DrawPointSet(points, Brushes.Crimson, 5);
                pbxBoard.Image = board.Image;
                pbxBoard.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoneGoal(Goal.Extend);
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoneGoal(Goal.Connect);
        }

        private void eyesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ZoneGoal(Goal.Eyes);
        }

        private void escapeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoneGoal(Goal.Escape);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoneGoal(Goal.Cut);
        }

        private void surrenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoneGoal(Goal.Surrender);
        }

        private void ripperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            RipperAgent agent = new RipperAgent();

            try
            {
                List<Move> moves = agent.GetKillMoves(game, selected.X, selected.Y);

                PointSet points = new PointSet();

                foreach (Move move in moves)
                    points.Add(move.Point);

                board.DrawPosition(game);
                board.DrawPointSet(points, Brushes.Crimson, 5);
                pbxBoard.Image = board.Image;
                pbxBoard.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simple2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            Simple4Agent agent = new Simple4Agent();
            Group group = game.GetGroup(selected.X, selected.Y);

            try
            {
                List<Move> moves = agent.GetKillMoves(game, selected.X, selected.Y, group.CountLiberties + 1);

                PointSet points = new PointSet();

                foreach (Move move in moves)
                    points.Add(move.Point);

                board.DrawPosition(game);
                board.DrawPointSet(points, Brushes.Crimson, 5);
                pbxBoard.Image = board.Image;
                pbxBoard.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DrawPointSet(PointSet points)
        {
            board.DrawPosition(game);
            board.DrawPointSet(points, Brushes.Crimson, 5);
            pbxBoard.Image = board.Image;
            pbxBoard.Refresh();
        }

        private void simple3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            Simple3Agent agent = new Simple3Agent();
            Group group = game.GetGroup(selected.X, selected.Y);

            try
            {
                List<Move> moves = agent.GetKillMoves(game, selected.X, selected.Y, group.CountLiberties + 1);

                PointSet points = new PointSet();

                foreach (Move move in moves)
                    points.Add(move.Point);

                board.DrawPosition(game);
                board.DrawPointSet(points, Brushes.Crimson, 5);
                pbxBoard.Image = board.Image;
                pbxBoard.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatchMoves(null, null);
        }

        private void lastMoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Move lastmove = game.GetLastMove();

            if (lastmove != null)
                MatchMoves(null, lastmove.Point);
            else
                MatchMoves(null, null);
        }

        private void connectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MatchMoves("Connect", null);
        }

        private void extendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatchMoves("Extend", null);
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MatchMoves("Cut", null);
        }

        private void cornerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatchMoves("Border", null);
        }

        private void EvaluateMovesWithMatches(IEvaluator evaluator)
        {
            Move lastmove = game.GetLastMove();
            Position position;
            Color color;

            if (lastmove != null && lastmove.Color == Color.Black)
            {
                color = Color.White;
                position = game.Position.InvertedClone();
            }
            else
            {
                color = Color.Black;
                position = game.Position;
            }

            List<MatchResult> results = Matches.GetResults(position, null, null);

            PointSet points = new PointSet();

            foreach (MatchResult result in results)
                points.Add(result.Point);

            GamesEvaluator eval = new GamesEvaluator();

            List<EvaluatedGame> games = eval.Evaluate(game, color, evaluator, points);

            PointSet ps = new PointSet();

            foreach (EvaluatedGame egame in games)
            {
                if (ps.Count < 10)
                    ps.Add(egame.Game.GetLastMove().Point);
            }

            DrawPointSet(ps);

            if (color == Color.Black)
                SetStatus(Status.SetBlack);
            else
                SetStatus(Status.SetWhite);
        }

        private void EvaluateMoves(IEvaluator evaluator)
        {
            GamesEvaluator eval = new GamesEvaluator();
            Move move = game.GetLastMove();

            Color color = Color.Black;

            if (move!=null && move.Color==Color.Black)
                color = Color.White;

            List<EvaluatedGame> games = eval.Evaluate(game, color, evaluator);

            PointSet ps = new PointSet();

            foreach (EvaluatedGame egame in games)
            {
                if (ps.Count < 10)
                    ps.Add(egame.Game.GetLastMove().Point);
            }

            DrawPointSet(ps);

            if (color == Color.Black)
                SetStatus(Status.SetBlack);
            else
                SetStatus(Status.SetWhite);
        }

        private void stonesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvaluateMoves(new StoneEvaluator());
        }

        private void byColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvaluateMoves(new ColorEvaluator());
        }

        private void byZonesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvaluateMoves(new ZoneGameEvaluator());
        }

        private void simpleXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            try
            {
                SimpleKillSaveAgent agent = new SimpleKillSaveAgent();
                Group group = game.GetGroup(selected.X, selected.Y);

                List<Move> moves = agent.GetKillMoves(game, selected.X, selected.Y, group.CountLiberties+1);

                PointSet points = new PointSet();

                foreach (Move move in moves)
                    points.Add(move.Point);

                board.DrawPosition(game);
                board.DrawPointSet(points, Brushes.Crimson, 5);
                pbxBoard.Image = board.Image;
                pbxBoard.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ZoneGameEvaluator zeval = new ZoneGameEvaluator();
            StoneEvaluator seval = new StoneEvaluator();
            ColorEvaluator ceval = new ColorEvaluator();

            txtStoneValue.Text = seval.Evaluate(game).Value.ToString();
            txtColorValue.Text = ceval.Evaluate(game).Value.ToString();
            txtZoneValue.Text = zeval.Evaluate(game).Value.ToString();
        }

        private void byZonesWithMatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvaluateMovesWithMatches(new ZoneGameEvaluator());
        }

        private void byColorsWithMatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvaluateMovesWithMatches(new ColorEvaluator());
        }

        private void frontierToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (selected == null)
                return;

            try
            {
                FrontierAgent agent = new FrontierAgent();

                List<Move> moves = agent.GetKillMoves(game, selected.X, selected.Y);

                PointSet points = new PointSet();

                foreach (Move move in moves)
                    points.Add(move.Point);

                board.DrawPosition(game);
                board.DrawPointSet(points, Brushes.Crimson, 5);
                pbxBoard.Image = board.Image;
                pbxBoard.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

