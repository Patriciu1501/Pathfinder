using System;
using System.Drawing;
using System.Windows.Forms;


namespace Pathfinder {
    static class Menu {

        private static readonly int topBarHeight = Window.height / 36;

        private static readonly Font menuButtonFont = new Font("Nirmala UI", 11, FontStyle.Bold);
        private static readonly Size resetButtonSize = new Size(Window.width / 24, Window.height / 43);
        private static readonly Size exitButtonSize = new Size(Window.width / 24, Window.height / 43);
        private static readonly Size BFSButtonSize = new Size(Window.width / 24, Window.height / 43);
        private static readonly Size DFSButtonSize = new Size(Window.width / 24, Window.height / 43);
        private static readonly Size DijkstraButtonSize = new Size(Window.width / 16, Window.height / 43);
        private static readonly Size AStarButtonSize = new Size(Window.width / 24, Window.height / 43);
        private static readonly Size weightButtonSize = new Size(Window.width / 14, Window.height / 43);
        private static readonly Size MazeButtonSize = new Size(Window.width / 24, Window.height / 43);
        private static readonly Size speedLabelSize = new Size(Window.width / 28, Window.height / 43);
        private static readonly Size speedUpButtonSize = new Size(Window.width / 64, Window.height / 43);
        private static readonly Size speedDownButtonSize = new Size(Window.width / 64, Window.height / 43);
        private static readonly Size topBarSize = new Size(Window.width, topBarHeight);

        public static Button resetButton;
        public static Button exitButton;
        public static Button BFSButton;
        public static Button DFSButton;
        public static Button DijkstraButton;
        public static Button AStarButton;
        public static Button weightButton;
        public static Button MazeButton;
        public static Label speedLabel;
        public static Button speedUpButton;
        public static Button speedDownButton;
        public static Tutorial tutorial;

        public static Label topBar;
        public static Label countObstaclesLabel;
        public static Label exploredNodesLabel;

        static Menu() {

            topBar = new Label() { Visible = true, BackColor = Color.Black, Size = topBarSize};
            resetButton = new Button() { Visible = true };
            exitButton = new Button() { Visible = true };
            BFSButton = new Button() { Visible = true };
            DFSButton = new Button() { Visible = true };
            DijkstraButton = new Button() { Visible = true };
            AStarButton = new Button() { Visible = true };
            MazeButton = new Button() { Visible = true };
            speedLabel = new Label() { Visible = true };
            speedUpButton = new Button() { Visible = true };
            speedDownButton = new Button() { Visible = true };
            countObstaclesLabel = new Label() { Visible = true };
            exploredNodesLabel = new Label() { Visible = true };
            weightButton = new Button() { Visible = true };
            tutorial = new Tutorial();

            resetButton.FlatStyle = FlatStyle.Flat;
            resetButton.FlatAppearance.BorderSize = 0;
            resetButton.Click += Events.resetClick;
            resetButton.Text = "&Reset";
            resetButton.ForeColor = Color.FromArgb(0, 207, 255);
            resetButton.BackColor = Color.Black;
            resetButton.Font = menuButtonFont;
            resetButton.Size = resetButtonSize;
            resetButton.MouseEnter += Events.mouseEnterMenuButton;
            resetButton.MouseLeave += Events.mouseLeaveMenuButton;

            exitButton.FlatStyle = FlatStyle.Flat;
            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.Click += Events.exitClick;
            exitButton.Text = "&Exit";
            exitButton.Size = exitButtonSize;
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
            BFSButton.Size = BFSButtonSize;
            BFSButton.MouseEnter += Events.mouseEnterMenuButton;
            BFSButton.MouseLeave += Events.mouseLeaveMenuButton;

            DFSButton.FlatStyle = FlatStyle.Flat;
            DFSButton.FlatAppearance.BorderSize = 0;
            DFSButton.Click += Events.DFSClick;
            DFSButton.Text = "&DFS";
            DFSButton.Location = new Point(BFSButton.Location.X + BFSButton.Width, 0);
            DFSButton.ForeColor = Color.FromArgb(0, 207, 255);
            DFSButton.BackColor = Color.Black;
            DFSButton.Font = menuButtonFont;
            DFSButton.Size = DFSButtonSize;
            DFSButton.MouseEnter += Events.mouseEnterMenuButton;
            DFSButton.MouseLeave += Events.mouseLeaveMenuButton;


            DijkstraButton.FlatStyle = FlatStyle.Flat;
            DijkstraButton.FlatAppearance.BorderSize = 0;
            //DijkstraButton.Click += Events.DFSClick;
            DijkstraButton.Text = "&Dijkstra";
            DijkstraButton.Location = new Point(DFSButton.Location.X + DFSButton.Width, 0);
            DijkstraButton.ForeColor = Color.FromArgb(0, 207, 255);
            DijkstraButton.BackColor = Color.Black;
            DijkstraButton.Font = menuButtonFont;
            DijkstraButton.Size = DijkstraButtonSize;
            DijkstraButton.MouseEnter += Events.mouseEnterMenuButton;
            DijkstraButton.MouseLeave += Events.mouseLeaveMenuButton;


            AStarButton.FlatStyle = FlatStyle.Flat;
            AStarButton.FlatAppearance.BorderSize = 0;
            //DijkstraButton.Click += Events.DFSClick;
            AStarButton.Text = "&A*";
            AStarButton.Location = new Point(DijkstraButton.Location.X + DijkstraButton.Width, 0);
            AStarButton.ForeColor = Color.FromArgb(0, 207, 255);
            AStarButton.BackColor = Color.Black;
            AStarButton.Font = menuButtonFont;
            AStarButton.Size = AStarButtonSize;
            AStarButton.MouseEnter += Events.mouseEnterMenuButton;
            AStarButton.MouseLeave += Events.mouseLeaveMenuButton;


            MazeButton.FlatStyle = FlatStyle.Flat;
            MazeButton.FlatAppearance.BorderSize = 0;
            MazeButton.Click += Events.MazeClick;
            MazeButton.Text = "&Maze";
            MazeButton.Location = new Point(AStarButton.Location.X + AStarButton.Width, 0);
            MazeButton.ForeColor = Color.FromArgb(0, 207, 255);
            MazeButton.BackColor = Color.Black;
            MazeButton.Font = menuButtonFont;
            MazeButton.Size = MazeButtonSize;
            MazeButton.MouseEnter += Events.mouseEnterMenuButton;
            MazeButton.MouseLeave += Events.mouseLeaveMenuButton;


            weightButton.FlatStyle = FlatStyle.Flat;
            weightButton.FlatAppearance.BorderSize = 0;
            weightButton.Text = "&Add weight";
            weightButton.Location = new Point(MazeButton.Location.X + MazeButton.Width, 0);
            weightButton.ForeColor = Color.FromArgb(0, 207, 255);
            weightButton.BackColor = Color.Black;
            weightButton.Font = menuButtonFont;
            weightButton.Size = weightButtonSize;
            weightButton.MouseEnter += Events.mouseEnterMenuButton;
            weightButton.MouseLeave += Events.mouseLeaveMenuButton;
            weightButton.Click += Events.weightButtonClick;


            countObstaclesLabel.FlatStyle = FlatStyle.Flat;
            countObstaclesLabel.Text = "Obstacles: 0";
            countObstaclesLabel.Location = new Point(exitButton.Location.X - exitButton.Width - 70, 0);
            countObstaclesLabel.ForeColor = Color.FromArgb(0, 207, 255);
            countObstaclesLabel.BackColor = Color.Black;
            countObstaclesLabel.Font = menuButtonFont;
            countObstaclesLabel.Size = new Size(Window.width / 10, Window.height / 43);


         
            exploredNodesLabel.FlatStyle = FlatStyle.Flat;
            exploredNodesLabel.Text = "Explored: 0";
            exploredNodesLabel.Location = new Point(countObstaclesLabel.Location.X - countObstaclesLabel.Width + 20, 0);
            exploredNodesLabel.ForeColor = Color.FromArgb(0, 207, 255);
            exploredNodesLabel.BackColor = Color.Black;
            exploredNodesLabel.Font = menuButtonFont;
            exploredNodesLabel.Size = new Size(Window.width / 16, Window.height / 43);


            speedLabel.FlatStyle = FlatStyle.Flat;
            speedLabel.Text = "&Speed";
            speedLabel.Location = new Point(exploredNodesLabel.Location.X - exploredNodesLabel.Width - 40, 0);
            speedLabel.ForeColor = Color.FromArgb(0, 207, 255);
            speedLabel.BackColor = Color.Black;
            speedLabel.Font = menuButtonFont;
            speedLabel.Size = speedLabelSize;

            speedUpButton.FlatStyle = FlatStyle.Flat;
            speedUpButton.FlatAppearance.BorderSize = 0;
            speedUpButton.Click += Events.speedUpClick;
            speedUpButton.Text = "&+";
            speedUpButton.Location = new Point(speedLabel.Location.X + speedLabel.Width - 2);
            speedUpButton.ForeColor = Color.FromArgb(0, 207, 255);
            speedUpButton.BackColor = Color.Black;
            speedUpButton.Font = menuButtonFont;
            speedUpButton.Size = speedUpButtonSize;
            speedUpButton.MouseEnter += Events.mouseEnterMenuButton;
            speedUpButton.MouseLeave += Events.mouseLeaveMenuButton;


            speedDownButton.FlatStyle = FlatStyle.Flat;
            speedDownButton.FlatAppearance.BorderSize = 0;
            speedDownButton.Click += Events.speedDownClick;
            speedDownButton.Text = "&-";
            speedDownButton.Size = speedDownButtonSize;
            speedDownButton.Location = new Point(speedLabel.Location.X - speedDownButton.Width - 8, 0);
            speedDownButton.ForeColor = Color.FromArgb(0, 207, 255);
            speedDownButton.BackColor = Color.Black;
            speedDownButton.Font = menuButtonFont;
            speedDownButton.MouseEnter += Events.mouseEnterMenuButton;
            speedDownButton.MouseLeave += Events.mouseLeaveMenuButton;

            Tutorial.tutorialEventNotifier.Location = new Point(speedDownButton.Location.X - (speedLabel.Width * 7), 0);


            topBar.Controls.Add(exitButton);
            topBar.Controls.Add(resetButton);
            topBar.Controls.Add(countObstaclesLabel);
            topBar.Controls.Add(exploredNodesLabel);
            topBar.Controls.Add(BFSButton);
            topBar.Controls.Add(DFSButton);
            topBar.Controls.Add(DijkstraButton);
            topBar.Controls.Add(AStarButton);
            topBar.Controls.Add(weightButton);
            topBar.Controls.Add(speedLabel);
            topBar.Controls.Add(MazeButton);
            topBar.Controls.Add(speedUpButton);
            topBar.Controls.Add(speedDownButton);
            topBar.Controls.Add(Tutorial.tutorialEventNotifier);


            Tutorial.runningNotifier = new System.Threading.Thread(tutorial.runNotifier);
        }


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
