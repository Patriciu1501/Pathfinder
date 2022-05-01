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

            List<Label> visited = new List<Label>();
            bool stopFunction = false;
            PreorderTraversal(Map.source, visited, ref stopFunction);
            algorithmState = AlgorithmState.Finished;

        }

    
        public static void PreorderTraversal(Label sursa, List<Label> visited, ref bool stopFunciton) {

            if(sursa == Map.destination) {

                Map.destination.BackColor = Map.searchColorBorder;
                Map.destination.Image = Map.destinationReachedImage;
                stopFunciton = true;
                return;
            }

            visited.Add(sursa);
            sursa.BackColor = Color.Gold;
            Thread.Sleep(20);
            sursa.BackColor = Map.searchColor;

            #region Explored Nodes
            string[] splits = Menu.exploredNodes.Text.Split(' ');
            int nr = Convert.ToInt32(splits[1]);
            nr++;
            Menu.exploredNodes.Text = splits[0] + " " + nr;
            #endregion

            foreach (var i in adjancecyList[sursa] as List<Label>) 
                if (!visited.Contains(i) && stopFunciton == false) PreorderTraversal(i, visited, ref stopFunciton);

        }
    
  

        protected override void GetPath(out List<Label> path) {

            base.GetPath(out path);

        }
    }

}
