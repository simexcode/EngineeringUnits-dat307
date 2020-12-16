using Microsoft.AspNetCore.Mvc;
using UnitConverter;
using System.ComponentModel.DataAnnotations;

namespace ConverterWebAPI.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}