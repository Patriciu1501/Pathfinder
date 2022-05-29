using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace Pathfinder.Algorithms {
    class DFS : Algorithm {

        public override void StartAlgorithm() {

            base.StartAlgorithm();

            if (!adjancecyList.Contains(Map.source)) {

                algorithmState = AlgorithmState.Finished;
                return;
            }

            algorithmName = AlgorithmName.DFS;
            Tutorial.algorithmLaunched = true;
            bool stopFunction = false;
            Helper(Map.source, Map.source, ref stopFunction);
            if(destinationFound) DrawPath();
            algorithmState = AlgorithmState.Finished;

        }

    
        public void Helper(Label sursa, Label prev, ref bool stopFunciton) {

            if(sursa == Map.destination) {

                path.Add(sursa, prev);
                Map.destination.BackColor = Map.searchColorBorder;
                Map.destination.Image = Map.destinationReachedImage;
                destinationFound = true;
                stopFunciton = true;
                return;
            }

            path.Add(sursa, prev);
            sursa.BackColor = Map.pathColor;
            Thread.Sleep((int)algorithmSpeed);
            sursa.BackColor = Map.searchColor;

            Menu.ModifyExploredNodes();

            foreach (var i in adjancecyList[sursa] as List<Label>) 
                if (!path.Contains(i) && stopFunciton == false) Helper(i, sursa, ref stopFunciton);

        }
 
    }

}
