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
    [Route("[controller]")]
    public class QuantityTypeController : ControllerBase {
        private readonly Converter converter;

        public QuantityTypeController() {
            this.converter = new Converter();
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
            var File = converter.GetQuantityTypes();

            for (int i = 0; i < File.Count; i++)
            {
                // for (int j = 0; j < File[i].Name.Length; j++)
                // {
                    // list.Add(File[i].descriptors[j]);
                    System.Console.WriteLine(File[i].Name);
                // }
            }
            return JsonSerializer.Serialize(list);
        }

        [HttpPost]
        public ConversionResult Convert(ConerterInput input) {
            var res = converter.Convert(input.data, input.from, input.to);
            return new ConversionResult { data = res.Item1, unit = res.Item2 };
        }
    }
}
