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

        public class Input {
            [Required] public string dimension { get; set; }
        }

        public class Jsonn { 
            public string desciptions { get; set; }
            public string dimension { get; set; }
        }

        [HttpGet]
        public string Starte()
        {
            var File = converter.GetQuantityTypes();
            var dict = new Dictionary<string, List<string>>();
            for (int i = 0; i < File.Count; i++){
                var unit = converter.GetUnitsInQuantity(File[i].Name);
                for (int j = 0; j < unit.Count; j++)
                {
                    if (!dict.ContainsKey(File[i].Name))
                        dict.Add(File[i].Name, new List<string>());
                    dict[File[i].Name].Add(unit[j].symbol);
                }
            }
            return JsonSerializer.Serialize(dict);
        }


        [HttpPost]
        public string Post(Input input) {
            var res = converter.GetUnitsInQuantity(input.dimension);
            List<string> list = new List<string>();
            for (int i = 0; i < res.Count; i++){
                list.Add(res[i].symbol);
            }
            return JsonSerializer.Serialize(list);
        }
    }
}
