using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace Pathfinder.Algorithms {

    

    class Algorithm {

        public enum AlgorithmState { Running, Finished, NeverFinished }   // am creat asta ca sa pot reincepe un algoritm in momentul cand s-a terminat
        public static AlgorithmState algorithmState;// doar cand e neverfinished nu se va sterge practic totul de pe map


        public static List<Thread> runningAlgorithm;
        public List<Thread> RunningAlgorithm { get => runningAlgorithm; set => runningAlgorithm = value; } // fac asta ca sa pot accesa membrul static in asociere cu un obiect
        public static OrderedDictionary adjancecyList;
        public static byte algorithmSpeed;

        static Algorithm() {

            algorithmState = AlgorithmState.NeverFinished;
            runningAlgorithm = new List<Thread>();
        }

        public virtual void StartAlgorithm() {

            if (Map.source == null || Map.destination == null) {

                RunningAlgorithm[RunningAlgorithm.Count - 1].Abort();
                RunningAlgorithm.RemoveAt(RunningAlgorithm.Count - 1);
            }

            adjancecyList = new OrderedDictionary();
            createAdjencecyList();
            Map.source.Image = Image.FromFile("startSearches.png");         
            Map.source.BackColor = Map.searchColor;

            if (algorithmState == AlgorithmState.Running || algorithmState == AlgorithmState.Finished) {
                
                RunningAlgorithm[RunningAlgorithm.Count - 2].Abort();
                RunningAlgorithm.RemoveAt(RunningAlgorithm.Count - 2);

                for (int i = 0; i < Map.labeluri.GetLength(0); i++)
                    for (int j = 0; j < Map.labeluri.GetLength(1); j++)
                        if (Map.labeluri[i, j].BackColor == Map.searchColor || Map.labeluri[i, j].BackColor == Color.Gold)
                            Map.labeluri[i, j].BackColor = Map.initialLabelColor;

                Map.destination.Image = Image.FromFile("destination.png");
                Map.source.Image = Image.FromFile("startSearches.png");
                Map.source.BackColor = Map.searchColor;
            }

            algorithmState = AlgorithmState.Running;

        }


        protected virtual void GetPath(out List<Label> path) {

            path = new List<Label>();




        }

        protected void createAdjencecyList() {


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
