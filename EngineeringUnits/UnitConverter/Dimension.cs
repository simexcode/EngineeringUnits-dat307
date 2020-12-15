using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter {
    class Dimension : IHashable {
        public string Name;
        int HashName;
        string IHashable.Name { get => Name; }
        int IHashable.HashName { get => HashName; }

        //public static implicit operator Dimension(string d) => d;
        //public static implicit operator string(Dimension d) => d.Name;
    }
}
