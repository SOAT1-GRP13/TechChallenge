using Application.Autenticacao.Boundaries.Cliente;
using Application.Autenticacao.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [SwaggerTag("Endpoints relacionados ao cliente, não é necessário se autenticar")]
    public class ClienteController : Controller
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public ClienteController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [SwaggerOperation(
            Summary = "Identificação do cliente",
            Description = "Endpoint responsavel por autenticar o cliente")]
        [SwaggerResponse(200, "Retorna dados se autenticado ou não", typeof(IdentificaOutput))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        [HttpPost]
        [Route("AutenticaCliente")]
        public async Task<IActionResult> AutenticaCliente([FromBody] IdentificaInput input)
        {
            var acesso = await _autenticacaoService.AutenticaCliente(input);

            return Ok(acesso);
        }

        [SwaggerOperation(
    Summary = "Cadastro do cliente",
    Description = "Endpoint responsavel por cadastrar o cliente")]
        [SwaggerResponse(200, "Cadastra o usuario e ja o autentica", typeof(IdentificaOutput))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        [HttpPost]
        [Route("CadastraCliente")]
        public async Task<IActionResult> CadastraCliente([FromBody] CadastraClienteInput input)
        {
            var acesso = await _autenticacaoService.CadastraCliente(input);

            return Ok(acesso);
        }


    }
}
