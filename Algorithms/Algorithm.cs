using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Specialized;
using System.Drawing;

namespace Pathfinder.Algorithms {

    

    partial class Algorithm {

        public static AlgorithmState algorithmState;
        public static AlgorithmSpeed algorithmSpeed;
        public static AlgorithmName algorithmName;
        public static List<Thread> runningAlgorithm;
        public static bool destinationFound;

        public List<Thread> RunningAlgorithm { get => runningAlgorithm; set => runningAlgorithm = value; } // fac asta ca sa pot accesa membrul static in asociere cu un obiect
        public OrderedDictionary adjancecyList;
        public OrderedDictionary path;

        static Algorithm() {  // initializez membrii statici intr-un constructor static

            algorithmSpeed = AlgorithmSpeed.Standard;
            algorithmState = AlgorithmState.NeverFinished;
            algorithmName = AlgorithmName.None;
            runningAlgorithm = new List<Thread>();
        }

        public virtual void StartAlgorithm() {

            if (!Map.sourceFlagAdded || !Map.destinationFlagAdded) {

                runningAlgorithm[runningAlgorithm.Count - 1].Abort();
                runningAlgorithm.RemoveAt(runningAlgorithm.Count - 1);
            }       

            if (algorithmState == AlgorithmState.Running || algorithmState == AlgorithmState.Finished) {
                
                if(algorithmSpeed == AlgorithmSpeed.Paused) {

                    runningAlgorithm[runningAlgorithm.Count - 2].Resume();
                    algorithmSpeed = AlgorithmSpeed.Standard;
                }

                runningAlgorithm[runningAlgorithm.Count - 2].Abort();
                runningAlgorithm.RemoveAt(runningAlgorithm.Count - 2);

                for (int i = 0; i < Map.labels.GetLength(0); i++) {

                    for (int j = 0; j < Map.labels.GetLength(1); j++) {

                        if (Map.labels[i, j].Image != null && Map.labels[i, j] != Map.destination && Map.labels[i, j] != Map.source) continue;

                        else if (Map.labels[i, j].BackColor != Map.obstacleColor) {

                            Map.labels[i, j].BackColor = Map.initialLabelColor;
                            Map.labels[i, j].Image = null;
                        }
                    }
                }
                            

                Map.destination.Image = Map.destinationImage;
                Map.source.Image = Map.sourceSearchesImage;
                Map.source.BackColor = Map.searchColor;
                Menu.exploredNodesLabel.Text = "Explored: 0";
            }

            Menu.weightButton.ForeColor = Color.Red;
            destinationFound = false;
            path = new OrderedDictionary();
            adjancecyList = new OrderedDictionary();
            createAdjencecyList();
            Map.source.Image = Map.sourceSearchesImage;
            Map.source.BackColor = Map.searchColor;

            algorithmState = AlgorithmState.Running;

        }


        protected void DrawPath() {

            OOPLabel last = path[path.Count - 1] as OOPLabel;
            List<OOPLabel> lista = new List<OOPLabel>();
            lista.Add(Map.destination);

            while (last != Map.source) {

                lista.Add(last);
                last = path[last] as OOPLabel;
            }

            lista.Add(Map.source);
            lista.Reverse();

            OOPLabel tempSource = Map.source;

            foreach (var i in lista) {

                Thread.Sleep((int)algorithmSpeed);
                tempSource.Image = null;
                tempSource = i;
                i.Image = Map.sourcePath;
                tempSource.BackColor = Map.pathColor;
            }

            Menu.weightButton.ForeColor = Color.FromArgb(0, 207, 255);
        }

        public void createAdjencecyList() {


            adjancecyList = new OrderedDictionary();
            Func<OOPLabel, bool> NeighbourNotObstacle = neighbour => neighbour.BackColor == Map.obstacleColor ? false : true;

            for (int i = 0; i < Map.labels.GetLength(0); i++) {

                for (int j = 0; j < Map.labels.GetLength(1); j++) {

                    if (Map.labels[i, j].BackColor == Map.obstacleColor) continue;

                    adjancecyList.Add(Map.labels[i, j], new List<OOPLabel>());

                    if (i == 0 && j == 0) {

                        if (NeighbourNotObstacle(Map.labels[i + 1, j])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i + 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j + 1])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i, j + 1]);
                    }

                    else if (i == 0 && (j != 0 && j != Map.labels.GetLength(1) - 1)) {

                        if (NeighbourNotObstacle(Map.labels[i + 1, j])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i + 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j + 1])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i, j + 1]);
                        if (NeighbourNotObstacle(Map.labels[i, j - 1])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i, j - 1]);
                    }

                    else if (i == 0 && j == Map.labels.GetLength(1) - 1) {

                        if (NeighbourNotObstacle(Map.labels[i + 1, j])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i + 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j - 1])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i, j - 1]);
                    }


                    else if (j == 0 && (i != 0 && i != Map.labels.GetLength(0) - 1)) {


                        if (NeighbourNotObstacle(Map.labels[i + 1, j])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i + 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i - 1, j])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i - 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j + 1])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i, j + 1]);
                    }


                    else if ((i != 0 && i != Map.labels.GetLength(0) - 1) && (j != 0 && j != Map.labels.GetLength(1) - 1)) {

                        if (NeighbourNotObstacle(Map.labels[i + 1, j])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i + 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i - 1, j])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i - 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j + 1])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i, j + 1]);
                        if (NeighbourNotObstacle(Map.labels[i, j - 1])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i, j - 1]);

                    }


                    else if (j == Map.labels.GetLength(1) - 1 && (i != 0 && i != Map.labels.GetLength(0) - 1)) {

                        if (NeighbourNotObstacle(Map.labels[i + 1, j])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i + 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i - 1, j])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i - 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j - 1])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i, j - 1]);
                    }


                    else if (i == Map.labels.GetLength(0) - 1 && j == 0) {

                        if (NeighbourNotObstacle(Map.labels[i - 1, j])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i - 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j + 1])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i, j + 1]);
                    }


                    else if (i == Map.labels.GetLength(0) - 1 && (j != 0 && j != Map.labels.GetLength(1) - 1)) {

                        if (NeighbourNotObstacle(Map.labels[i - 1, j])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i - 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j + 1])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i, j + 1]);
                        if (NeighbourNotObstacle(Map.labels[i, j - 1])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i, j - 1]);
                    }


                    else if (i == Map.labels.GetLength(0) - 1 && j == Map.labels.GetLength(1) - 1) {

                        if (NeighbourNotObstacle(Map.labels[i - 1, j])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i - 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j - 1])) (adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Add(Map.labels[i, j - 1]);
                    }

                    if ((adjancecyList[Map.labels[i, j]] as List<OOPLabel>).Count == 0) adjancecyList.RemoveAt(adjancecyList.Count - 1); // celulele "inconjurate" de obstacole nu vor fi adaugate
                }

            }

        }


    }
}
