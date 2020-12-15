using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter {
    class Dimension {
        public string Name;

        public Dimension(string name) {
            Name = name;
        }


        public static implicit operator Dimension(string d) => new Dimension(d);
        public static implicit operator string(Dimension d) => d.Name;
    }
}
