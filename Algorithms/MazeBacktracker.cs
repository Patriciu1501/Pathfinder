using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Pathfinder.Algorithms {
    class MazeBacktracker: Algorithm {


        public static Random randomNumber;
        public static List<Tuple<Label, Label>> permanentPairs;
        public static Dictionary<Label, List<Label>> neighbours;
        public override void StartAlgorithm() {

            // aici nu am base apel pentru ca e nevoie de un comportament mai diferit a metodei respective, ea nefiind una de pathfinding
            if (algorithmState == AlgorithmState.Running || algorithmState == AlgorithmState.Finished) {

                if (algorithmSpeed == AlgorithmSpeed.Paused) {

                    runningAlgorithm[runningAlgorithm.Count - 2].Resume();
                    algorithmSpeed = AlgorithmSpeed.Standard;
                }

                runningAlgorithm[runningAlgorithm.Count - 2].Abort();
                runningAlgorithm.RemoveAt(runningAlgorithm.Count - 2);

                for (int i = 0; i < Map.labels.GetLength(0); i++)
                    for (int j = 0; j < Map.labels.GetLength(1); j++)
                         {
                            Map.labels[i, j].BackColor = Map.initialLabelColor;
                            Map.labels[i, j].Image = null;
                        }
                            
            }

            Map.source = null;
            Map.destination = null;
            Map.destinationFlagAdded = false;
            Map.sourceFlagAdded = false;
            randomNumber = new Random();
            neighbours = new Dictionary<Label, List<Label>>();
            permanentPairs = new List<Tuple<Label, Label>>();
            createAdjencecyList();
            algorithmState = AlgorithmState.Running;


            Helper(Map.labels[0, 0]);


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

                        Map.labels[i, j].BackColor = Map.obstacleColor;
                    }
                }
            }


            Menu.countObstacles.Text = "Obstacles: " + (Map.labels.Length - labelsNames.Count);
            algorithmState = AlgorithmState.Finished;
            Thread.Sleep(500);
        }



        static void Helper(Label curr) {

            if (neighbours.ContainsKey(curr)) return;

            //EntryPoint.fisier.WriteLine("\n*" + curr.Name + "*");
            Tuple<int, int> currPos = Map.GetPos(curr.Name);
            List<Label> vecini = adjancecyList[curr] as List<Label>;
            neighbours.Add(curr, new List<Label>());
 


            for (int i = 0; i < vecini.Count; i++) {

                List<Label> farVecini = adjancecyList[vecini[i]] as List<Label>;

                for (int j = 0; j < farVecini.Count; j++) {

                    Tuple<int, int> farNPos = Map.GetPos(farVecini[j].Name);

                    if (!neighbours.ContainsKey(farVecini[j]) && (currPos.Item1 == farNPos.Item1 || currPos.Item2 == farNPos.Item2)) 
                        neighbours[curr].Add(farVecini[j]);
             
                }
            }

            if (neighbours[curr].Count == 0) return;

            while(neighbours[curr].Count > 0) {
                
                int value = randomNumber.Next(0, neighbours[curr].Count);
                
                //EntryPoint.fisier.WriteLine(curr.Name + "->" + neighbours[curr][value].Name);
                if (!neighbours.ContainsKey(neighbours[curr][value])) {

                    permanentPairs.Add(Tuple.Create(curr, neighbours[curr][value]));
                    Helper(neighbours[curr][value]);
                }
                neighbours[curr].RemoveAt(value);
            }

            
        }
       

        static string Middle(Tuple<Label,Label> pair) {

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
