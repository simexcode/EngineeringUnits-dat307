using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter {
    public class Converter {

        DataReader reader;
        List<Unit> units = new List<Unit>();

        public Converter() {
            Read();
        }

        public void Read() {
            reader = new DataReader();
            reader.load();
            units = reader.GetUnits();
        }

        public (double, string) Convert(double d, String f, String t) {
            Unit from = units.FirstOrDefault(n => { return n.UnitName == f || n.Name.ToLower() == f.ToLower() || n.symbol == f; });
            Unit to = units.FirstOrDefault(n => { return n.UnitName == t || n.Name.ToLower() == t.ToLower() || n.symbol == t; });

            if (from == null)
                throw new KeyNotFoundException("No unit of type '" + f + "' was found");

            if (to == null)
                throw new KeyNotFoundException("No unit of type '" + t + "' was found");

            if (to.baseUnit != from.baseUnit)
                throw new InvalidOperationException("Cannot convert from type '" + from.Name + "' to type '" + to.Name + "', no common base unit found");

            return Convert(d, from, to);
        }

        private (double, string) Convert(double d, Unit f, Unit t) {
            var temp = f.ToBase(d);
            return (t.FromBase(temp), t.symbol);
        }

        public List<Dimension> GetDimensionClasses() {
            return reader.GetDimensions();
        }

        public List<QuantityType> GetQuantityTypes() { throw new NotImplementedException(); }

        public List<Unit> GetUnitsInDimension(string dimension) {
            var classes = reader.GetDimensions();
            var dim = classes.FirstOrDefault(d => d.descriptors.Contains(dimension.ToLower()));
            return GetUnitsInDimension(dim);
        }

        public List<Unit> GetUnitsInDimension(Dimension dimension) {return reader.GetUnitsInDimmention(dimension); }

        public List<Unit> GetUnitsInQuantity(string quantity) {
            var classes = reader.GetQuantityTypes();
            var dim = classes.FirstOrDefault(d => d.Name.ToLower() == quantity.ToLower());
            return GetUnitsInQuantity(dim);
        }

        public List<Unit> GetUnitsInQuantity(QuantityType quantityType) { return reader.GetUnitsInQuantity(quantityType); }
    }
}
