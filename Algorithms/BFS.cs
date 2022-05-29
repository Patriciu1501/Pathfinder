using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;


namespace Pathfinder.Algorithms {

    class BFS : Algorithm {

        public override void StartAlgorithm() {

            base.StartAlgorithm();

            if (!adjancecyList.Contains(Map.source)) {

                algorithmState = AlgorithmState.Finished;
                return;
            }

            algorithmName = AlgorithmName.BFS;
            Tutorial.algorithmLaunched = true;
            Queue<Label> toVisit = new Queue<Label>();
            toVisit.Enqueue(Map.source);
            path.Add(Map.source, Map.source);

            while (!destinationFound && toVisit.Count > 0) {


                for (int i = 0; i < (adjancecyList[toVisit.Peek()] as List<Label>).Count; i++) {

                    if (!path.Contains((adjancecyList[toVisit.Peek()] as List<Label>)[i])) {

                        path.Add((adjancecyList[toVisit.Peek()] as List<Label>)[i], toVisit.Peek());
                        toVisit.Enqueue((adjancecyList[toVisit.Peek()] as List<Label>)[i]);
                        (adjancecyList[toVisit.Peek()] as List<Label>)[i].BackColor = Map.searchColorBorder;


                        if ((adjancecyList[toVisit.Peek()] as List<Label>)[i] == Map.destination) {

                            destinationFound = true;
                            Tutorial.destinationFound = true;
                            Map.destination.Image = Map.destinationReachedImage;
                            Map.destination.BackColor = Map.searchColorBorder;
                            break;
                        }
                    }

                }

                Menu.ModifyExploredNodes();
                toVisit.Peek().BackColor = Map.pathColor;
                Thread.Sleep((int)algorithmSpeed);
                toVisit.Peek().BackColor = Map.searchColor;
                toVisit.Dequeue();

            }

            if(destinationFound) DrawPath();
            algorithmState = AlgorithmState.Finished;

        }

    }
}
