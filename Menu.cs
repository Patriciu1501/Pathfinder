using System.Drawing;
using System.Windows.Forms;


namespace Pathfinder {
    class Menu {

        public static Label topBar;
        private Button buttonResetare;
        private Button buttonIesire;
        private Button BFS;
        private Button DFS;
        private Button Maze;
        private Label speed;
        private Button speedUp;
        private Button speedDown;

        public static Label countObstacles;
        public static Label exploredNodes;

        public Menu() {

            topBar = new Label() { Visible = true, BackColor = Color.Black, Dock = DockStyle.Top };
            buttonResetare = new Button() { Visible = true };
            buttonIesire = new Button() { Visible = true };
            BFS = new Button() { Visible = true };
            DFS = new Button() { Visible = true };
            Maze = new Button() { Visible = true };
            speed = new Label() { Visible = true };
            speedUp = new Button() { Visible = true };
            speedDown = new Button() { Visible = true };

            countObstacles = new Label() { Visible = true };
            exploredNodes = new Label() { Visible = true };

            buttonResetare.FlatStyle = FlatStyle.Flat;
            buttonResetare.FlatAppearance.BorderSize = 0;
            buttonResetare.Click += Events.clickResetare;
            buttonResetare.Text = "&Reset";
            buttonResetare.ForeColor = Color.FromArgb(0, 207, 255);
            buttonResetare.BackColor = Color.Black;
            buttonResetare.Font = new Font("Nirmala UI", 11, FontStyle.Bold);
            buttonResetare.Size = new Size(80, 20);
            buttonResetare.MouseEnter += Events.mouseEnterMenuButton;
            buttonResetare.MouseLeave += Events.mouseLeaveMenuButton;

            buttonIesire.FlatStyle = FlatStyle.Flat;
            buttonIesire.FlatAppearance.BorderSize = 0;
            buttonIesire.Click += Events.clickIesire;
            buttonIesire.Text = "&Exit";
            buttonIesire.Location = new Point(buttonResetare.Width + 10, 0);
            buttonIesire.ForeColor = Color.FromArgb(0, 207, 255);
            buttonIesire.BackColor = Color.Black;
            buttonIesire.Font = new Font("Nirmala UI", 11, FontStyle.Bold);
            buttonIesire.Size = new Size(80, 20);
            buttonIesire.MouseEnter += Events.mouseEnterMenuButton;
            buttonIesire.MouseLeave += Events.mouseLeaveMenuButton;

            BFS.FlatStyle = FlatStyle.Flat;
            BFS.FlatAppearance.BorderSize = 0;
            BFS.Click += Events.BFSClick;
            BFS.Text = "&BFS";
            BFS.Location = new Point(buttonResetare.Width + 150, 0);
            BFS.ForeColor = Color.FromArgb(0, 207, 255);
            BFS.BackColor = Color.Black;
            BFS.Font = new Font("Nirmala UI", 11, FontStyle.Bold);
            BFS.Size = new Size(80, 20);
            BFS.MouseEnter += Events.mouseEnterMenuButton;
            BFS.MouseLeave += Events.mouseLeaveMenuButton;

            DFS.FlatStyle = FlatStyle.Flat;
            DFS.FlatAppearance.BorderSize = 0;
            DFS.Click += Events.DFSClick;
            DFS.Text = "&DFS";
            DFS.Location = new Point(buttonResetare.Width + 250, 0);
            DFS.ForeColor = Color.FromArgb(0, 207, 255);
            DFS.BackColor = Color.Black;
            DFS.Font = new Font("Nirmala UI", 11, FontStyle.Bold);
            DFS.Size = new Size(80, 20);
            DFS.MouseEnter += Events.mouseEnterMenuButton;
            DFS.MouseLeave += Events.mouseLeaveMenuButton;


            Maze.FlatStyle = FlatStyle.Flat;
            Maze.FlatAppearance.BorderSize = 0;
            Maze.Click += Events.MazeClick;
            Maze.Text = "&Maze";
            Maze.Location = new Point(buttonResetare.Width + 350, 0);
            Maze.ForeColor = Color.FromArgb(0, 207, 255);
            Maze.BackColor = Color.Black;
            Maze.Font = new Font("Nirmala UI", 11, FontStyle.Bold);
            Maze.Size = new Size(80, 20);
            Maze.MouseEnter += Events.mouseEnterMenuButton;
            Maze.MouseLeave += Events.mouseLeaveMenuButton;


            countObstacles.FlatStyle = FlatStyle.Flat;
            countObstacles.Text = "Obstacles: 0";
            countObstacles.Location = new Point(buttonResetare.Width + 1300, 0);
            countObstacles.ForeColor = Color.FromArgb(0, 207, 255);
            countObstacles.BackColor = Color.Black;
            countObstacles.Font = new Font("Nirmala UI", 11, FontStyle.Bold);
            countObstacles.Size = new Size(120, 20);


            speed.FlatStyle = FlatStyle.Flat;
            speed.Text = "&Speed";
            speed.Location = new Point(buttonResetare.Width + 1000, 0);
            speed.ForeColor = Color.FromArgb(0, 207, 255);
            speed.BackColor = Color.Black;
            speed.Font = new Font("Nirmala UI", 11, FontStyle.Bold);
            speed.Size = new Size(50, 20);

            speedUp.FlatStyle = FlatStyle.Flat;
            speedUp.FlatAppearance.BorderSize = 0;
            speedUp.Click += Events.speedUpClick;
            speedUp.Text = "&+";
            speedUp.Location = new Point(speed.Location.X + speed.Width);
            speedUp.ForeColor = Color.FromArgb(0, 207, 255);
            speedUp.BackColor = Color.Black;
            speedUp.Font = new Font("Nirmala UI", 14, FontStyle.Bold);
            speedUp.Size = new Size(30, 20);
            speedUp.MouseEnter += Events.mouseEnterMenuButton;
            speedUp.MouseLeave += Events.mouseLeaveMenuButton;


            speedDown.FlatStyle = FlatStyle.Flat;
            speedDown.FlatAppearance.BorderSize = 0;
            speedDown.Click += Events.speedDownClick;
            speedDown.Text = "&-";
            speedDown.Location = new Point(speed.Location.X - speed.Width);
            speedDown.ForeColor = Color.FromArgb(0, 207, 255);
            speedDown.BackColor = Color.Black;
            speedDown.Font = new Font("Nirmala UI", 14, FontStyle.Bold);
            speedDown.Size = new Size(30, 20);
            speedDown.MouseEnter += Events.mouseEnterMenuButton;
            speedDown.MouseLeave += Events.mouseLeaveMenuButton;


            exploredNodes.FlatStyle = FlatStyle.Flat;
            exploredNodes.Text = "Explored: 0";
            exploredNodes.Location = new Point(buttonResetare.Width + 1150, 0);
            exploredNodes.ForeColor = Color.FromArgb(0, 207, 255);
            exploredNodes.BackColor = Color.Black;
            exploredNodes.Font = new Font("Nirmala UI", 11, FontStyle.Bold);
            exploredNodes.Size = new Size(120, 20);



            topBar.Controls.Add(buttonIesire);
            topBar.Controls.Add(buttonResetare);
            topBar.Controls.Add(countObstacles);
            topBar.Controls.Add(exploredNodes);
            topBar.Controls.Add(BFS);
            topBar.Controls.Add(DFS);
            topBar.Controls.Add(speed);
            topBar.Controls.Add(Maze);
            topBar.Controls.Add(speedUp);
            topBar.Controls.Add(speedDown);
        }
    }
}
