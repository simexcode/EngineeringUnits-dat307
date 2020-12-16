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
        public string Start() {
            List<Jsonn> list = new List<Jsonn>();
            var File = converter.GetQuantityTypes();
            for (int i = 0; i < File.Count; i++){
                var unit = converter.GetUnitsInQuantity(File[i].Name);

                 for (int j = 0; j < unit.Count; j++){
                    var jsonn = new Jsonn
                    {
                        desciptions = File[i].Name,
                        dimension = unit[j].symbol
                    };
                    list.Add(jsonn);
                 }

                

                // list.Add(File[i].descriptors[0] + " : " + File[i].dimension);
            }
            return JsonSerializer.Serialize(list);
        }

        // public string Start() {
            // List<string> list = new List<string>();
            // var File = converter.GetQuantityTypes();
            // for (int i = 0; i < File.Count; i++){
                // List<string> list2 = new List<string>();
                // var unit = converter.GetUnitsInQuantity(File[i].Name);
                //  for (int j = 0; j < unit.Count; j++){
                //      list.Add(File[i].Name+" : "+unit[j].symbol);
                //  }
            // }
            // return JsonSerializer.Serialize(list);
        // }

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
