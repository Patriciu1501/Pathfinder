using System;
using System.Drawing;
using System.Windows.Forms;


namespace Pathfinder {
    class Menu {

        private readonly Font menuButtonFont = new Font("Nirmala UI", 11, FontStyle.Bold);
        private static readonly int topBarHeight = Window.height / 36;

        private Button resetButton;
        private Button exitButton;
        private Button BFSButton;
        private Button DFSButton;
        private Button MazeButton;
        private Label speedLabel;
        private Button speedUpButton;
        private Button speedDownButton;
        private Tutorial tutorial;

        public Label topBar;
        public static Label countObstaclesLabel;
        public static Label exploredNodesLabel;

        public Menu() {

            topBar = new Label() { Visible = true, BackColor = Color.Black, Size = new Size(Window.width, topBarHeight)};
            resetButton = new Button() { Visible = true };
            exitButton = new Button() { Visible = true };
            BFSButton = new Button() { Visible = true };
            DFSButton = new Button() { Visible = true };
            MazeButton = new Button() { Visible = true };
            speedLabel = new Label() { Visible = true };
            speedUpButton = new Button() { Visible = true };
            speedDownButton = new Button() { Visible = true };
            countObstaclesLabel = new Label() { Visible = true };
            exploredNodesLabel = new Label() { Visible = true };
            tutorial = new Tutorial();

            resetButton.FlatStyle = FlatStyle.Flat;
            resetButton.FlatAppearance.BorderSize = 0;
            resetButton.Click += Events.resetClick;
            resetButton.Text = "&Reset";
            resetButton.ForeColor = Color.FromArgb(0, 207, 255);
            resetButton.BackColor = Color.Black;
            resetButton.Font = menuButtonFont;
            resetButton.Size = new Size(Window.width / 24, Window.height / 43);
            resetButton.MouseEnter += Events.mouseEnterMenuButton;
            resetButton.MouseLeave += Events.mouseLeaveMenuButton;

            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.Click += Events.exitClick;
            exitButton.Text = "&Exit";
            exitButton.Size = new Size(Window.width / 24, Window.height / 43);
            exitButton.Location = new Point(topBar.Width - exitButton.Width);
            exitButton.ForeColor = Color.FromArgb(0, 207, 255);
            exitButton.BackColor = Color.Black;
            exitButton.Font = menuButtonFont;
            exitButton.MouseEnter += Events.mouseEnterMenuButton;
            exitButton.MouseLeave += Events.mouseLeaveMenuButton;

            BFSButton.FlatStyle = FlatStyle.Flat;
            BFSButton.FlatAppearance.BorderSize = 0;
            BFSButton.Click += Events.BFSClick;
            BFSButton.Text = "&BFS";
            BFSButton.Location = new Point(resetButton.Width + (Window.width / 147), 0);
            BFSButton.ForeColor = Color.FromArgb(0, 207, 255);
            BFSButton.BackColor = Color.Black;
            BFSButton.Font = menuButtonFont;
            BFSButton.Size = new Size(Window.width / 24, Window.height / 43);
            BFSButton.MouseEnter += Events.mouseEnterMenuButton;
            BFSButton.MouseLeave += Events.mouseLeaveMenuButton;

            DFSButton.FlatStyle = FlatStyle.Flat;
            DFSButton.FlatAppearance.BorderSize = 0;
            DFSButton.Click += Events.DFSClick;
            DFSButton.Text = "&DFS";
            DFSButton.Location = new Point(resetButton.Width + 250, 0);
            DFSButton.ForeColor = Color.FromArgb(0, 207, 255);
            DFSButton.BackColor = Color.Black;
            DFSButton.Font = menuButtonFont;
            DFSButton.Size = new Size(Window.width / 24, Window.height / 43);
            DFSButton.MouseEnter += Events.mouseEnterMenuButton;
            DFSButton.MouseLeave += Events.mouseLeaveMenuButton;


            MazeButton.FlatStyle = FlatStyle.Flat;
            MazeButton.FlatAppearance.BorderSize = 0;
            MazeButton.Click += Events.MazeClick;
            MazeButton.Text = "&Maze";
            MazeButton.Location = new Point(resetButton.Width + 350, 0);
            MazeButton.ForeColor = Color.FromArgb(0, 207, 255);
            MazeButton.BackColor = Color.Black;
            MazeButton.Font = menuButtonFont;
            MazeButton.Size = new Size(Window.width / 24, Window.height / 43);
            MazeButton.MouseEnter += Events.mouseEnterMenuButton;
            MazeButton.MouseLeave += Events.mouseLeaveMenuButton;


            countObstaclesLabel.FlatStyle = FlatStyle.Flat;
            countObstaclesLabel.Text = "Obstacles: 0";
            countObstaclesLabel.Location = new Point(resetButton.Width + 1300, 0);
            countObstaclesLabel.ForeColor = Color.FromArgb(0, 207, 255);
            countObstaclesLabel.BackColor = Color.Black;
            countObstaclesLabel.Font = menuButtonFont;
            countObstaclesLabel.Size = new Size(Window.width / 10, Window.height / 43);


            speedLabel.FlatStyle = FlatStyle.Flat;
            speedLabel.Text = "&Speed";
            speedLabel.Location = new Point(resetButton.Width + 1000, 0);
            speedLabel.ForeColor = Color.FromArgb(0, 207, 255);
            speedLabel.BackColor = Color.Black;
            speedLabel.Font = menuButtonFont;
            speedLabel.Size = new Size(Window.width / 31, Window.height / 43);

            speedUpButton.FlatStyle = FlatStyle.Flat;
            speedUpButton.FlatAppearance.BorderSize = 0;
            speedUpButton.Click += Events.speedUpClick;
            speedUpButton.Text = "&+";
            speedUpButton.Location = new Point(speedLabel.Location.X + speedLabel.Width);
            speedUpButton.ForeColor = Color.FromArgb(0, 207, 255);
            speedUpButton.BackColor = Color.Black;
            speedUpButton.Font = menuButtonFont;
            speedUpButton.Size = new Size(Window.width / 64, Window.height / 43);
            speedUpButton.MouseEnter += Events.mouseEnterMenuButton;
            speedUpButton.MouseLeave += Events.mouseLeaveMenuButton;


            speedDownButton.FlatStyle = FlatStyle.Flat;
            speedDownButton.FlatAppearance.BorderSize = 0;
            speedDownButton.Click += Events.speedDownClick;
            speedDownButton.Text = "&-";
            speedDownButton.Size = new Size(Window.width / 64, Window.height / 43);
            speedDownButton.Location = new Point(speedLabel.Location.X - speedDownButton.Width - 2);
            speedDownButton.ForeColor = Color.FromArgb(0, 207, 255);
            speedDownButton.BackColor = Color.Black;
            speedDownButton.Font = menuButtonFont;  
            speedDownButton.MouseEnter += Events.mouseEnterMenuButton;
            speedDownButton.MouseLeave += Events.mouseLeaveMenuButton;


            exploredNodesLabel.FlatStyle = FlatStyle.Flat;
            exploredNodesLabel.Text = "Explored: 0";
            exploredNodesLabel.Location = new Point(resetButton.Width + 1150, 0);
            exploredNodesLabel.ForeColor = Color.FromArgb(0, 207, 255);
            exploredNodesLabel.BackColor = Color.Black;
            exploredNodesLabel.Font = menuButtonFont;
            exploredNodesLabel.Size = new Size(Window.width / 16, Window.height / 43);

            



            topBar.Controls.Add(exitButton);
            topBar.Controls.Add(resetButton);
            topBar.Controls.Add(countObstaclesLabel);
            topBar.Controls.Add(exploredNodesLabel);
            topBar.Controls.Add(BFSButton);
            topBar.Controls.Add(DFSButton);
            topBar.Controls.Add(speedLabel);
            topBar.Controls.Add(MazeButton);
            topBar.Controls.Add(speedUpButton);
            topBar.Controls.Add(speedDownButton);


            tutorial.tutorialEventNotifier.Location = new Point(Window.width / 3, 0);
            topBar.Controls.Add(tutorial.tutorialEventNotifier);

            Tutorial.runningNotifier = new System.Threading.Thread(tutorial.runNotifier);
        }

        public static int GetTopBarHeight() => topBarHeight;

        public static void ModifyCountObstacles(bool increment) {

            string[] splits = countObstaclesLabel.Text.Split(' ');
            int nr = Convert.ToInt32(splits[1]);

            if (increment) {
      
                nr++;
                countObstaclesLabel.Text = splits[0] + " " + nr;
            }

            else if(!increment) {

                nr--;
                countObstaclesLabel.Text = splits[0] + " " + nr;
            }
        }


        public static void ResetCountObstacles() => countObstaclesLabel.Text = "Obstacles: 0";


        public static void ModifyExploredNodes() {

            string[] splits = exploredNodesLabel.Text.Split(' ');
            int nr = Convert.ToInt32(splits[1]);
            nr++;
            exploredNodesLabel.Text = splits[0] + " " + nr;
        }


        public static void ResetExploredNodes() => exploredNodesLabel.Text = "Explored: 0";
    }
}
