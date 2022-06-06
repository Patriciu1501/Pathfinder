using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;

namespace Pathfinder.Algorithms {

    class MinPriorityQueue<T> {

        private List<OOPLabel> storedElements = new List<OOPLabel>();
        public OOPLabel peek;
        public int count;

        public void InsertElement(params OOPLabel[] elements) {

            for (int z = 0; z < elements.Length; z++) {

                if (storedElements.Count == 0) storedElements.Add(elements[z]);

                else if (typeof(T) == typeof(Dijkstra)) {


                    for (int i = 0; i < storedElements.Count; i++) {


                        if (elements[z].distance < storedElements[i].distance) {

                            storedElements.Insert(i, elements[z]);
                            break;
                        }


                        else if (i == storedElements.Count - 1) {

                            storedElements.Add(elements[z]);
                            break;
                        }
                    }
                }

                else if (typeof(T) == typeof(aStar)) {

                    for (int i = 0; i < storedElements.Count; i++) {

                        if (elements[z].fScore < storedElements[i].fScore) {

                            storedElements.Insert(i, elements[z]);
                            break;
                        }


                        else if (i == storedElements.Count - 1) {

                            storedElements.Add(elements[z]);
                            break;
                        }
                    }
                }

                peek = storedElements[0];
                count++;

            }
        }


        public void Clear() {

            storedElements.Clear();
            peek = null;
        }

        public bool Contains(OOPLabel element) {

            if (storedElements.Contains(element)) return true;

            return false;
        }


        public void RemoveFront() {

            if (storedElements.Count == 0) return;

            storedElements.Remove(storedElements[0]);
            if (storedElements.Count == 0) peek = null;
            else peek = storedElements[0];

        }


        public void Refresh() {

            storedElements = storedElements.OrderBy((node) => node.distance).ToList();
            peek = storedElements[0];
        }

        public OOPLabel this[int i] {

            get {

                return storedElements[i];
            }

            set {

                storedElements[i] = value;
            }
        }

    }


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
            runningAlgorithm = new List<Thread>();
        }

        public virtual void StartAlgorithm() {

            if (!Map.sourceFlagAdded || !Map.destinationFlagAdded) {

                runningAlgorithm[runningAlgorithm.Count - 1].Abort();
                runningAlgorithm.RemoveAt(runningAlgorithm.Count - 1);
            }       

            if (algorithmState == AlgorithmState.Running || algorithmState == AlgorithmState.Finished) {
                
                if(runningAlgorithm[runningAlgorithm.Count - 2].ThreadState == ThreadState.Suspended) {

                    runningAlgorithm[runningAlgorithm.Count - 2].Resume();
                    algorithmSpeed = AlgorithmSpeed.Standard;
                }

                runningAlgorithm[runningAlgorithm.Count - 2].Abort();
                runningAlgorithm.RemoveAt(runningAlgorithm.Count - 2);

                foreach(var node in Map.labels) {

                    if (node.weight == OOPLabel.WeightValue) node.Image = Map.weightInitialImage;

                    if (node.BackColor != Map.obstacleColor) node.BackColor = Map.initialLabelColor;
                }
                            

                Map.destination.Image = Map.destinationImage;
                Map.source.Image = Map.sourceSearchesImage;
                Map.source.BackColor = Map.searchColor;
                Menu.exploredNodesLabel.Text = "Explored: 0";
            }

            foreach (var i in Map.labels) {

                i.distance = OOPLabel.INF;
                i.gScore = OOPLabel.INF;
                i.hScore = OOPLabel.INF;
                i.fScore = OOPLabel.INF;
            }
            Menu.weightButton.ForeColor = Color.Red;
            destinationFound = false;
            path = new OrderedDictionary();
            adjancecyList = new OrderedDictionary();
            createAdjencecyList();
            Map.source.Image = Map.sourceSearchesImage;

            if (Events.destinationDragging) algorithmSpeed = AlgorithmSpeed.Instant; // daca algoritmul se lanseaza cand "trag" de destinatie, viteza va fi instanta
            algorithmState = AlgorithmState.Running;

        }


        protected void DrawPath() {

            OOPLabel last = path[path.Count - 1] as OOPLabel;
            List<OOPLabel> lista = new List<OOPLabel>();
            lista.Add(Map.destination);

            while (last != Map.source) {

                if (last == null) break;
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

            Menu.weightButton.ForeColor = Menu.buttonForeColor;
        }

        public void createAdjencecyList() {


            adjancecyList = new OrderedDictionary();
            Func<OOPLabel, bool> NeighbourNotObstacle = neighbour => neighbour.BackColor != Map.obstacleColor;

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
                }
            }
        }
    }
}
