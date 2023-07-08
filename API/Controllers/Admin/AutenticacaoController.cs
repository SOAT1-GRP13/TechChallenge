using Application.Autenticacao.Boundaries.LogIn;
using Application.Autenticacao.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.Admin
{
    [ApiController]
    [Route("[Controller]")]
    public class AutenticacaoController : Controller
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [SwaggerOperation(
            Summary = "Autenticação do Gestor/Atendente",
            Description = "Endpoint responsavel por autenticar o Gestor ou atendente")]
        [SwaggerResponse(200, "Retorna dados se autenticado ou não", typeof(LogInUsuarioOutput))]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        [HttpPost]
        [Route("LogInUsuario")]
        public async Task<IActionResult> LogInUsuario([FromBody] LogInUsuarioInput input)
        {
            var acesso = await _autenticacaoService.AutenticaUsuario(input);

            return Ok(acesso);
        }


    }
}
