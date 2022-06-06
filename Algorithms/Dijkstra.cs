
using System.Collections.Generic;
using System.Threading;


namespace Pathfinder.Algorithms {


    class Dijkstra : Algorithm {

        public override void StartAlgorithm() {

            base.StartAlgorithm();

            algorithmName = AlgorithmName.Dijkstra;
            Notifier.algorithmLaunched = true;
        
            MinPriorityQueue<Dijkstra> distances = new MinPriorityQueue<Dijkstra>();
            Map.source.distance = 0;
            distances.InsertElement(Map.source);
            path.Add(Map.source, Map.source);


            for(int i = 0; i < adjancecyList.Count; i++) {

                if (destinationFound) break;

                OOPLabel minUnprocessed = distances.peek;

                if (minUnprocessed == null) break;

                minUnprocessed.BackColor = Map.searchColor;
                if (minUnprocessed.IsWeighted()) minUnprocessed.Image = Map.weightSearchedImage;

                Thread.Sleep((int)algorithmSpeed);

                foreach (var neighbour in adjancecyList[minUnprocessed] as List<OOPLabel>) {
               
                    if (path.Contains(neighbour)) continue;

                    if(minUnprocessed.distance + neighbour.weight < neighbour.distance) {

                        neighbour.BackColor = Map.searchColorBorder;
                        if (neighbour.IsWeighted()) neighbour.Image = Map.weightSearchedBorderImage;
                        neighbour.distance = minUnprocessed.distance + neighbour.weight;
                        if (!path.Contains(neighbour)) path.Add(neighbour, minUnprocessed);
                        else path[neighbour] = minUnprocessed;
                    }

          
                    if(!distances.Contains(neighbour)) distances.InsertElement(neighbour);
                    

                    if(neighbour == Map.destination) {

                        destinationFound = true;
                        break;
                    }
                }

                distances.RemoveFront();
            }

            if(destinationFound) DrawPath();
            algorithmState = AlgorithmState.Finished;
            Menu.SetExploredNodes();
            Menu.weightButton.ForeColor = Menu.buttonForeColor;

            foreach (var i in Map.labels) i.distance = OOPLabel.INF; 

        }

    }
}
