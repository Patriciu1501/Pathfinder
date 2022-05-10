using System;
using System.Collections.Generic;
using System.Drawing;
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
                            Map.destination.Image = Map.destinationReachedImage;
                            Map.destination.BackColor = Map.searchColorBorder;
                            break;
                        }
                    }

                }

                #region Explored Nodes
                string[] splits = Menu.exploredNodes.Text.Split(' ');
                int nr = Convert.ToInt32(splits[1]);
                nr++;
                Menu.exploredNodes.Text = splits[0] + " " + nr;
                #endregion

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
