﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Pathfinder.Algorithms {
    class MazeBacktracker: Algorithm {

        private Random randomNumber;
        private List<Tuple<OOPLabel, OOPLabel>> permanentPairs;
        private Dictionary<OOPLabel, List<OOPLabel>> neighbours;
        private OOPLabel startPoint;

        public override void StartAlgorithm() {

            if (runningAlgorithm.Count > 0)
                for (int i = 0; i < runningAlgorithm.Count - 1; i++) {

                    if (runningAlgorithm[i].ThreadState == ThreadState.Suspended) // you must resume a suspended thread before abort
                        runningAlgorithm[i].Resume();

                    runningAlgorithm[i].Abort();
                    runningAlgorithm.RemoveAt(i);
                }


            for (int i = 0; i < Map.labels.GetLength(0); i++)
                for (int j = 0; j < Map.labels.GetLength(1); j++) {

                    Map.labels[i, j].BackColor = Map.initialLabelColor;
                    Map.labels[i, j].Image = null;
                }


            Menu.weightButton.ForeColor = Menu.buttonForeColor;
            Map.weightedGraph = false;
            Menu.BFSButton.ForeColor = Menu.buttonForeColor;
            Menu.DFSButton.ForeColor = Menu.buttonForeColor;
            Map.destination = null;
            Map.source = null;
            Map.destinationFlagAdded = false;
            Map.sourceFlagAdded = false;
            Notifier.algorithmLaunched = true;
            Menu.countObstaclesLabel.Text = "Obstacles: 0";
            algorithmName = AlgorithmName.Maze;
            algorithmState = AlgorithmState.Running;
            randomNumber = new Random();
            neighbours = new Dictionary<OOPLabel, List<OOPLabel>>();
            permanentPairs = new List<Tuple<OOPLabel, OOPLabel>>();
            createAdjencecyList();

            startPoint = Map.labels[0, 0];
            Helper(startPoint);


            List<string> labelsNames = new List<string>();

            foreach(var i in permanentPairs) {

                labelsNames.Add(i.Item1.Name);
                labelsNames.Add(i.Item2.Name);
                labelsNames.Add(Middle(i));
            }

            Thread.Sleep(300);

            for (int i = 0; i < Map.labels.GetLength(0); i++) {

                for (int j = 0; j < Map.labels.GetLength(1); j++) {

                    if (!labelsNames.Contains(Map.labels[i, j].Name)) {

                        string[] splits = Menu.countObstaclesLabel.Text.Split(' ');
                        int nr = Convert.ToInt32(splits[1]);
                        nr++;
                        Menu.countObstaclesLabel.Text = splits[0] + " " + nr;
                        Map.labels[i, j].BackColor = Map.obstacleColor;
                    }
                }
            }


            algorithmState = AlgorithmState.NeverFinished; // neverfinished to avoid notifier
            Thread.Sleep(500);
        }



        private void Helper(OOPLabel curr) {

            if (neighbours.ContainsKey(curr)) return;

            Tuple<int, int> currPos = Map.GetPos(curr.Name);
            List<OOPLabel> vecini = adjancecyList[curr] as List<OOPLabel>;
            neighbours.Add(curr, new List<OOPLabel>());
 

            foreach(var i in vecini) {

                List<OOPLabel> farVecini = adjancecyList[i] as List<OOPLabel>;

                for (int j = 0; j < farVecini.Count; j++) {

                    Tuple<int, int> farNPos = Map.GetPos(farVecini[j].Name);

                    if (!neighbours.ContainsKey(farVecini[j]) && (currPos.Item1 == farNPos.Item1 || currPos.Item2 == farNPos.Item2)) 
                        neighbours[curr].Add(farVecini[j]);
             
                }
            }

            if (neighbours[curr].Count == 0) return;

            while(neighbours[curr].Count > 0) {
                
                int value = randomNumber.Next(0, neighbours[curr].Count);
                
                if (!neighbours.ContainsKey(neighbours[curr][value])) {

                    permanentPairs.Add(Tuple.Create(curr, neighbours[curr][value]));
                    Helper(neighbours[curr][value]);
                }
                neighbours[curr].RemoveAt(value);
            }
            
        }
       

        private string Middle(Tuple<OOPLabel, OOPLabel> pair) {

            string result = string.Empty;

            Tuple<int, int> firstEntity = Map.GetPos(pair.Item1.Name);
            Tuple<int, int> secondEntity = Map.GetPos(pair.Item2.Name);

            if(firstEntity.Item1 == secondEntity.Item1) {

                if (firstEntity.Item2 > secondEntity.Item2) result = firstEntity.Item1.ToString() + " " + (firstEntity.Item2 - 1).ToString();
                else if (firstEntity.Item2 < secondEntity.Item2) result = firstEntity.Item1.ToString() + " " + (firstEntity.Item2 + 1).ToString();
            }

            else if(firstEntity.Item2 == secondEntity.Item2) {

                if (firstEntity.Item1 > secondEntity.Item1) result = (firstEntity.Item1 - 1).ToString() + " " + firstEntity.Item2.ToString();
                else if (firstEntity.Item1 < secondEntity.Item1) result = (firstEntity.Item1 + 1).ToString() + " " + firstEntity.Item2 .ToString();
            }

            return result;
        }

    }
}
