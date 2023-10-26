using MediatR;
using Application.Catalogo.Dto;
using Microsoft.AspNetCore.Mvc;
using Application.Catalogo.Queries;
using Application.Catalogo.Commands;
using Application.Catalogo.Boundaries;
using Domain.Base.Communication.Mediator;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Domain.Base.Messages.CommonMessages.Notifications;

namespace API.Controllers.Admin
{
    [ApiController]
    [Route("Catalogo")]
    [SwaggerTag("Endpoints relacionados a produtos, Alguns são necessários estar autenticado para acessar, outros não precisa e alguns deles é necessário estar autenticado como gestor. O detalhamento do nível de autenticacão estará descrito em cada endpoint.")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosQueries _produtosQueries;
        private readonly IMediatorHandler _mediatorHandler;
        public ProdutosController(IProdutosQueries produtosQueries,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _produtosQueries = produtosQueries;
            _mediatorHandler = mediatorHandler;
        }


        [HttpGet("lista_produtos")]
        [SwaggerOperation(
            Summary = "Listar produtos",
            Description = "Lista os produtos cadastrados. Não precisa estar autenticado")]
        [SwaggerResponse(200, "Retorna produtos cadastrados", typeof(IEnumerable<ProdutoDto>))]
        [SwaggerResponse(404, "Caso não tenha nenhum produto cadastrado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var produtos = await _produtosQueries.ObterTodos();
                if (produtos == null) return NotFound("Nenhum produto encontrado.");

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                       $"Erro ao tentar recuperar produtos. Erro: {ex.Message}");
            }

        }

        [HttpGet("lista_categorias")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Listar categorias",
            Description = "Lista as categorias cadastradas. Precisa se autenticar")]
        [SwaggerResponse(200, "Retorna categorias cadastrados", typeof(IEnumerable<ProdutoDto>))]
        [SwaggerResponse(404, "Caso não tenha nenhum categoria cadastrado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> GetGategories()
        {
            try
            {
                var categorias = await _produtosQueries.ObterCategorias();
                if (categorias == null) return NotFound("Nenhuma categoria encontrada.");

                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                       $"Erro ao tentar recuperar categorias. Erro: {ex.Message}");
            }

        }

        [HttpGet("busca_produto/{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Busca produto por id",
            Description = "Retorna produto por id. Precisa se autenticar")]
        [SwaggerResponse(200, "Retorna produto", typeof(ProdutoDto))]
        [SwaggerResponse(404, "Caso não tenha produto com o id informado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Get([FromRoute, SwaggerRequestBody("uuid do produto", Required = true)] Guid id)
        {
            try
            {
                var produto = await _produtosQueries.ObterPorId(id);
                if (produto == null) return NotFound("Produto não encontrado.");

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                       $"Erro ao tentar recuperar produto. Erro: {ex.Message}");
            }

        }

        [HttpGet("busca_produto_por_categoria/{codigo}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Listar produtos por categoria",
            Description = "Lista os produtos cadastrados filtrados por categoria. Precisa se autenticar")]
        [SwaggerResponse(200, "Retorna produtos ", typeof(IEnumerable<ProdutoDto>))]
        [SwaggerResponse(404, "Caso não tenha nenhum produto cadastrado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Get([FromRoute, SwaggerRequestBody("codigo do produto", Required = true)] int codigo)
        {
            try
            {
                var produto = await _produtosQueries.ObterPorCategoria(codigo);
                if (produto == null) return NotFound("Produto não encontrado.");

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                       $"Erro ao tentar recuperar produto. Erro: {ex.Message}");
            }

        }


        [HttpPost("cadastro_produto")]
        [Authorize(Roles = "Gestor")]
        [SwaggerOperation(
            Summary = "Cadastrar produto",
            Description = "Realiza o cadastro do produto, sendo necessário estar autenticado como gestor")]
        [SwaggerResponse(200, "Retorna produto ", typeof(ProdutoOutput))]
        [SwaggerResponse(400, "Caso de erro ao adicionar produto")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Post([FromBody] ProdutoInput produtoInput)
        {

            try
            {

                var command = new AdicionarProdutoCommand(produtoInput);
                var produto = await _mediatorHandler.EnviarComando<AdicionarProdutoCommand, ProdutoOutput>(command);

                if (OperacaoValida())
                {
                    return Ok(produto);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ObterMensagensErro());
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                                                          $"Erro ao tentar adicionar produto. Erro: {ex.Message}");
            }
        }

        [HttpPut("editar_produto")]
        [Authorize(Roles = "Gestor")]
        [SwaggerOperation(
            Summary = "Editar produto",
            Description = "Realiza o cadastro do produto, sendo necessário estar autenticado como gestor")]
        [SwaggerResponse(200, "Retorna produto ", typeof(ProdutoOutput))]
        [SwaggerResponse(404, "Caso não encontre o produto com o Id informado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Put([FromBody] ProdutoEditarInput produtoEditarInput)
        {
            try
            {
                var produto = await _produtosQueries.ObterPorId(produtoEditarInput.Id);
                if (produto == null) return NotFound("Produto não encontrado.");

                var command = new AtualizarProdutoCommand(produtoEditarInput);
                var produtoAtualizado = await _mediatorHandler.EnviarComando<AtualizarProdutoCommand, ProdutoOutput>(command);

                return Ok(produtoAtualizado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                                          $"Erro ao tentar atualizar produto. Erro: {ex.Message}");
            }
        }

        [HttpDelete("excluir_produto/{id}")]
        [Authorize(Roles = "Gestor")]
        [SwaggerOperation(
            Summary = "Deletar produto",
            Description = "Realiza o cadastro do produto, sendo necessário estar autenticado como gestor")]
        [SwaggerResponse(200, "Em caso de remoção com sucesso")]
        [SwaggerResponse(404, "Caso não encontre o produto com o Id informado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                var produto = await _produtosQueries.ObterPorId(id);
                if (produto == null) return NotFound("Produto não encontrado.");

                var command = new RemoverProdutoCommand(id);
                var produtoRemovido = await _mediatorHandler.EnviarComando<RemoverProdutoCommand, bool>(command);

                return Ok("Produto excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                                          $"Erro ao tentar excluir produto. Erro: {ex.Message}");
            }
        }
    }
}
