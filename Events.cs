using System;
using System.Windows.Forms;
using System.Drawing;
using Pathfinder.Algorithms;
using System.Threading;

namespace Pathfinder {
    public static class Events {


        private static bool destinationDragging = false;

        static public void labelMouseMove(object sender, MouseEventArgs e) {

            OOPLabel curr = sender as OOPLabel;

            if (curr == Map.destination && e.Button == MouseButtons.Left && Algorithm.algorithmState == Algorithm.AlgorithmState.Finished) destinationDragging = true;

            else if (destinationDragging && curr != Map.destination && curr.BackColor != Map.obstacleColor && e.Button == MouseButtons.Left) {

                Map.destination = curr;
                Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Instant;

                if (Algorithm.algorithmName == Algorithm.AlgorithmName.BFS) BFSClick(sender, e);
                else if (Algorithm.algorithmName == Algorithm.AlgorithmName.DFS) DFSClick(sender, e);

            }

            else if (destinationDragging && curr != Map.destination && e.Button == MouseButtons.None) destinationDragging = false;

            else if (e.Button == MouseButtons.None && curr.Image == null && curr.BackColor == Map.initialLabelColor) curr.BackColor = Color.Aqua;

            else if (!Map.sourceFlagAdded || !Map.destinationFlagAdded) return;

            else if (e.Button == MouseButtons.Left && curr.Image == null && curr.BackColor != Map.obstacleColor && Algorithm.algorithmState != Algorithm.AlgorithmState.Running) {

                Menu.ModifyCountObstacles(increment: true);
                curr.BackColor = Map.obstacleColor;
            }

            else if (e.Button == MouseButtons.Right && curr.BackColor == Map.obstacleColor && Algorithm.algorithmState != Algorithm.AlgorithmState.Running) {

                Menu.ModifyCountObstacles(increment: false);
                curr.BackColor = Map.initialLabelColor;
            }

            curr.Capture = false;
        }


        static public void labelMouseLeave(object sender, EventArgs e) {

            OOPLabel curr = sender as OOPLabel;

            if (curr.BackColor == Color.Aqua) curr.BackColor = Map.initialLabelColor;
        }


        static public void labelMouseDown(object sender, EventArgs e) {

            MouseEventArgs pressedButton = e as MouseEventArgs;
            OOPLabel curr = sender as OOPLabel;

            if (Map.sourceFlagAdded == false && curr.BackColor != Map.obstacleColor) {

                curr.BackColor = Map.initialLabelColor;
                curr.Image = Map.sourceImage;
                Map.source = curr;
                Map.sourceFlagAdded = true;
            }

            else if (Map.destinationFlagAdded == false && curr.Image == null && curr.BackColor != Map.obstacleColor) {

                curr.BackColor = Map.initialLabelColor;
                curr.Image = Map.destinationImage;
                Map.destination = curr;
                Map.destinationFlagAdded = true;
            }

            else if (MouseButtons.Left == pressedButton.Button && curr.Image == null && curr.BackColor != Map.obstacleColor && Algorithm.algorithmState != Algorithm.AlgorithmState.Running) {

                Menu.ModifyCountObstacles(increment: true);
                curr.BackColor = Map.obstacleColor;
            }

            else if (MouseButtons.Right == pressedButton.Button && curr.BackColor == Map.obstacleColor && Algorithm.algorithmState != Algorithm.AlgorithmState.Running) {

                Menu.ModifyCountObstacles(increment: false);
                curr.BackColor = Map.initialLabelColor;
            }
        }


        static public void resetClick(object sender, EventArgs e) {

            if (Algorithm.algorithmState == Algorithm.AlgorithmState.Running) {

                
                if (Algorithm.algorithmSpeed == Algorithm.AlgorithmSpeed.Paused) {

                    Algorithm.runningAlgorithm[Algorithm.runningAlgorithm.Count - 1].Resume();
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Standard;
                }

                for (int i = 0; i < Algorithm.runningAlgorithm.Count; i++) Algorithm.runningAlgorithm[i].Abort();             
            }

            for (int i = 0; i < Map.labels.GetLength(0); i++)
                for (int j = 0; j < Map.labels.GetLength(1); j++) {

                    Map.labels[i, j].BackColor = Map.initialLabelColor;
                    Map.labels[i, j].Image = null;
                }


            Map.source = null;
            Map.destination = null;
            Map.sourceFlagAdded = false;
            Map.destinationFlagAdded = false;
            Menu.ResetCountObstacles();
            Menu.ResetExploredNodes();
            Algorithm.algorithmState = Algorithm.AlgorithmState.NeverFinished;
        }



        static public void exitClick(object sender, EventArgs e) {

            DialogResult answ = MessageBox.Show("Are you sure?", "Exit program", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answ == DialogResult.Yes) {

                if(Algorithm.runningAlgorithm.Count != 0) Algorithm.runningAlgorithm[0].Abort();
                Tutorial.runningNotifier.Abort();
                Application.ExitThread();            
            }
        }

        static public void formClose(object sender, FormClosingEventArgs e) => exitClick(sender, e); 

        static public void mouseEnterMenuButton(object sender, EventArgs e) {

            if ((sender as Button).Text.Length <= 6) {

                (sender as Button).Cursor = Cursors.Hand;
                (sender as Button).BackColor = Color.DarkGray;
            }
        }


        static public void mouseLeaveMenuButton(object sender, EventArgs e) {

            (sender as Button).BackColor = Color.Black;
        }

        static public void BFSClick(object sender, EventArgs e) {

            Algorithm startBFS = new BFS();

            startBFS.RunningAlgorithm.Add(new Thread(startBFS.StartAlgorithm));
            startBFS.RunningAlgorithm[startBFS.RunningAlgorithm.Count - 1].Start();

        }


        static public void DFSClick(object sender, EventArgs e) {

            Algorithm startDFS = new DFS();

            startDFS.RunningAlgorithm.Add(new Thread(startDFS.StartAlgorithm));
            startDFS.RunningAlgorithm[startDFS.RunningAlgorithm.Count - 1].Start();

        }

        static public void MazeClick(object sender, EventArgs e) {

            Algorithm startMazeBacktracker = new MazeBacktracker();

            startMazeBacktracker.RunningAlgorithm.Add(new Thread(startMazeBacktracker.StartAlgorithm));
            startMazeBacktracker.RunningAlgorithm[startMazeBacktracker.RunningAlgorithm.Count - 1].Start();

        }


        static public void speedUpClick(object sender, EventArgs e) {

            if (Algorithm.algorithmSpeed == Algorithm.AlgorithmSpeed.Paused) {
                
                Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.VerySlow;
                Algorithm.runningAlgorithm[Algorithm.runningAlgorithm.Count - 1].Resume();
            }

            else if (Algorithm.algorithmSpeed > Algorithm.AlgorithmSpeed.Instant) Algorithm.algorithmSpeed -= 20;
            Tutorial.speedModified = true;

        }


        static public void speedDownClick(object sender, EventArgs e) {

            if (Algorithm.algorithmSpeed == Algorithm.AlgorithmSpeed.VerySlow && Algorithm.runningAlgorithm.Count > 0) {

                Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Paused;
                Algorithm.runningAlgorithm[Algorithm.runningAlgorithm.Count - 1].Suspend();
            }

            else if (Algorithm.algorithmSpeed <= Algorithm.AlgorithmSpeed.Paused) Algorithm.algorithmSpeed += 20;
            Tutorial.speedModified = true;
        }


        static public void PaintGrid(object sender, PaintEventArgs e) {

            ControlPaint.DrawBorder(e.Graphics, (sender as OOPLabel).DisplayRectangle, Color.Black, ButtonBorderStyle.Outset);
        }
        

        static public void DeleteGrid(object sender, PaintEventArgs e) {

            ControlPaint.DrawBorder(e.Graphics, (sender as OOPLabel).DisplayRectangle, Color.DarkGray, ButtonBorderStyle.None);
        }

    }
}