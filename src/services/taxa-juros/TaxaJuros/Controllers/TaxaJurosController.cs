using Microsoft.AspNetCore.Mvc;

namespace TaxaJuros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxaJurosController : ControllerBase
    {
        [HttpGet]
        public OkObjectResult ObterTaxa()
        {
            return Ok("0,01");
        }
    }
}
