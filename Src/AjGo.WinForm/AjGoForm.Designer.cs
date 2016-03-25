namespace AjGo.WinForm
{
    partial class AjGoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbxBoard = new System.Windows.Forms.PictureBox();
            this.Button5 = new System.Windows.Forms.Button();
            this.Button9 = new System.Windows.Forms.Button();
            this.btnColors = new System.Windows.Forms.Button();
            this.btnEmptyStone = new System.Windows.Forms.Button();
            this.Button6 = new System.Windows.Forms.Button();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.btnWhiteStone = new System.Windows.Forms.Button();
            this.btnBlackStone = new System.Windows.Forms.Button();
            this.chkShowWork = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.libertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frontierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectZoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.libertiesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.frontierToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eyesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadPositionDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.chkStep = new System.Windows.Forms.CheckBox();
            this.btnMatch = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.evaluateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SavePositionDialog = new System.Windows.Forms.SaveFileDialog();
            this.newPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nazgulToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eyesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.escapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.surrenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ripperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simple2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simple3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastMoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.extendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cornerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stonesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byZonesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtZoneValue = new System.Windows.Forms.TextBox();
            this.txtColorValue = new System.Windows.Forms.TextBox();
            this.txtStoneValue = new System.Windows.Forms.TextBox();
            this.byZonesWithMatchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byColorsWithMatchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frontierToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBoard)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbxBoard
            // 
            this.pbxBoard.Location = new System.Drawing.Point(12, 41);
            this.pbxBoard.Name = "pbxBoard";
            this.pbxBoard.Size = new System.Drawing.Size(272, 240);
            this.pbxBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxBoard.TabIndex = 1;
            this.pbxBoard.TabStop = false;
            this.pbxBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbxBoard_MouseDown);
            // 
            // Button5
            // 
            this.Button5.Location = new System.Drawing.Point(403, 297);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(136, 24);
            this.Button5.TabIndex = 19;
            this.Button5.Text = "Play White";
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button9
            // 
            this.Button9.Location = new System.Drawing.Point(403, 271);
            this.Button9.Name = "Button9";
            this.Button9.Size = new System.Drawing.Size(136, 24);
            this.Button9.TabIndex = 18;
            this.Button9.Text = "Play Black";
            this.Button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // btnColors
            // 
            this.btnColors.Location = new System.Drawing.Point(403, 238);
            this.btnColors.Name = "btnColors";
            this.btnColors.Size = new System.Drawing.Size(136, 24);
            this.btnColors.TabIndex = 17;
            this.btnColors.Text = "&Colors";
            this.btnColors.Click += new System.EventHandler(this.btnColors_Click);
            // 
            // btnEmptyStone
            // 
            this.btnEmptyStone.Location = new System.Drawing.Point(403, 82);
            this.btnEmptyStone.Name = "btnEmptyStone";
            this.btnEmptyStone.Size = new System.Drawing.Size(136, 24);
            this.btnEmptyStone.TabIndex = 16;
            this.btnEmptyStone.Text = "Empt&y";
            this.btnEmptyStone.Click += new System.EventHandler(this.btnEmptyStone_Click);
            // 
            // Button6
            // 
            this.Button6.Location = new System.Drawing.Point(403, 169);
            this.Button6.Name = "Button6";
            this.Button6.Size = new System.Drawing.Size(136, 24);
            this.Button6.TabIndex = 15;
            this.Button6.Text = "&Evaluate";
            this.Button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(403, 136);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(136, 24);
            this.Button4.TabIndex = 14;
            this.Button4.Text = "Play White";
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(403, 112);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(136, 24);
            this.Button3.TabIndex = 13;
            this.Button3.Text = "Play Black";
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // btnWhiteStone
            // 
            this.btnWhiteStone.Location = new System.Drawing.Point(403, 58);
            this.btnWhiteStone.Name = "btnWhiteStone";
            this.btnWhiteStone.Size = new System.Drawing.Size(136, 24);
            this.btnWhiteStone.TabIndex = 12;
            this.btnWhiteStone.Text = "&White Stone";
            this.btnWhiteStone.Click += new System.EventHandler(this.btnWhiteStone_Click);
            // 
            // btnBlackStone
            // 
            this.btnBlackStone.Location = new System.Drawing.Point(403, 35);
            this.btnBlackStone.Name = "btnBlackStone";
            this.btnBlackStone.Size = new System.Drawing.Size(136, 24);
            this.btnBlackStone.TabIndex = 11;
            this.btnBlackStone.Text = "&Black Stone";
            this.btnBlackStone.Click += new System.EventHandler(this.btnBlackStone_Click);
            // 
            // chkShowWork
            // 
            this.chkShowWork.AutoSize = true;
            this.chkShowWork.Location = new System.Drawing.Point(403, 342);
            this.chkShowWork.Name = "chkShowWork";
            this.chkShowWork.Size = new System.Drawing.Size(82, 17);
            this.chkShowWork.TabIndex = 20;
            this.chkShowWork.Text = "Show Work";
            this.chkShowWork.UseVisualStyleBackColor = true;
            this.chkShowWork.CheckedChanged += new System.EventHandler(this.chkShowWork_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.groupToolStripMenuItem,
            this.zoneToolStripMenuItem,
            this.matchToolStripMenuItem,
            this.playToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(549, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.newPositionToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "&Load Position...";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "&Save Position...";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // groupToolStripMenuItem
            // 
            this.groupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectToolStripMenuItem,
            this.libertiesToolStripMenuItem,
            this.frontierToolStripMenuItem,
            this.killToolStripMenuItem});
            this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            this.groupToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.groupToolStripMenuItem.Text = "&Group";
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.selectToolStripMenuItem.Text = "&Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // libertiesToolStripMenuItem
            // 
            this.libertiesToolStripMenuItem.Name = "libertiesToolStripMenuItem";
            this.libertiesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.libertiesToolStripMenuItem.Text = "&Liberties";
            this.libertiesToolStripMenuItem.Click += new System.EventHandler(this.libertiesToolStripMenuItem_Click);
            // 
            // frontierToolStripMenuItem
            // 
            this.frontierToolStripMenuItem.Name = "frontierToolStripMenuItem";
            this.frontierToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.frontierToolStripMenuItem.Text = "&Frontier";
            this.frontierToolStripMenuItem.Click += new System.EventHandler(this.frontierToolStripMenuItem_Click);
            // 
            // zoneToolStripMenuItem
            // 
            this.zoneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectZoneToolStripMenuItem,
            this.libertiesToolStripMenuItem1,
            this.frontierToolStripMenuItem1,
            this.eyesToolStripMenuItem,
            this.evaluateToolStripMenuItem,
            this.goalToolStripMenuItem});
            this.zoneToolStripMenuItem.Name = "zoneToolStripMenuItem";
            this.zoneToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.zoneToolStripMenuItem.Text = "&Zone";
            // 
            // selectZoneToolStripMenuItem
            // 
            this.selectZoneToolStripMenuItem.Name = "selectZoneToolStripMenuItem";
            this.selectZoneToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.selectZoneToolStripMenuItem.Text = "&Select";
            this.selectZoneToolStripMenuItem.Click += new System.EventHandler(this.selectZoneToolStripMenuItem_Click);
            // 
            // libertiesToolStripMenuItem1
            // 
            this.libertiesToolStripMenuItem1.Name = "libertiesToolStripMenuItem1";
            this.libertiesToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.libertiesToolStripMenuItem1.Text = "&Liberties";
            this.libertiesToolStripMenuItem1.Click += new System.EventHandler(this.libertiesToolStripMenuItem1_Click);
            // 
            // frontierToolStripMenuItem1
            // 
            this.frontierToolStripMenuItem1.Name = "frontierToolStripMenuItem1";
            this.frontierToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.frontierToolStripMenuItem1.Text = "&Frontier";
            this.frontierToolStripMenuItem1.Click += new System.EventHandler(this.frontierToolStripMenuItem1_Click);
            // 
            // eyesToolStripMenuItem
            // 
            this.eyesToolStripMenuItem.Name = "eyesToolStripMenuItem";
            this.eyesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.eyesToolStripMenuItem.Text = "&Eyes";
            // 
            // LoadPositionDialog
            // 
            this.LoadPositionDialog.FileName = "openFileDialog1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 442);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(549, 22);
            this.statusStrip1.TabIndex = 22;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStatusLabel
            // 
            this.toolStatusLabel.Name = "toolStatusLabel";
            this.toolStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // chkStep
            // 
            this.chkStep.AutoSize = true;
            this.chkStep.Location = new System.Drawing.Point(403, 365);
            this.chkStep.Name = "chkStep";
            this.chkStep.Size = new System.Drawing.Size(87, 17);
            this.chkStep.TabIndex = 23;
            this.chkStep.Text = "Step by Step";
            this.chkStep.UseVisualStyleBackColor = true;
            this.chkStep.CheckedChanged += new System.EventHandler(this.chkStep_CheckedChanged);
            // 
            // btnMatch
            // 
            this.btnMatch.Location = new System.Drawing.Point(403, 388);
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Size = new System.Drawing.Size(136, 24);
            this.btnMatch.TabIndex = 24;
            this.btnMatch.Text = "Matc&h";
            this.btnMatch.Click += new System.EventHandler(this.btnMatch_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(403, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 24);
            this.button1.TabIndex = 25;
            this.button1.Text = "Load Matches";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // evaluateToolStripMenuItem
            // 
            this.evaluateToolStripMenuItem.Name = "evaluateToolStripMenuItem";
            this.evaluateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.evaluateToolStripMenuItem.Text = "E&valuate";
            this.evaluateToolStripMenuItem.Click += new System.EventHandler(this.evaluateToolStripMenuItem_Click);
            // 
            // killToolStripMenuItem
            // 
            this.killToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simpleToolStripMenuItem,
            this.nazgulToolStripMenuItem,
            this.ripperToolStripMenuItem,
            this.simple2ToolStripMenuItem,
            this.simple3ToolStripMenuItem,
            this.simpleXToolStripMenuItem,
            this.frontierToolStripMenuItem2});
            this.killToolStripMenuItem.Name = "killToolStripMenuItem";
            this.killToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.killToolStripMenuItem.Text = "Kill";
            // 
            // simpleToolStripMenuItem
            // 
            this.simpleToolStripMenuItem.Name = "simpleToolStripMenuItem";
            this.simpleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.simpleToolStripMenuItem.Text = "Simple";
            this.simpleToolStripMenuItem.Click += new System.EventHandler(this.simpleToolStripMenuItem_Click);
            // 
            // newPositionToolStripMenuItem
            // 
            this.newPositionToolStripMenuItem.Name = "newPositionToolStripMenuItem";
            this.newPositionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newPositionToolStripMenuItem.Text = "&New Position";
            this.newPositionToolStripMenuItem.Click += new System.EventHandler(this.newPositionToolStripMenuItem_Click);
            // 
            // nazgulToolStripMenuItem
            // 
            this.nazgulToolStripMenuItem.Name = "nazgulToolStripMenuItem";
            this.nazgulToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nazgulToolStripMenuItem.Text = "Nazgul";
            this.nazgulToolStripMenuItem.Click += new System.EventHandler(this.nazgulToolStripMenuItem_Click);
            // 
            // goalToolStripMenuItem
            // 
            this.goalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeToolStripMenuItem,
            this.connectToolStripMenuItem,
            this.eyesToolStripMenuItem1,
            this.escapeToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.surrenderToolStripMenuItem});
            this.goalToolStripMenuItem.Name = "goalToolStripMenuItem";
            this.goalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.goalToolStripMenuItem.Text = "Goal";
            // 
            // sizeToolStripMenuItem
            // 
            this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            this.sizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sizeToolStripMenuItem.Text = "Size";
            this.sizeToolStripMenuItem.Click += new System.EventHandler(this.sizeToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // eyesToolStripMenuItem1
            // 
            this.eyesToolStripMenuItem1.Name = "eyesToolStripMenuItem1";
            this.eyesToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.eyesToolStripMenuItem1.Text = "Eyes";
            this.eyesToolStripMenuItem1.Click += new System.EventHandler(this.eyesToolStripMenuItem1_Click);
            // 
            // escapeToolStripMenuItem
            // 
            this.escapeToolStripMenuItem.Name = "escapeToolStripMenuItem";
            this.escapeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.escapeToolStripMenuItem.Text = "Escape";
            this.escapeToolStripMenuItem.Click += new System.EventHandler(this.escapeToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // surrenderToolStripMenuItem
            // 
            this.surrenderToolStripMenuItem.Name = "surrenderToolStripMenuItem";
            this.surrenderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.surrenderToolStripMenuItem.Text = "Surrender";
            this.surrenderToolStripMenuItem.Click += new System.EventHandler(this.surrenderToolStripMenuItem_Click);
            // 
            // ripperToolStripMenuItem
            // 
            this.ripperToolStripMenuItem.Name = "ripperToolStripMenuItem";
            this.ripperToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ripperToolStripMenuItem.Text = "Ripper";
            this.ripperToolStripMenuItem.Click += new System.EventHandler(this.ripperToolStripMenuItem_Click);
            // 
            // simple2ToolStripMenuItem
            // 
            this.simple2ToolStripMenuItem.Name = "simple2ToolStripMenuItem";
            this.simple2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.simple2ToolStripMenuItem.Text = "Simple 2";
            this.simple2ToolStripMenuItem.Click += new System.EventHandler(this.simple2ToolStripMenuItem_Click);
            // 
            // simple3ToolStripMenuItem
            // 
            this.simple3ToolStripMenuItem.Name = "simple3ToolStripMenuItem";
            this.simple3ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.simple3ToolStripMenuItem.Text = "Simple 3";
            this.simple3ToolStripMenuItem.Click += new System.EventHandler(this.simple3ToolStripMenuItem_Click);
            // 
            // matchToolStripMenuItem
            // 
            this.matchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.lastMoveToolStripMenuItem,
            this.connectToolStripMenuItem1,
            this.extendToolStripMenuItem,
            this.cutToolStripMenuItem1,
            this.cornerToolStripMenuItem});
            this.matchToolStripMenuItem.Name = "matchToolStripMenuItem";
            this.matchToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.matchToolStripMenuItem.Text = "&Match";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.allToolStripMenuItem.Text = "&All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // lastMoveToolStripMenuItem
            // 
            this.lastMoveToolStripMenuItem.Name = "lastMoveToolStripMenuItem";
            this.lastMoveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lastMoveToolStripMenuItem.Text = "&Last Move";
            this.lastMoveToolStripMenuItem.Click += new System.EventHandler(this.lastMoveToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem1
            // 
            this.connectToolStripMenuItem1.Name = "connectToolStripMenuItem1";
            this.connectToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.connectToolStripMenuItem1.Text = "Connect";
            this.connectToolStripMenuItem1.Click += new System.EventHandler(this.connectToolStripMenuItem1_Click);
            // 
            // extendToolStripMenuItem
            // 
            this.extendToolStripMenuItem.Name = "extendToolStripMenuItem";
            this.extendToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.extendToolStripMenuItem.Text = "Extend";
            this.extendToolStripMenuItem.Click += new System.EventHandler(this.extendToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem1
            // 
            this.cutToolStripMenuItem1.Name = "cutToolStripMenuItem1";
            this.cutToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.cutToolStripMenuItem1.Text = "Cut";
            this.cutToolStripMenuItem1.Click += new System.EventHandler(this.cutToolStripMenuItem1_Click);
            // 
            // cornerToolStripMenuItem
            // 
            this.cornerToolStripMenuItem.Name = "cornerToolStripMenuItem";
            this.cornerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cornerToolStripMenuItem.Text = "Border";
            this.cornerToolStripMenuItem.Click += new System.EventHandler(this.cornerToolStripMenuItem_Click);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stonesToolStripMenuItem,
            this.byZonesToolStripMenuItem,
            this.byColorsToolStripMenuItem,
            this.byZonesWithMatchesToolStripMenuItem,
            this.byColorsWithMatchesToolStripMenuItem});
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.playToolStripMenuItem.Text = "&Play";
            // 
            // stonesToolStripMenuItem
            // 
            this.stonesToolStripMenuItem.Name = "stonesToolStripMenuItem";
            this.stonesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stonesToolStripMenuItem.Text = "By &Stones";
            this.stonesToolStripMenuItem.Click += new System.EventHandler(this.stonesToolStripMenuItem_Click);
            // 
            // byZonesToolStripMenuItem
            // 
            this.byZonesToolStripMenuItem.Name = "byZonesToolStripMenuItem";
            this.byZonesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.byZonesToolStripMenuItem.Text = "By &Zones";
            this.byZonesToolStripMenuItem.Click += new System.EventHandler(this.byZonesToolStripMenuItem_Click);
            // 
            // byColorsToolStripMenuItem
            // 
            this.byColorsToolStripMenuItem.Name = "byColorsToolStripMenuItem";
            this.byColorsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.byColorsToolStripMenuItem.Text = "By &Colors";
            this.byColorsToolStripMenuItem.Click += new System.EventHandler(this.byColorsToolStripMenuItem_Click);
            // 
            // simpleXToolStripMenuItem
            // 
            this.simpleXToolStripMenuItem.Name = "simpleXToolStripMenuItem";
            this.simpleXToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.simpleXToolStripMenuItem.Text = "SimpleX";
            this.simpleXToolStripMenuItem.Click += new System.EventHandler(this.simpleXToolStripMenuItem_Click);
            // 
            // txtZoneValue
            // 
            this.txtZoneValue.Location = new System.Drawing.Point(499, 199);
            this.txtZoneValue.Name = "txtZoneValue";
            this.txtZoneValue.ReadOnly = true;
            this.txtZoneValue.Size = new System.Drawing.Size(38, 20);
            this.txtZoneValue.TabIndex = 26;
            // 
            // txtColorValue
            // 
            this.txtColorValue.Location = new System.Drawing.Point(451, 199);
            this.txtColorValue.Name = "txtColorValue";
            this.txtColorValue.ReadOnly = true;
            this.txtColorValue.Size = new System.Drawing.Size(38, 20);
            this.txtColorValue.TabIndex = 27;
            // 
            // txtStoneValue
            // 
            this.txtStoneValue.Location = new System.Drawing.Point(403, 199);
            this.txtStoneValue.Name = "txtStoneValue";
            this.txtStoneValue.ReadOnly = true;
            this.txtStoneValue.Size = new System.Drawing.Size(38, 20);
            this.txtStoneValue.TabIndex = 28;
            // 
            // byZonesWithMatchesToolStripMenuItem
            // 
            this.byZonesWithMatchesToolStripMenuItem.Name = "byZonesWithMatchesToolStripMenuItem";
            this.byZonesWithMatchesToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.byZonesWithMatchesToolStripMenuItem.Text = "By Zones with Matches";
            this.byZonesWithMatchesToolStripMenuItem.Click += new System.EventHandler(this.byZonesWithMatchesToolStripMenuItem_Click);
            // 
            // byColorsWithMatchesToolStripMenuItem
            // 
            this.byColorsWithMatchesToolStripMenuItem.Name = "byColorsWithMatchesToolStripMenuItem";
            this.byColorsWithMatchesToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.byColorsWithMatchesToolStripMenuItem.Text = "By Colors with Matches";
            this.byColorsWithMatchesToolStripMenuItem.Click += new System.EventHandler(this.byColorsWithMatchesToolStripMenuItem_Click);
            // 
            // frontierToolStripMenuItem2
            // 
            this.frontierToolStripMenuItem2.Name = "frontierToolStripMenuItem2";
            this.frontierToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.frontierToolStripMenuItem2.Text = "Frontier";
            this.frontierToolStripMenuItem2.Click += new System.EventHandler(this.frontierToolStripMenuItem2_Click);
            // 
            // AjGoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 464);
            this.Controls.Add(this.txtStoneValue);
            this.Controls.Add(this.txtColorValue);
            this.Controls.Add(this.txtZoneValue);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnMatch);
            this.Controls.Add(this.chkStep);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkShowWork);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.Button9);
            this.Controls.Add(this.btnColors);
            this.Controls.Add(this.btnEmptyStone);
            this.Controls.Add(this.Button6);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.btnWhiteStone);
            this.Controls.Add(this.btnBlackStone);
            this.Controls.Add(this.pbxBoard);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AjGoForm";
            this.Text = "AjGo";
            this.Load += new System.EventHandler(this.AjGoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxBoard)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox pbxBoard;
        internal System.Windows.Forms.Button Button5;
        internal System.Windows.Forms.Button Button9;
        internal System.Windows.Forms.Button btnColors;
        internal System.Windows.Forms.Button btnEmptyStone;
        internal System.Windows.Forms.Button Button6;
        internal System.Windows.Forms.Button Button4;
        internal System.Windows.Forms.Button Button3;
        internal System.Windows.Forms.Button btnWhiteStone;
        internal System.Windows.Forms.Button btnBlackStone;
        private System.Windows.Forms.CheckBox chkShowWork;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem libertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frontierToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog LoadPositionDialog;
        private System.Windows.Forms.ToolStripMenuItem zoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectZoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem libertiesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem frontierToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eyesToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLabel;
        private System.Windows.Forms.CheckBox chkStep;
        internal System.Windows.Forms.Button btnMatch;
        internal System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem evaluateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem killToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog SavePositionDialog;
        private System.Windows.Forms.ToolStripMenuItem newPositionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nazgulToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eyesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem escapeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem surrenderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ripperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simple2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simple3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastMoveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem extendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cornerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stonesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byZonesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byColorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleXToolStripMenuItem;
        private System.Windows.Forms.TextBox txtZoneValue;
        private System.Windows.Forms.TextBox txtColorValue;
        private System.Windows.Forms.TextBox txtStoneValue;
        private System.Windows.Forms.ToolStripMenuItem byZonesWithMatchesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byColorsWithMatchesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frontierToolStripMenuItem2;

    }
}

