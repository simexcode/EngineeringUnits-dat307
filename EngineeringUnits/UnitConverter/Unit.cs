using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter {
    class Unit : IHashable {
        string Name;
        int HashName;
        string IHashable.Name { get => Name; }
        int IHashable.HashName { get => HashName; }

        public readonly string symbol;


        public Unit(string Name, string Symbol, Func<double> ToBase) {
            this.Name = Name;
            this.HashName = Name.GetHashCode();
            this.symbol = Symbol;

        }

        public double ToBase() { throw new NotImplementedException(); }
    }
}
