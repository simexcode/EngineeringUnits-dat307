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

        public class ConverterInput {
            [Required] public string quantity { get; set; }
        }

        [HttpGet]
        public string Start() {
            List<string> list = new List<string>();
            var File = converter.GetQuantityTypes();

            for (int i = 0; i < File.Count; i++){
                List<string> list2 = new List<string>();
                var unit = converter.GetUnitsInQuantity(File[i].Name);
                

                // list.Add(File[i].Name+" : "+JsonSerializer.Serialize(unit[i].symbol) );
                // list.Add(File[i].Name+" : "+unit[j].symbol);

                 for (int j = 0; j < unit.Count; j++){
                     list.Add(File[i].Name+" : "+unit[j].symbol);
                 }
                // JsonSerializer.Serialize(unit);

            }
            return JsonSerializer.Serialize(list);
        }

        [HttpPost]
        public string Post(ConverterInput input) {
            var res = converter.GetUnitsInQuantity(input.quantity);
            List<string> list = new List<string>();
            for (int i = 0; i < res.Count; i++){
                list.Add(res[i].symbol);
            }
            return JsonSerializer.Serialize(list);
        }
    }
}
