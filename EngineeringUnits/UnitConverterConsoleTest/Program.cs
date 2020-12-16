using System;
using UnitConverter;

namespace UnitConverterConsoleTest {
    class Program {
        static void Main(string[] args) {

            //var units = converter.GetUnitsInDimension("Length");
            /*
             var units = converter.GetUnitsInQuantity("temperature per time");
             foreach (var unit in units) {
                 Console.WriteLine(unit.Name);
             }
            */
            Console.ReadKey();
            Converter converter = new Converter();
            try {
                Unit endUnit;
                var result = converter.Convert(983, "cm", "m", out endUnit);
                Console.WriteLine("GOT: " + result.Item1 + " " + result.Item2 + " " + endUnit.Name);
                result = converter.Convert(983, "degC", "K");
                Console.WriteLine("GOT: " + result.Item1 + " " + result.Item2);
                result = converter.Convert(983, "kg", "t");
                Console.WriteLine("GOT: " + result.Item1 + " " + result.Item2);
                result = converter.Convert(983, "cm", "m");
                Console.WriteLine("GOT: " + result.Item1 + " " + result.Item2);

            }
            catch (Exception e) {
                Console.WriteLine("ERROR: " + e.Message);
            }

            Console.ReadKey();
        }
    }
}
