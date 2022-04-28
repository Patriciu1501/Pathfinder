using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Specialized;
using Pathfinder.Algorithms;
using System.Threading;

namespace Pathfinder {
    public static class Events {

        static public void labelMouseMove(object sender, MouseEventArgs e) {

            Label curr = sender as Label;

            if (e.Button == MouseButtons.None && curr.Image == null && curr.BackColor == Map.initialLabelColor) curr.BackColor = Color.Aqua;

            else if (e.Button == MouseButtons.Left && curr.Image == null && curr.BackColor != Map.obstacleColor && Algorithm.algorithmState != Algorithm.AlgorithmState.Running) {

                string[] splits = Menu.countObstacles.Text.Split(' ');
                int nr = Convert.ToInt32(splits[1]);
                nr++;
                Menu.countObstacles.Text = splits[0] + " " + nr;
                curr.BackColor = Map.obstacleColor;           
            }

            else if (e.Button == MouseButtons.Right && curr.BackColor == Map.obstacleColor && Algorithm.algorithmState != Algorithm.AlgorithmState.Running) {

                string[] splits = Menu.countObstacles.Text.Split(' ');
                int nr = Convert.ToInt32(splits[1]);
                nr--;
                Menu.countObstacles.Text = splits[0] + " " + nr;
                curr.BackColor = Map.initialLabelColor;   
            }

            curr.Capture = false;
        }


        static public void labelMouseLeave(object sender, EventArgs e) {

            Label curr = sender as Label;

            if (curr.BackColor == Color.Aqua) curr.BackColor = Map.initialLabelColor;
        }


        static public void labelMouseDown(object sender, EventArgs e) {

            MouseEventArgs pressedButton = e as MouseEventArgs;
            Label curr = sender as Label;

            if (Map.startFlagAdded == false) {

                curr.BackColor = Map.initialLabelColor;
                curr.Image = Image.FromFile("start.png");
                Map.source = curr;
                Map.startFlagAdded = true;
            }

            else if (Map.destinationFlagAdded == false) {

                curr.BackColor = Map.initialLabelColor;
                curr.Image = Image.FromFile("destination.png");
                Map.destination = curr;
                Map.destinationFlagAdded = true;
            }

            else if (MouseButtons.Left == pressedButton.Button && curr.Image == null && curr.BackColor != Map.obstacleColor && Algorithm.algorithmState != Algorithm.AlgorithmState.Running) {
           
                string[] splits = Menu.countObstacles.Text.Split(' ');
                int nr = Convert.ToInt32(splits[1]);
                nr++;
                Menu.countObstacles.Text = splits[0] + " " + nr;
                curr.BackColor = Map.obstacleColor;               
            }

            else if (MouseButtons.Right == pressedButton.Button && curr.BackColor == Map.obstacleColor && Algorithm.algorithmState != Algorithm.AlgorithmState.Running) {
                
                string[] splits = Menu.countObstacles.Text.Split(' ');
                int nr = Convert.ToInt32(splits[1]);
                nr--;
                Menu.countObstacles.Text = splits[0] + " " + nr;
                curr.BackColor = Map.initialLabelColor;                         
            }
        }


        static public void clickResetare(object sender, EventArgs e) {

            for (int i = 0; i < Map.labeluri.GetLength(0); i++) 
                for (int j = 0; j < Map.labeluri.GetLength(1); j++) Map.labeluri[i, j].BackColor = Map.initialLabelColor;


            if(Map.source != null && Map.destination != null) {   // need this because when you try to reset an empty map you access a null object

                Map.source.Image = null;
                Map.destination.Image = null;
                Map.source = null;
                Map.destination = null;
            }
            
            Map.startFlagAdded = false;
            Map.destinationFlagAdded = false;
            Menu.countObstacles.Text = "Obstacles: 0";
            Algorithm.adjancecyList = new OrderedDictionary();
            
            if (Algorithm.algorithmState == Algorithm.AlgorithmState.Running) {

                Algorithm.runningAlgorithm[Algorithm.runningAlgorithm.Count - 1].Abort();
                Algorithm.algorithmState = Algorithm.AlgorithmState.NeverFinished;
            }
        }



        static public void clickIesire(object sender, EventArgs e) {

            DialogResult answ = MessageBox.Show("Are you sure?", "Exit program", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answ == DialogResult.Yes) {

                foreach (var i in Algorithm.runningAlgorithm) i.Abort();
                Application.ExitThread();
            }
        }

        static public void formClose(object sender, FormClosingEventArgs e) => clickIesire(sender, e); // for ALT+F4

        static public void mouseEnterMenuButton(object sender, EventArgs e) => (sender as Button).Cursor = Cursors.Hand;


        static public void BFSClick(object sender, EventArgs e) {

            Algorithm startBFS = new BFS();

            startBFS.RunningAlgorithm.Add(new Thread(startBFS.StartAlgorithm));
            startBFS.RunningAlgorithm[startBFS.RunningAlgorithm.Count - 1].Start();
                      
        }

    }
}
