using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinder.Algorithms {
    partial class Algorithm {

        public enum AlgorithmState { Running, Finished, NeverFinished }   // am creat asta ca sa pot reincepe un algoritm in momentul cand s-a terminat
        // doar cand e neverfinished nu se va sterge practic totul de pe map
        public enum AlgorithmSpeed : byte {

            Paused = 120,
            VerySlow = 100,
            Slow = 80,
            Standard = 60,
            Fast = 40,
            VeryFast = 20,
            Instant = 0

        };

        public enum AlgorithmName {

            None,
            BFS,
            DFS,
            Dijkstra,
            aStar,
            Maze
        }
    }
}
