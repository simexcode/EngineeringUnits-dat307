using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter {
    class Dimension {
        public readonly string Name;
        public readonly string dimension;

        public Dimension(string name, string dimension) {
            Name = name;
            this.dimension = dimension;
        }
    }
}
