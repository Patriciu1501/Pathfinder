using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace Pathfinder.Algorithms {
    class DFS : Algorithm {

        public override void StartAlgorithm() {

            base.StartAlgorithm();

            algorithmName = AlgorithmName.DFS;
            Notifier.algorithmLaunched = true;
            Helper(Map.source, Map.source);
            if(destinationFound) DrawPath();
            Menu.SetExploredNodes();
            algorithmState = AlgorithmState.Finished;
        }

    
        public bool Helper(OOPLabel sursa, OOPLabel prev) {

            if(sursa == Map.destination) {

                path.Add(sursa, prev);
                Map.destination.BackColor = Map.searchColorBorder;
                Map.destination.Image = Map.destinationReachedImage;
                destinationFound = true;
                return true;
            }

            path.Add(sursa, prev);
            sursa.BackColor = Map.pathColor;
            Thread.Sleep((int)algorithmSpeed);
            sursa.BackColor = Map.searchColor;

            foreach (var i in adjancecyList[sursa] as List<OOPLabel>)
                if (!path.Contains(i) && Helper(i, sursa)) return true;

            return false; // se returneaza false => la apelul precedent se va continua iteratia, apelarea, in cazul daca acel for nu s-a terminat
        }
 
    }

}
