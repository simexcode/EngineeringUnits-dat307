using System;

namespace UnitConverter {
    class Program {
        static void Main(string[] args) {
            Converter converter = new Converter();
            converter.Read();
            try {
                var result = converter.Convert(100, "cm", "km");
                Console.WriteLine("GOT: " + result);
            }
            catch (Exception e) {
                Console.WriteLine("ERROR: " + e.Message);
            }           
        }
    }
}
