using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace UnitConverter {
    public class Converter {

        DataReader reader;
        //List<Unit> units = new List<Unit>();
        Dictionary<string, List<Unit>> cache = new Dictionary<string, List<Unit>>();
        Queue<string> cacheQueue = new Queue<string>();  
        int cacheLevel = 1;

        public Converter(int cacheLevel = 1) {
            //Read();
            this.cacheLevel = cacheLevel;
            reader = new DataReader();
        }


        private bool UnitInCache(string name, ref List<Unit> hit) {

            foreach (var key in cache.Keys) {
                var candidate = cache[key].FirstOrDefault(n => { return n.UnitName == name || n.Name.ToLower() == name.ToLower() || n.symbol == name; });
                if (candidate != null) {
                    hit = cache[key];
                    return true;
                }
            }

            return false;
        }

        private List<Unit> LoadUnit(string name) {
            List<Unit> hit = new List<Unit>();
            if (UnitInCache(name, ref hit))
                return hit;

            if (cacheQueue.Count == cacheLevel) {
                var last = cacheQueue.Dequeue();
                cache[last].Clear();
            }

            var all = reader.GetUnits();
            var candidate = all.FirstOrDefault(n => { return n.UnitName == name || n.Name.ToLower() == name.ToLower() || n.symbol == name; });

            if (candidate == null)
                throw new KeyNotFoundException("Could not find any unit '" + name + "'");

            var filtered = all.Where(n => n.baseUnit == candidate.baseUnit).ToList();
            cache[candidate.dimention] = filtered;
            cacheQueue.Enqueue(candidate.dimention);
            return filtered;
        }


        public (double, string) Convert(double d, String f, String t, out Unit endUnit) {
            Unit from = LoadUnit(f).FirstOrDefault(n => { return n.UnitName == f || n.Name.ToLower() == f.ToLower() || n.symbol == f; });
            Unit to = LoadUnit(t).FirstOrDefault(n => { return n.UnitName == t || n.Name.ToLower() == t.ToLower() || n.symbol == t; });
            endUnit = to;

            if (from == null)
                throw new KeyNotFoundException("No unit of type '" + f + "' was found");

            if (to == null)
                throw new KeyNotFoundException("No unit of type '" + t + "' was found");

            if (to.baseUnit != from.baseUnit)
                throw new InvalidOperationException("Cannot convert from type '" + from.Name + "' to type '" + to.Name + "', no common base unit found");

            return Convert(d, from, to);
        }

        public (double, string) Convert(double d, String f, String t) {
            Unit temp;
            return Convert(d, f, t, out temp);
        }

        public (double, string) Convert(double d, Unit f, Unit t) {
            var temp = f.ToBase(d);
            return (t.FromBase(temp), t.symbol);
        }

        public List<Dimension> GetDimensionClasses() {
            return reader.GetDimensions();
        }

        public List<QuantityType> GetQuantityTypes() { 
            return reader.GetQuantityTypes(); 
        }

        public List<Unit> GetUnitsInDimension(string dimension) {
            var classes = reader.GetDimensions();
            var dim = classes.FirstOrDefault(d => d.descriptors.Contains(dimension.ToLower()));
            return GetUnitsInDimension(dim);
        }

        public List<Unit> GetUnitsInDimension(Dimension dimension) {
            return reader.GetUnitsInDimmention(dimension); 
        }

        public List<Unit> GetUnitsInQuantity(string quantity) {
            var classes = reader.GetQuantityTypes();
            var dim = classes.FirstOrDefault(d => d.Name.ToLower() == quantity.ToLower());
            return GetUnitsInQuantity(dim);
        }

        public List<Unit> GetUnitsInQuantity(QuantityType quantityType) { 
            return reader.GetUnitsInQuantity(quantityType); 
        }
    }
}
