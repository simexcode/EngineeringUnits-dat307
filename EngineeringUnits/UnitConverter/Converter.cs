using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter {
    class Converter {

        DataReader reader;
        List<Unit> units = new List<Unit>();

        public void Read() {
            reader = new DataReader();
            reader.load();
            units = reader.GetUnits();
        }

        public double Convert(double d, String f, String t) {
            Unit from = units.First(n => { return n.UnitName == f || n.Name.ToLower() == f.ToLower() || n.symbol == f; });
            Unit to = units.First(n => { return n.UnitName == t || n.Name.ToLower() == t.ToLower() || n.symbol == t; });

            foreach (var unit in units) {
                if (unit.UnitName == t)
                    to = unit;

                if (unit.UnitName == f)
                    from = unit;
            }

            return Convert(d, from, to);
        }

        public double Convert(double d, Unit f, Unit t) {
            var temp = f.ToBase(d);
            return t.FromBase(temp);
        }

        public List<Dimension> GetDimensionClasses() { throw new NotImplementedException(); }

        public List<QuantityType> GetQuantityTypes() { throw new NotImplementedException(); }

        public List<Unit> GetUnitsInDimension(Dimension dimension) { throw new NotImplementedException(); }

        public List<Unit> GetUnitsInQuantity(QuantityType quantityType) { throw new NotImplementedException(); }
    }
}
