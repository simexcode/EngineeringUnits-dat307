using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace UnitConverter {
    class DataReader {
        XmlDocument xmlDocument = new XmlDocument();

        public void load() {
            String foo = Properties.Resources.units;
            xmlDocument.LoadXml(foo);
        }


        /// <summary>
        /// use This tosdfhjkhgjkddfg
        /// </summary>
        /// <returns>sdfsg</returns>
        public List<Unit> GetUnits() {
            List<Unit> units = new List<Unit>();
            var nodes = xmlDocument.GetElementsByTagName("UnitOfMeasure");
            for (int i = 0; i < nodes.Count; i++) {
                var item = nodes.Item(i).ChildNodes;
                string Name = "";
                string symbol = nodes.Item(i).Attributes.GetNamedItem("annotation").Value;
                Func<double, double> ToBase = (value) => { return value * 1; };
                Func<double, double> FromBase = (value) => { return value * 1; };

                for (int j = 0; j < item.Count; j++) {
                    var node = item.Item(j);

                    if (node.Name == "Name") Name = node.InnerText;


                    if (node.Name == "ConversionToBaseUnit") {
                        if (node.ChildNodes.Item(0).Name == "Factor") {
                            var fac = double.Parse(node.ChildNodes.Item(0).InnerText);
                            ToBase = (value) => { return value * fac; };
                            FromBase = (value) => { return value / fac; };
                        }
                        else {
                            var num = double.Parse(node.ChildNodes.Item(0).ChildNodes.Item(0).InnerText);
                            var den = double.Parse(node.ChildNodes.Item(0).ChildNodes.Item(1).InnerText);
                            ToBase = (value) => { return value * (num / den); };
                            FromBase = (value) => { return value / (num / den); };
                        }
                    }
                }

                Console.WriteLine(Name + " " + symbol);
            }

            return units;
        }


    }
}
