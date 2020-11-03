using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CalculaJuros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculaJurosController : ControllerBase
    {
        private readonly IHttpClientFactory _client;
        private readonly DockerGatewayIpAddressConfiguration _dockerGatewayIpAddressConfiguration;

        public CalculaJurosController(IHttpClientFactory client, IOptions<DockerGatewayIpAddressConfiguration> dockerGatewayIpAddressConfiguration)
        {
            _client = client;
            _dockerGatewayIpAddressConfiguration = dockerGatewayIpAddressConfiguration.Value;
        }

        [HttpGet("/calculajuros/{valorInicial}/{meses}")]
        public async Task<IActionResult> CalcularJuros(decimal valorInicial, int meses)
        {
            if (Math.Abs(valorInicial) < 0 || meses == 0) return BadRequest("Deve preencher os dados para poder continuar");

            var taxa = await ObterTaxa();
            var juros = CalcularJuros(valorInicial, meses, taxa);
            return Ok(juros);
        }

        private static string CalcularJuros(decimal valorInicial, int meses, string taxa)
        {
            var provider = new NumberFormatInfo {NumberDecimalSeparator = ",", NumberDecimalDigits = 2};
            var result = Convert.ToDecimal(taxa, provider);
            var valorFinal = Math.Pow(Convert.ToDouble(valorInicial * (1 + result), provider), meses);
            var valor = Math.Truncate(100 * valorFinal / 100000000).ToString("000,#");
            return valor;
        }

        private async Task<string> ObterTaxa()
        {
            var client = _client.CreateClient();
            // Docker Gateway IP Address
            client.BaseAddress = new Uri(_dockerGatewayIpAddressConfiguration.IpAndPortOfFilterApp);
            var response = await client.GetAsync("api/taxajuros");
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
