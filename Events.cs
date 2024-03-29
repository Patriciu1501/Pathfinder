﻿using System;
using System.Windows.Forms;
using System.Drawing;
using Pathfinder.Algorithms;
using System.Threading;


namespace Pathfinder {
    public static class Events {

        public static bool destinationDragging = false;

        public static void labelMouseMove(object sender, MouseEventArgs e) {

            OOPLabel curr = sender as OOPLabel;

            if (curr == Map.destination && e.Button == MouseButtons.Left && Algorithm.algorithmState == Algorithm.AlgorithmState.Finished) destinationDragging = true;

            else if (destinationDragging && curr != Map.destination && curr.BackColor != Map.obstacleColor && e.Button == MouseButtons.Left) {

                Map.destination.Image = null;
                Map.destination = curr;
                Map.destination.Image = Map.destinationImage;
                Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Instant;

                if (Algorithm.algorithmName == Algorithm.AlgorithmName.BFS) BFSClick(sender, e);
                else if (Algorithm.algorithmName == Algorithm.AlgorithmName.DFS) DFSClick(sender, e);
                else if (Algorithm.algorithmName == Algorithm.AlgorithmName.Dijkstra) DijkstraClick(sender, e);
                else if (Algorithm.algorithmName == Algorithm.AlgorithmName.aStar) AStarClick(sender, e);

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


        public static void labelMouseLeave(object sender, EventArgs e) {

            OOPLabel curr = sender as OOPLabel;

            if (curr.BackColor == Color.Aqua) curr.BackColor = Map.initialLabelColor;
        }


        public static void labelMouseDown(object sender, EventArgs e) {

            MouseEventArgs pressedButton = e as MouseEventArgs;
            OOPLabel curr = sender as OOPLabel;

            if(curr.Cursor != Cursors.Default && curr != Map.source && curr != Map.destination && curr.BackColor != Map.obstacleColor) {

                curr.weight = OOPLabel.WeightValue;

                if (curr.BackColor == Color.Aqua) {

                    curr.BackColor = Map.initialLabelColor;
                    curr.Image = Map.weightInitialImage;
                }

                else if (curr.BackColor == Map.searchColor) curr.Image = Map.weightSearchedImage;
                else if (curr.BackColor == Map.searchColorBorder) curr.Image = Map.weightSearchedBorderImage;
                else if(curr.BackColor == Map.pathColor) curr.Image = Map.weightPathImage;
                

                if (!Map.weightedGraph) {

                    Map.weightedGraph = true;
                    Menu.BFSButton.ForeColor = Color.Red;
                    Menu.DFSButton.ForeColor = Color.Red;
                }

                foreach (var i in Map.labels) i.Cursor = Cursors.Default;
            }

            else if (Map.sourceFlagAdded == false && curr.BackColor != Map.obstacleColor) {

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


        public static void resetClick(object sender, EventArgs e) {

            
            if(Algorithm.runningAlgorithm.Count > 0)
                foreach(var thread in Algorithm.runningAlgorithm) {

                    if (thread.ThreadState == ThreadState.Suspended) // you must resume a suspended thread before abort
                        thread.Resume();

                    thread.Abort();
                }

            Algorithm.runningAlgorithm.Clear();
            

            foreach(var node in Map.labels) {

                node.Image = null;
                node.BackColor = Map.initialLabelColor;
                node.weight = OOPLabel.UnweightValue;
                node.distance = OOPLabel.INF;
                node.gScore = OOPLabel.INF;
                node.hScore = OOPLabel.INF;
                node.fScore = OOPLabel.INF;
            }


            Map.weightedGraph = false;
            Menu.weightButton.ForeColor = Menu.buttonForeColor;
            Menu.BFSButton.ForeColor = Menu.buttonForeColor;
            Menu.DFSButton.ForeColor = Menu.buttonForeColor;
            Map.source = null;
            Map.destination = null;
            Map.sourceFlagAdded = false;
            Map.destinationFlagAdded = false;
            Menu.ResetCountObstacles();
            Menu.ResetExploredNodes();
            Algorithm.algorithmState = Algorithm.AlgorithmState.NeverFinished;
            
        }



        public static void exitClick(object sender, EventArgs e) {

            DialogResult answ = MessageBox.Show("Are you sure?", "Exit program", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answ == DialogResult.Yes) {

                if(Algorithm.runningAlgorithm.Count != 0) {

                    if (Algorithm.runningAlgorithm[0].ThreadState == ThreadState.Suspended) Algorithm.runningAlgorithm[0].Resume();

                    Algorithm.runningAlgorithm[0].Abort();
                }

                Notifier.runningNotifier.Abort();
                Application.ExitThread();            
            }
        }

        public static void formClose(object sender, FormClosingEventArgs e) => exitClick(sender, e); 
        
        public static void mouseEnterMenuButton(object sender, EventArgs e) {
            
            (sender as Button).Cursor = Cursors.Hand;
            (sender as Button).BackColor = Color.DarkGray;
  
        }


        public static void mouseLeaveMenuButton(object sender, EventArgs e) {

            (sender as Button).BackColor = Color.Black;
        }

        public static void BFSClick(object sender, EventArgs e) {

            if (Menu.BFSButton.ForeColor != Color.Red) {

                Algorithm startBFS = new BFS();
                Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Standard;

                startBFS.RunningAlgorithm.Add(new Thread(startBFS.StartAlgorithm));
                startBFS.RunningAlgorithm[startBFS.RunningAlgorithm.Count - 1].Start();
            }

        }


        public static void DFSClick(object sender, EventArgs e) {

            if (Menu.DFSButton.ForeColor != Color.Red) {

                Algorithm startDFS = new DFS();
                Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Standard;

                startDFS.RunningAlgorithm.Add(new Thread(startDFS.StartAlgorithm));
                startDFS.RunningAlgorithm[startDFS.RunningAlgorithm.Count - 1].Start();
            }
        }


        public static void DijkstraClick(object sender, EventArgs e) {

            
            Algorithm startDijkstra = new Dijkstra();
            Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Standard;

            startDijkstra.RunningAlgorithm.Add(new Thread(startDijkstra.StartAlgorithm));
            startDijkstra.RunningAlgorithm[startDijkstra.RunningAlgorithm.Count - 1].Start();
            
        }


        public static void AStarClick(object sender, EventArgs e) {


            Algorithm startAStar = new aStar();
            Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Standard;

            startAStar.RunningAlgorithm.Add(new Thread(startAStar.StartAlgorithm));
            startAStar.RunningAlgorithm[startAStar.RunningAlgorithm.Count - 1].Start();

        }


        public static void MazeClick(object sender, EventArgs e) {

            Algorithm startMazeBacktracker = new MazeBacktracker();

            startMazeBacktracker.RunningAlgorithm.Add(new Thread(startMazeBacktracker.StartAlgorithm));
            startMazeBacktracker.RunningAlgorithm[startMazeBacktracker.RunningAlgorithm.Count - 1].Start();

        }


        public static void speedUpClick(object sender, EventArgs e) {

 
            switch (Algorithm.algorithmSpeed) {

                case Algorithm.AlgorithmSpeed.Paused:
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.VerySlow;
                    if (Algorithm.runningAlgorithm[Algorithm.runningAlgorithm.Count - 1].ThreadState == ThreadState.Suspended)
                        Algorithm.runningAlgorithm[Algorithm.runningAlgorithm.Count - 1].Resume();
                    break;

                case Algorithm.AlgorithmSpeed.VerySlow:
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Slow;
                    break;

                case Algorithm.AlgorithmSpeed.Slow:
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Standard;
                    break;

                case Algorithm.AlgorithmSpeed.Standard:
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Fast;
                    break;

                case Algorithm.AlgorithmSpeed.Fast:
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.VeryFast;
                    break;

                case Algorithm.AlgorithmSpeed.VeryFast:
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Instant;
                    break;             
            }

            Notifier.speedModified = true;

        }


        public static void speedDownClick(object sender, EventArgs e) {


            switch (Algorithm.algorithmSpeed) {

                case Algorithm.AlgorithmSpeed.VerySlow:
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Paused;
                    Algorithm.runningAlgorithm[Algorithm.runningAlgorithm.Count - 1].Suspend();
                    break;

                case Algorithm.AlgorithmSpeed.Slow:
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.VerySlow;
                    break;

                case Algorithm.AlgorithmSpeed.Standard:
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Slow;
                    break;

                case Algorithm.AlgorithmSpeed.Fast:
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Standard;
                    break;

                case Algorithm.AlgorithmSpeed.VeryFast:
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.Fast;
                    break;

                case Algorithm.AlgorithmSpeed.Instant:
                    Algorithm.algorithmSpeed = Algorithm.AlgorithmSpeed.VeryFast;
                    break;

            }

            Notifier.speedModified = true;
        }


        public static void PaintGrid(object sender, PaintEventArgs e) {

            ControlPaint.DrawBorder(e.Graphics, (sender as OOPLabel).DisplayRectangle, Color.Black, ButtonBorderStyle.Outset);
        }


        public static void weightButtonClick(object sender, EventArgs e) {


            if(Menu.weightButton.ForeColor != Color.Red) foreach (var i in Map.labels) i.Cursor = Map.weightedCursor;
                 
        }
        
    }
}