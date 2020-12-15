using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter {
    public class Dimension {
        public readonly string[] descriptors;
        public readonly string dimension;

        public Dimension(string[] descriptors, string dimension) {
            this.descriptors = descriptors;
            this.dimension = dimension;
        }
    }
}
