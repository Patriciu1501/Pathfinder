
using System.Windows.Forms;

namespace Pathfinder {
    public class OOPLabel : Label {

        public readonly byte WeightValue = 5;
        public readonly byte UnweightValue = 1;
        public readonly int INF = int.MaxValue;

        public byte weight;
        public int distance;
    }
}

