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

        public List<Unit> GetUnits() {
            List<Unit> units = new List<Unit>();
            var nodes = xmlDocument.GetElementsByTagName("UnitOfMeasure");
            for (int i = 0; i < nodes.Count; i++) {
                var item = nodes.Item(i).ChildNodes;
                string Name = "";
                string UnitName = nodes.Item(i).Attributes.GetNamedItem("id").Value;
                string baseUnit = "";
                string Symbol = nodes.Item(i).Attributes.GetNamedItem("annotation").Value;
                Func<double, double> ToBase = (value) => { return value * 1; };
                Func<double, double> FromBase = (value) => { return value * 1; };

                for (int j = 0; j < item.Count; j++) {
                    var node = item.Item(j);

                    if (node.Name == "Name") Name = node.InnerText;


                    if (node.Name == "ConversionToBaseUnit") {
                        baseUnit = node.Attributes.GetNamedItem("baseUnit").InnerText;
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

                Unit unit = new Unit(Name, UnitName, Symbol, baseUnit, ToBase, FromBase);
                units.Add(unit);
            }

            return units;
        }

        public List<Dimension> GetDimensions() {
            List<Dimension> dimensions = new List<Dimension>();
            var nodes = xmlDocument.GetElementsByTagName("UnitOfMeasure");

            foreach (XmlNode node in nodes) {
                var subNodes = node.ChildNodes;
                var dimention = ""; var quantity = ""; var isBaseUnit = true;

                foreach (XmlNode subNode in subNodes) {
                    if (subNode.Name == "ConversionToBaseUnit")
                        isBaseUnit = false;

                    if (subNode.Name == "DimensionalClass")
                        dimention = subNode.InnerText;

                    if (subNode.Name == "QuantityType")
                        quantity = subNode.InnerText;
                }

                if (isBaseUnit == false)
                    continue;

                Console.WriteLine(quantity + ", " + dimention);
            }

            return dimensions;
        }

        public void GetDimensionalClass(string QTypeID) {
            XmlDocument Units = new XmlDocument();
            Units.Load("poscUnits22.xml");
            XmlNodeList nodeList = Units.GetElementsByTagName("UnitOfMeasure");

            // Foreach loop gets the dimensionclass of the input unit
            // Need to make a list of the nodes with same base unit.
            foreach (XmlNode node in nodeList) {
                if (node.Attributes[0].Value == QTypeID) {

                    //Iterates through the childnodes in the main node.
                    for (int i = 0; i < node.ChildNodes.Count; i++) {
                        //Finds the node with correct tag
                        if (node.ChildNodes[i].Name == "DimensionalClass") {
                            Console.WriteLine(node.ChildNodes[i].InnerText);
                        }
                    }
                }
            }
        }
    }
}
