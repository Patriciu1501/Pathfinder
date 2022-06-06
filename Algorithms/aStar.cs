using System;
using System.Collections.Generic;
using System.Threading;


namespace Pathfinder.Algorithms {
    class aStar: Algorithm {

        private int HFunction(OOPLabel node) {
            
            Tuple<int, int> firstCoords = Map.GetPos(node.Name);
            Tuple<int, int> secondCoords = Map.GetPos(Map.destination.Name);

            return Math.Abs(firstCoords.Item1 - secondCoords.Item1) + Math.Abs(firstCoords.Item2 - secondCoords.Item2);
            
        }


        public override void StartAlgorithm() {
           
            base.StartAlgorithm();
            algorithmName = AlgorithmName.aStar;

            MinPriorityQueue<aStar> coada = new MinPriorityQueue<aStar>();
            Map.source.gScore = 0;
            Map.source.hScore = HFunction(Map.source);
            Map.source.fScore = Map.source.hScore;
            coada.InsertElement(Map.source);

            while(coada.count > 0 && coada.peek != null) {

                OOPLabel curr = coada.peek;

                if(curr == Map.destination) {

                    destinationFound = true;
                    break;
                }

                if (curr.IsWeighted())curr.Image = Map.weightSearchedImage;

                List <OOPLabel> adjencecyNodes = adjancecyList[curr] as List<OOPLabel>;
                coada.RemoveFront();

                Thread.Sleep((int)algorithmSpeed);

                foreach(var node in adjencecyNodes) {

                    if (node.IsWeighted()) node.Image = Map.weightSearchedImage;
                    node.BackColor = Map.searchColor;
                    int tempDist = curr.gScore + node.weight;

                    if (tempDist < node.gScore) {

                        node.hScore = HFunction(node);
                        node.gScore = tempDist;
                        node.fScore = node.hScore + node.gScore;

                        if (!path.Contains(node)) path.Add(node, curr);
                       
                        coada.InsertElement(node);
                    }
                }
            }

            if (destinationFound) DrawPath();
            algorithmState = AlgorithmState.Finished;
            Menu.SetExploredNodes();
            Menu.weightButton.ForeColor = Menu.buttonForeColor;

            foreach (var i in Map.labels) {

                i.distance = OOPLabel.INF;
                i.gScore = OOPLabel.INF;
                i.hScore = OOPLabel.INF;
                i.fScore = OOPLabel.INF;        
            }         
        }
    }
}
