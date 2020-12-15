using System;
using System.Linq;
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
                units.Add(CreateUnitFromXMl(nodes.Item(i).OuterXml));
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

                //Console.WriteLine(quantity + ", " + dimention);
                dimensions.Add(new Dimension(quantity, dimention)); ;
            }

            return dimensions;
        }

        public List<QuantityType> GetQuantityTypes() {
            List<QuantityType> qTypes = new List<QuantityType>();

            var nodes = xmlDocument.GetElementsByTagName("UnitOfMeasure");

            foreach (XmlNode node in nodes) {
                var subNodes = node.ChildNodes;

                foreach (XmlNode subNode in subNodes) {
                    if (subNode.Name == "QuantityType") {
                        if (qTypes.FirstOrDefault(n => n.Name == subNode.InnerText) == null) {
                            qTypes.Add(new QuantityType(subNode.InnerText));
                        }
                    }
                }
            }

            return qTypes;
        }

        public List<Unit> GetUnitsInDimmention(string dimension) {
            var classes = GetDimensions();
            var dim = classes.FirstOrDefault(d => d.Name.ToLower() == dimension.ToLower());
            return GetUnitsInDimmention(dim);
        }

        public List<Unit> GetUnitsInDimmention(Dimension dimension) {
            List<Unit> units = new List<Unit>();
            var nodes = xmlDocument.GetElementsByTagName("UnitOfMeasure");

            foreach (XmlNode node in nodes) {
                if (node.ChildNodes.Cast<XmlNode>().FirstOrDefault(n => n.Name == "DimensionalClass" && n.InnerText == dimension.dimension) != null){
                    units.Add(CreateUnitFromXMl(node.OuterXml));
                }
            }

            return units;
        }

        private Unit CreateUnitFromXMl(string xml) {
            XmlDocument xmlUnit = new XmlDocument();
            xmlUnit.LoadXml(xml);
            //var nodes = xmlDocument.GetElementsByTagName("UnitOfMeasure");
            var element = xmlUnit.GetElementsByTagName("UnitOfMeasure").Item(0);

            var item = element.ChildNodes;
            string Name = "";
            string UnitName = element.Attributes.GetNamedItem("id").Value;
            string baseUnit = "";
            string Symbol = element.Attributes.GetNamedItem("annotation").Value;
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
                    else if(node.ChildNodes.Item(0).Name == "Fraction") {
                        var num = double.Parse(node.ChildNodes.Item(0).ChildNodes.Item(0).InnerText);
                        var den = double.Parse(node.ChildNodes.Item(0).ChildNodes.Item(1).InnerText);
                        ToBase = (value) => { return value * (num / den); };
                        FromBase = (value) => { return value / (num / den); };
                    }
                    else if(node.ChildNodes.Item(0).Name == "Formula"){
                        var A = double.Parse(node.ChildNodes.Item(0).ChildNodes.Item(0).InnerText);
                        var B = double.Parse(node.ChildNodes.Item(0).ChildNodes.Item(1).InnerText);
                        var C = double.Parse(node.ChildNodes.Item(0).ChildNodes.Item(2).InnerText);
                        var D = double.Parse(node.ChildNodes.Item(0).ChildNodes.Item(3).InnerText);
                        ToBase = (value) => {return (A + B * value)/(C + D* value); };
                        FromBase = (value) => {return (A- C * value)/(D * value - B);};    
                    }
                }
            }

            if (baseUnit == "")
                baseUnit = UnitName;

            return new Unit(Name, UnitName, Symbol, baseUnit, ToBase, FromBase);
        }
    }
}
