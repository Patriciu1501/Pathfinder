
using System.Collections.Generic;
using System.Drawing;
using System.Threading;


namespace Pathfinder.Algorithms {

    #region PriorityQueueClass

    class MinPriorityQueue  {

        private List<OOPLabel> storedElements = new List<OOPLabel>();
        public OOPLabel peek;
        public int count;

        public void InsertElement(OOPLabel element) {

            if (storedElements.Count == 0) storedElements.Add(element);

            else {

                for (int i = 0; i < storedElements.Count; i++) {

                    if (element.distance < storedElements[i].distance) {

                        storedElements.Insert(i, element);
                        break;
                    }

                    else if (i == storedElements.Count - 1) {

                        storedElements.Add(element);
                        break;
                    }
                }

            }


            peek = storedElements[0];
            count++;
        }


        public bool Contains(OOPLabel element) {

            if (storedElements.Contains(element)) return true;

            return false;
        }


        public void Remove(OOPLabel element) {

            if (storedElements.Contains(element)) {
                count--;
                storedElements.Remove(element);
                if (storedElements.Count > 0) peek = storedElements[0];
                else peek = null;
            }
            
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

    #endregion

    class Dijkstra : Algorithm {

        public override void StartAlgorithm() {

            base.StartAlgorithm();


            if (!adjancecyList.Contains(Map.source)) {

                algorithmState = AlgorithmState.Finished;
                return;
            }

            algorithmName = AlgorithmName.Dijkstra;
            Tutorial.algorithmLaunched = true;


            List<OOPLabel> visitedNodes = new List<OOPLabel>();
            MinPriorityQueue distances = new MinPriorityQueue();
            Map.source.distance = 0;
            distances.InsertElement(Map.source);
            path.Add(Map.source, Map.source);


            for(int i = 0; i < adjancecyList.Count; i++) {

                if (destinationFound) break;

                OOPLabel minUnprocessed = distances.peek;
                if (minUnprocessed == null) break; // va fi null cand lista distances va fi goala, adica cand nu se poate gasi destinatia
                minUnprocessed.BackColor = Map.searchColor;
                if (minUnprocessed.weight == minUnprocessed.WeightValue) minUnprocessed.Image = Map.weightSearchedImage;
                visitedNodes.Add(minUnprocessed);

                Thread.Sleep((int)algorithmSpeed);

                foreach (var neighbour in adjancecyList[minUnprocessed] as List<OOPLabel>) {
               
                    if (visitedNodes.Contains(neighbour)) continue;

                    if(minUnprocessed.distance + neighbour.weight < neighbour.distance) {

                        neighbour.BackColor = Map.searchColorBorder;
                        if (neighbour.weight == neighbour.WeightValue) neighbour.Image = Map.weightSearchedBorderImage;
                        neighbour.distance = minUnprocessed.distance + neighbour.weight;
                        if (!path.Contains(neighbour)) path.Add(neighbour, minUnprocessed);
                        else path[neighbour] = minUnprocessed;
                    }

                    if (distances.Contains(neighbour)) distances.Remove(neighbour);
                    distances.InsertElement(neighbour);
                    

                    if(neighbour == Map.destination) {

                        destinationFound = true;
                        break;
                    }
                }

                if(distances.count > 0) distances.Remove(distances[0]);

            }

            if(destinationFound) DrawPath();
            algorithmState = AlgorithmState.Finished;
            Menu.SetExploredNodes();
            Menu.weightButton.ForeColor = Menu.buttonForeColor;

            foreach (var i in Map.labels) i.distance = int.MaxValue;

        }

    }
}
