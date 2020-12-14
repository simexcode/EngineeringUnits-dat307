using System;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter {
    class Converter {

        DataReader reader;

        public void Read() {
            reader = new DataReader();
            reader.load();
            reader.GetUnits();
        }

        public double Convert(double d, Unit t, Unit f) {
            return 1;
        }

        public List<Dimension> GetDimensionClasses() { throw new NotImplementedException(); }

        public List<QuantityType> GetQuantityTypes() { throw new NotImplementedException(); }

        public List<Unit> GetUnitsInDimension(Dimension dimension) { throw new NotImplementedException(); }

        public List<Unit> GetUnitsInQuantity(QuantityType quantityType) { throw new NotImplementedException(); }
    }
}
