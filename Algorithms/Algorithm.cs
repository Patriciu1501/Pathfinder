﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace Pathfinder.Algorithms {

    

    class Algorithm {

        public enum AlgorithmState{ Running, Finished, NeverFinished }   // am creat asta ca sa pot reincepe un algoritm in momentul cand s-a terminat
        public static AlgorithmState algorithmState;// doar cand e neverfinished nu se va sterge practic totul de pe map
        public enum AlgorithmSpeed: byte { 

            Paused = 200, 
            VerySlow = 100, 
            Slow = 80, 
            Standard = 60, 
            Fast = 40, 
            VeryFast = 20,
            Instant = 0
        
        };
        public static AlgorithmSpeed algorithmSpeed;


        public static List<Thread> runningAlgorithm;
        public List<Thread> RunningAlgorithm { get => runningAlgorithm; set => runningAlgorithm = value; } // fac asta ca sa pot accesa membrul static in asociere cu un obiect
        public static OrderedDictionary adjancecyList;
        public OrderedDictionary path;
        protected bool destinationFound;

        static Algorithm() {  // initializez membrii statici intr-un constructor static

            algorithmSpeed = AlgorithmSpeed.Standard;
            algorithmState = AlgorithmState.NeverFinished;
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

                for (int i = 0; i < Map.labels.GetLength(0); i++)
                    for (int j = 0; j < Map.labels.GetLength(1); j++)
                        if (Map.labels[i, j].BackColor != Map.obstacleColor) {

                            Map.labels[i, j].BackColor = Map.initialLabelColor;
                            Map.labels[i, j].Image = null;
                        }
                            

                Map.destination.Image = Map.destinationImage;
                Map.source.Image = Map.sourceSearchesImage;
                Map.source.BackColor = Map.searchColor;
                Menu.exploredNodes.Text = "Explored: 0";
            }

            destinationFound = false;
            path = new OrderedDictionary();
            adjancecyList = new OrderedDictionary();
            createAdjencecyList();
            Map.source.Image = Map.sourceSearchesImage;
            Map.source.BackColor = Map.searchColor;

            algorithmState = AlgorithmState.Running;

        }


        protected void DrawPath() {

            Label last = path[path.Count - 1] as Label;
            List<Label> lista = new List<Label>();
            lista.Add(Map.destination);

            while (last != Map.source) {

                lista.Add(last);
                last = path[last] as Label;
            }

            lista.Add(Map.source);
            lista.Reverse();

            Label tempSource = Map.source;

            foreach (var i in lista) {

                Thread.Sleep((int)algorithmSpeed);
                tempSource.Image = null;
                tempSource = i;
                i.Image = Map.sourcePath;
                tempSource.BackColor = Map.pathColor;
            }
        }

        public static void createAdjencecyList() {


            adjancecyList = new OrderedDictionary();
            Func<Label, bool> NeighbourNotObstacle = neighbour => neighbour.BackColor == Map.obstacleColor ? false : true;

            for (int i = 0; i < Map.labels.GetLength(0); i++) {

                for (int j = 0; j < Map.labels.GetLength(1); j++) {

                    if (Map.labels[i, j].BackColor == Map.obstacleColor) continue;

                    adjancecyList.Add(Map.labels[i, j], new List<Label>());

                    if (i == 0 && j == 0) {

                        if (NeighbourNotObstacle(Map.labels[i + 1, j])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i + 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j + 1])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i, j + 1]);
                    }

                    else if (i == 0 && (j != 0 && j != Map.labels.GetLength(1) - 1)) {

                        if (NeighbourNotObstacle(Map.labels[i + 1, j])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i + 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j + 1])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i, j + 1]);
                        if (NeighbourNotObstacle(Map.labels[i, j - 1])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i, j - 1]);
                    }

                    else if (i == 0 && j == Map.labels.GetLength(1) - 1) {

                        if (NeighbourNotObstacle(Map.labels[i + 1, j])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i + 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j - 1])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i, j - 1]);
                    }


                    else if (j == 0 && (i != 0 && i != Map.labels.GetLength(0) - 1)) {


                        if (NeighbourNotObstacle(Map.labels[i + 1, j])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i + 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i - 1, j])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i - 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j + 1])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i, j + 1]);
                    }


                    else if ((i != 0 && i != Map.labels.GetLength(0) - 1) && (j != 0 && j != Map.labels.GetLength(1) - 1)) {

                        if (NeighbourNotObstacle(Map.labels[i + 1, j])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i + 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i - 1, j])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i - 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j + 1])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i, j + 1]);
                        if (NeighbourNotObstacle(Map.labels[i, j - 1])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i, j - 1]);

                    }


                    else if (j == Map.labels.GetLength(1) - 1 && (i != 0 && i != Map.labels.GetLength(0) - 1)) {

                        if (NeighbourNotObstacle(Map.labels[i + 1, j])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i + 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i - 1, j])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i - 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j - 1])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i, j - 1]);
                    }


                    else if (i == Map.labels.GetLength(0) - 1 && j == 0) {

                        if (NeighbourNotObstacle(Map.labels[i - 1, j])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i - 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j + 1])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i, j + 1]);
                    }


                    else if (i == Map.labels.GetLength(0) - 1 && (j != 0 && j != Map.labels.GetLength(1) - 1)) {

                        if (NeighbourNotObstacle(Map.labels[i - 1, j])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i - 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j + 1])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i, j + 1]);
                        if (NeighbourNotObstacle(Map.labels[i, j - 1])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i, j - 1]);
                    }


                    else if (i == Map.labels.GetLength(0) - 1 && j == Map.labels.GetLength(1) - 1) {

                        if (NeighbourNotObstacle(Map.labels[i - 1, j])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i - 1, j]);
                        if (NeighbourNotObstacle(Map.labels[i, j - 1])) (adjancecyList[Map.labels[i, j]] as List<Label>).Add(Map.labels[i, j - 1]);
                    }

                    if ((adjancecyList[Map.labels[i, j]] as List<Label>).Count == 0) adjancecyList.RemoveAt(adjancecyList.Count - 1); // celulele "inconjurate" de obstacole nu vor fi adaugate
                }

            }

        }


    }
}
