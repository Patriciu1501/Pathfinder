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
            List<Label> visited = new List<Label>();
            bool destinationReached = false;

            toVisit.Enqueue(Map.source);
            visited.Add(Map.source);

            while (!destinationReached && toVisit.Count > 0) {


                for (int i = 0; i < (adjancecyList[toVisit.Peek()] as List<Label>).Count; i++) {

                    if (!visited.Contains((adjancecyList[toVisit.Peek()] as List<Label>)[i])) {

                        toVisit.Enqueue((adjancecyList[toVisit.Peek()] as List<Label>)[i]);
                        visited.Add((adjancecyList[toVisit.Peek()] as List<Label>)[i]);
                        visited[visited.Count - 1].BackColor = Map.searchColorBorder;


                        if (visited[visited.Count - 1] == Map.destination) {

                            destinationReached = true;
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

                toVisit.Peek().BackColor = Color.Gold;
                Thread.Sleep(algorithmSpeed = 20);
                toVisit.Peek().BackColor = Map.searchColor;
                toVisit.Dequeue();

            }

            algorithmState = AlgorithmState.Finished;
         
        }


        protected override void GetPath(out List<Label> path) {

            base.GetPath(out path);

        }
    }
}
