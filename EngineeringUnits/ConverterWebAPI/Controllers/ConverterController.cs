using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnitConverter;

namespace ConverterWebAPI.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class ConverterController : ControllerBase {
        private readonly Converter converter;

        public ConverterController() {
            this.converter = new Converter();
        }

        public class ConversionResult {
            public double data { get; set; }
            public string unit { get; set; }
            public string annotation { get; set; }
        }

        public class ConerterInput {
            [Required] public double data { get; set; }
            [Required] public string from { get; set; }
            [Required] public string to { get; set; }
        }

        [HttpPost]
        public ConversionResult Convert(ConerterInput input) {
            Unit endUnit;
            var res = converter.Convert(input.data, input.from, input.to, out endUnit);
            return new ConversionResult { data = res.Item1, unit = endUnit.Name , annotation = res.Item2};
        }
    }
}
