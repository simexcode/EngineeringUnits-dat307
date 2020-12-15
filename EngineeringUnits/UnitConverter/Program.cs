using System;

namespace UnitConverter {
    class Program {
        static void Main(string[] args) {
            Converter converter = new Converter();
            converter.Read();

            //var units = converter.GetUnitsInDimension("Length");
            var units = converter.GetUnitsInQuantity("temperature per time");
            foreach (var unit in units) {
                Console.WriteLine(unit.Name);
            }

            try {
                var result = converter.Convert(983, "cm", "m");
                Console.WriteLine("GOT: " + result.Item1 + " " + result.Item2);
            }
            catch (Exception e) {
                Console.WriteLine("ERROR: " + e.Message);
            }           
        }
    }
}
