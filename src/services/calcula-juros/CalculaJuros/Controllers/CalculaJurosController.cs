using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CalculaJuros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculaJurosController : ControllerBase
    {
        private readonly IHttpClientFactory _client;
        private readonly DockerGatewayIpAddressConfiguration _dockerGatewayIpAddressConfiguration;

        public CalculaJurosController(IHttpClientFactory client, IOptions<DockerGatewayIpAddressConfiguration> options)
        {
            _client = client;
            _dockerGatewayIpAddressConfiguration = options.Value;
        }

        [HttpGet("/calculajuros/{valorInicial}/{meses}")]
        public async Task<IActionResult> CalcularJuros(decimal valorInicial, int meses)
        {
            if (valorInicial == 0 || meses == 0) return BadRequest("Deve preencher os dados para poder continuar");

            var client = _client.CreateClient();
            // Docker Gateway IP Address
            client.BaseAddress = new Uri(_dockerGatewayIpAddressConfiguration.IpAndPortOfFilterApp);
            var response = await client.GetAsync("api/taxajuros");
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(responseBody);

            var calculationResult = valorInicial * (1 + result);
            var valorFinal = Math.Pow(Convert.ToDouble(calculationResult), meses);
            return Ok(valorFinal);
        }
    }
}
