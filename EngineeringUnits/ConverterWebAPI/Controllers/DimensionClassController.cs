using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnitConverter;
using System.Text.Json;

namespace ConverterWebAPI.Controllers {

    [ApiController]
    [Route("DimensionClass")]
    public class DimensionClassController : ControllerBase {
        private readonly Converter converter;

        public DimensionClassController() {
            this.converter = new Converter();
        }

        public class DimensionsOutput {
            public List<Dimension> dimensions;
        }

        public class ConverterInput {
            [Required] public string from { get; set; }
        }

        public class ConversionResult {
            public double data { get; set; }
            public string unit { get; set; }
        }

        public class ConerterInput {
            [Required] public double data { get; set; }
            [Required] public string from { get; set; }
            [Required] public string to { get; set; }
        }

        [HttpGet]
        public string Start() {
            List<string> list = new List<string>();
            var File = converter.GetDimensionClasses();
            for (int i = 0; i < File.Count; i++)
            {
                for (int j = 0; j < File[i].descriptors.Length; j++)
                {
                    list.Add(File[i].descriptors[j]);
                    System.Console.WriteLine(File[i].descriptors[j]);
                }
            }
            return JsonSerializer.Serialize(list);
        }

        [HttpPost]
        public List<Unit> reply(string input) {
            var res = converter.GetUnitsInDimension("length");
            return res;
        }

    }
}
