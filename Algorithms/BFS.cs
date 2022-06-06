using System.Collections.Generic;
using System.Threading;



namespace Pathfinder.Algorithms {

    class BFS : Algorithm {

        public override void StartAlgorithm() {

            base.StartAlgorithm();

            algorithmName = AlgorithmName.BFS;
            Notifier.algorithmLaunched = true;
            Queue<OOPLabel> toVisit = new Queue<OOPLabel>();
            toVisit.Enqueue(Map.source);
            path.Add(Map.source, Map.source);

            while (!destinationFound && toVisit.Count > 0) {

                foreach (var i in adjancecyList[toVisit.Peek()] as List<OOPLabel>) {

                    if (!path.Contains(i)) {

                        path.Add(i, toVisit.Peek());
                        toVisit.Enqueue(i);
                        i.BackColor = Map.searchColorBorder;


                        if (i == Map.destination) {

                            destinationFound = true;
                            Map.destination.Image = Map.destinationReachedImage;
                            Map.destination.BackColor = Map.searchColorBorder;
                            break;
                        }
                    }

                }

                toVisit.Peek().BackColor = Map.pathColor;
                Thread.Sleep((int)algorithmSpeed);
                toVisit.Peek().BackColor = Map.searchColor;
                toVisit.Dequeue();

            }

            if(destinationFound) DrawPath();
            Menu.SetExploredNodes();
            algorithmState = AlgorithmState.Finished;

        }

    }
}
