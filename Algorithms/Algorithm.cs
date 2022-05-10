using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Drawing;

namespace Pathfinder.Algorithms {

    

    class Algorithm {

        public enum AlgorithmState{ Running, Finished, NeverFinished }   // am creat asta ca sa pot reincepe un algoritm in momentul cand s-a terminat
        public static AlgorithmState algorithmState;// doar cand e neverfinished nu se va sterge practic totul de pe map
        public enum AlgorithmSpeed: byte { 

            Paused = 200, 
            VerySlow = 50, 
            Slow = 40, 
            Standard = 30, 
            Fast = 20, 
            VeryFast = 10,
            Instant = 0
        
        };
        public static AlgorithmSpeed algorithmSpeed;


        public static List<Thread> runningAlgorithm;
        public List<Thread> RunningAlgorithm { get => runningAlgorithm; set => runningAlgorithm = value; } // fac asta ca sa pot accesa membrul static in asociere cu un obiect
        public static OrderedDictionary adjancecyList;
        public OrderedDictionary path;
        protected bool destinationFound;

        static Algorithm() {

            algorithmSpeed = AlgorithmSpeed.VeryFast;
            algorithmState = AlgorithmState.NeverFinished;
            runningAlgorithm = new List<Thread>();
        }

        public virtual void StartAlgorithm() {

            if (Map.source == null || Map.destination == null) {

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

                for (int i = 0; i < Map.labeluri.GetLength(0); i++)
                    for (int j = 0; j < Map.labeluri.GetLength(1); j++)
                        if (Map.labeluri[i, j].BackColor != Map.obstacleColor) {

                            Map.labeluri[i, j].BackColor = Map.initialLabelColor;
                            Map.labeluri[i, j].Image = null;
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
            Func<Label, bool> VecinNotObstacle = (Label a) => a.BackColor == Map.obstacleColor ? false : true;

            for (int i = 0; i < Map.labeluri.GetLength(0); i++) {

                for (int j = 0; j < Map.labeluri.GetLength(1); j++) {

                    if (Map.labeluri[i, j].BackColor == Map.obstacleColor) continue;

                    adjancecyList.Add(Map.labeluri[i, j], new List<Label>());

                    if (i == 0 && j == 0) {

                        if (VecinNotObstacle(Map.labeluri[i + 1, j])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i + 1, j]);
                        if (VecinNotObstacle(Map.labeluri[i, j + 1])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i, j + 1]);
                    }

                    else if (i == 0 && (j != 0 && j != Map.labeluri.GetLength(1) - 1)) {

                        if (VecinNotObstacle(Map.labeluri[i + 1, j])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i + 1, j]);
                        if (VecinNotObstacle(Map.labeluri[i, j + 1])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i, j + 1]);
                        if (VecinNotObstacle(Map.labeluri[i, j - 1])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i, j - 1]);
                    }

                    else if (i == 0 && j == Map.labeluri.GetLength(1) - 1) {

                        if (VecinNotObstacle(Map.labeluri[i + 1, j])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i + 1, j]);
                        if (VecinNotObstacle(Map.labeluri[i, j - 1])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i, j - 1]);
                    }


                    else if (j == 0 && (i != 0 && i != Map.labeluri.GetLength(0) - 1)) {


                        if (VecinNotObstacle(Map.labeluri[i + 1, j])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i + 1, j]);
                        if (VecinNotObstacle(Map.labeluri[i - 1, j])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i - 1, j]);
                        if (VecinNotObstacle(Map.labeluri[i, j + 1])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i, j + 1]);
                    }


                    else if ((i != 0 && i != Map.labeluri.GetLength(0) - 1) && (j != 0 && j != Map.labeluri.GetLength(1) - 1)) {

                        if (VecinNotObstacle(Map.labeluri[i + 1, j])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i + 1, j]);
                        if (VecinNotObstacle(Map.labeluri[i - 1, j])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i - 1, j]);
                        if (VecinNotObstacle(Map.labeluri[i, j + 1])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i, j + 1]);
                        if (VecinNotObstacle(Map.labeluri[i, j - 1])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i, j - 1]);

                    }


                    else if (j == Map.labeluri.GetLength(1) - 1 && (i != 0 && i != Map.labeluri.GetLength(0) - 1)) {

                        if (VecinNotObstacle(Map.labeluri[i + 1, j])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i + 1, j]);
                        if (VecinNotObstacle(Map.labeluri[i - 1, j])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i - 1, j]);
                        if (VecinNotObstacle(Map.labeluri[i, j - 1])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i, j - 1]);
                    }


                    else if (i == Map.labeluri.GetLength(0) - 1 && j == 0) {

                        if (VecinNotObstacle(Map.labeluri[i - 1, j])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i - 1, j]);
                        if (VecinNotObstacle(Map.labeluri[i, j + 1])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i, j + 1]);
                    }


                    else if (i == Map.labeluri.GetLength(0) - 1 && (j != 0 && j != Map.labeluri.GetLength(1) - 1)) {

                        if (VecinNotObstacle(Map.labeluri[i - 1, j])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i - 1, j]);
                        if (VecinNotObstacle(Map.labeluri[i, j + 1])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i, j + 1]);
                        if (VecinNotObstacle(Map.labeluri[i, j - 1])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i, j - 1]);
                    }


                    else if (i == Map.labeluri.GetLength(0) - 1 && j == Map.labeluri.GetLength(1) - 1) {

                        if (VecinNotObstacle(Map.labeluri[i - 1, j])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i - 1, j]);
                        if (VecinNotObstacle(Map.labeluri[i, j - 1])) (adjancecyList[Map.labeluri[i, j]] as List<Label>).Add(Map.labeluri[i, j - 1]);
                    }

                    if ((adjancecyList[Map.labeluri[i, j]] as List<Label>).Count == 0) adjancecyList.RemoveAt(adjancecyList.Count - 1); // celulele "inconjurate" de obstacole nu vor fi adaugate
                }

            }

        }


    }
}
