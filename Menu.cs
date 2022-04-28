using System.Drawing;
using System.Windows.Forms;


namespace Pathfinder {
    class Menu {

        public Label topBar;
        Button buttonResetare;
        Button buttonIesire;
        Button BFS;

        public static Button countObstacles;

        public Menu() {

            topBar = new Label() { Visible = true, BackColor = Color.Black, Dock = DockStyle.Top };
            buttonResetare = new Button() { Visible = true };
            buttonIesire = new Button() { Visible = true };
            BFS = new Button() { Visible = true };
            countObstacles = new Button() { Visible = true };

            buttonResetare.FlatStyle = FlatStyle.Flat;
            buttonResetare.FlatAppearance.BorderSize = 0;
            buttonResetare.Click += Events.clickResetare;
            buttonResetare.Text = "&Reset";
            buttonResetare.ForeColor = Color.FromArgb(0, 207, 255);
            buttonResetare.BackColor = Color.Black;
            buttonResetare.Font = new Font("Nirmala UI", 11, FontStyle.Bold);
            buttonResetare.Size = new Size(80, 20);
            buttonResetare.MouseEnter += Events.mouseEnterMenuButton;

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


            countObstacles.FlatStyle = FlatStyle.Flat;
            countObstacles.FlatAppearance.BorderSize = 0;
            countObstacles.Text = "Obstacles: 0";
            countObstacles.Location = new Point(buttonResetare.Width + 1300, 0);
            countObstacles.ForeColor = Color.FromArgb(0, 207, 255);
            countObstacles.BackColor = Color.Black;
            countObstacles.Font = new Font("Nirmala UI", 11, FontStyle.Bold);
            countObstacles.Size = new Size(120, 20);
            countObstacles.MouseEnter += Events.mouseEnterMenuButton;


            topBar.Controls.Add(buttonIesire);
            topBar.Controls.Add(buttonResetare);
            topBar.Controls.Add(countObstacles);
            topBar.Controls.Add(BFS);
        }
    }
}
