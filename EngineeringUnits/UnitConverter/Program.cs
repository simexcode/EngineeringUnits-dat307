using System;

namespace UnitConverter {
    class Program {
        static void Main(string[] args) {
            Converter converter = new Converter();
            converter.Read();
            var result = converter.Convert(100, "cm", "km");
            Console.WriteLine("GOT: " + result);
        }
    }
}
