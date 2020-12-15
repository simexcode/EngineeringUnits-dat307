using System;

namespace UnitConverter {
    class Program {
        static void Main(string[] args) {
            Converter converter = new Converter();
            converter.Read();

            var units = converter.GetUnitsInDimension("Length");
            foreach (var unit in units) {
                Console.WriteLine(unit.Name);
            }

            try {
                var result = converter.Convert(983, "degC", "degF");
                Console.WriteLine("GOT: " + result);
            }
            catch (Exception e) {
                Console.WriteLine("ERROR: " + e.Message);
            }           
        }
    }
}
