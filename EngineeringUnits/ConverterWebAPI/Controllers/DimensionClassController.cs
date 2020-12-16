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

        public class Input {
            [Required] public string dimension { get; set; }
        }

        [HttpGet]
        public string Start() {
            List<string> list = new List<string>();
            var File = converter.GetDimensionClasses();
            for (int i = 0; i < File.Count; i++){
                list.Add(File[i].descriptors[0]+" : "+File[i].dimension);
            }
            return JsonSerializer.Serialize(list);
        }

        [HttpPost]
        public string Post(Input input) {
            var res = converter.GetUnitsInDimension(input.dimension);
            List<string> list = new List<string>();
            for (int i = 0; i < res.Count; i++){
                list.Add(res[i].Name);
            }
            return JsonSerializer.Serialize(list);
        }
    }
}
