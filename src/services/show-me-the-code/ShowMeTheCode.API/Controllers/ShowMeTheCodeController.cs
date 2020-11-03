using Microsoft.AspNetCore.Mvc;

namespace ShowMeTheCode.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowMeTheCodeController : ControllerBase
    {
        [HttpGet]
        public OkObjectResult Get()
        {
            return Ok("https://github.com/LucasOscarSimon/CalcularJuros");
        }
    }
}
