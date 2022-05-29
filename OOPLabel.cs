
using System.Windows.Forms;

namespace Pathfinder {
    public class OOPLabel : Label {

        public readonly byte WeightValue = 5;
        public readonly byte UnweightValue = 1;

        public byte Weight { get; set; }
    }
}

