
using System.Windows.Forms;

namespace Pathfinder {
    public class OOPLabel : Label {

        public const byte WeightValue = 5;
        public const byte UnweightValue = 1;
        public const int INF = int.MaxValue;

        public byte weight;
        public int distance;
        public int fScore;
        public int gScore;
        public int hScore;


        public OOPLabel() {

            weight = UnweightValue;
            distance = INF;
            fScore = INF;
            gScore = INF;
            hScore = INF;
        }

        public bool IsWeighted() => weight == WeightValue ? true : false;
    }
}

