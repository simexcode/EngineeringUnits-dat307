using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter {
    public class Unit {
        public string Name;
        public string UnitName;
        public readonly string baseUnit;
        public readonly string symbol;
        public readonly Func<double, double> ToBase;
        public readonly Func<double, double> FromBase;


        public Unit(string Name, string UnitName, string Symbol, string BaseUnit, Func<double, double> ToBase, Func<double, double> FromBase) {
            this.Name = Name;
            this.UnitName = UnitName;
            this.symbol = Symbol;
            this.ToBase = ToBase;
            this.FromBase = FromBase;
            this.baseUnit = BaseUnit;
        }

    }
}
