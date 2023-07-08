using Application.Catalogo.Boundaries;
using Application.Catalogo.Dto;
using Application.Catalogo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.Admin
{
    [ApiController]
    [Route("Catalogo")]
    public class ProdutosController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;

        public ProdutosController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet("lista_produtos")]
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = "Listar produtos",
            Description = "Lista os produtos cadastrados, não é necessario se autenticar")]
        [SwaggerResponse(200, "Retorna produtos cadastrados", typeof(IEnumerable<ProdutoDto>))]
        [SwaggerResponse(404, "Caso não tenha nenhum produto cadastrado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var produtos = await _produtoAppService.ObterTodos();
                if (produtos == null) return NotFound("Nenhum produto encontrado.");

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                       $"Erro ao tentar recuperar produtos. Erro: {ex.Message}");
            }

        }

        [HttpGet("lista_categorias")]
        [AllowAnonymous]
        [SwaggerOperation(
    Summary = "Listar categorias",
    Description = "Lista as categorias cadastradas, não é necessario se autenticar")]
        [SwaggerResponse(200, "Retorna categorias cadastrados", typeof(IEnumerable<ProdutoDto>))]
        [SwaggerResponse(404, "Caso não tenha nenhum categoria cadastrado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> GetGategories()
        {
            try
            {
                var categorias = await _produtoAppService.ObterCategorias();
                if (categorias == null) return NotFound("Nenhuma categoria encontrada.");

                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                       $"Erro ao tentar recuperar categorias. Erro: {ex.Message}");
            }

        }

        [HttpGet("busca_produto/{id}")]
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = "Busca produto por id",
            Description = "Retorna produto por id, não é necessario se autenticar")]
        [SwaggerResponse(200, "Retorna produto", typeof(ProdutoDto))]
        [SwaggerResponse(404, "Caso não tenha produto com o id informado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Get([FromRoute, SwaggerRequestBody("uuid do produto", Required = true)] Guid id)
        {
            try
            {
                var produto = await _produtoAppService.ObterPorId(id);
                if (produto == null) return NotFound("Produto não encontrado.");

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                       $"Erro ao tentar recuperar produto. Erro: {ex.Message}");
            }

        }

        [HttpGet("busca_produto_por_categoria/{codigo}")]
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = "Listar produtos por categoria",
            Description = "Lista os produtos cadastrados filtrados por categoria, não é necessario se autenticar")]
        [SwaggerResponse(200, "Retorna produtos ", typeof(IEnumerable<ProdutoDto>))]
        [SwaggerResponse(404, "Caso não tenha nenhum produto cadastrado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Get([FromRoute, SwaggerRequestBody("codigo do produto", Required = true)]  int codigo)
        {
            try
            {
                var produto = await _produtoAppService.ObterPorCategoria(codigo);
                if (produto == null) return NotFound("Produto não encontrado.");

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                       $"Erro ao tentar recuperar produto. Erro: {ex.Message}");
            }

        }


        [HttpPost("cadastro_produto")]
        [Authorize(Roles = "Gestor")]
        [SwaggerOperation(
            Summary = "Cadastrar produto",
            Description = "Realiza o cadastro do produto, é necessario estar autenticado como gestor")]
        [SwaggerResponse(200, "Retorna produto ", typeof(ProdutoOutput))]
        [SwaggerResponse(400, "Caso de erro ao adicionar produto")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Post([FromBody] ProdutoInput produtoInput)
        {
            try
            {
                var produto = await _produtoAppService.AdicionarProduto(produtoInput);
                if (produto == null) return BadRequest("Erro ao tentar adicionar produto.");

                return Ok(produto);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                                          $"Erro ao tentar adicionar produto. Erro: {ex.Message}");
            }
        }

        [HttpPut("editar_produto")]
        [Authorize(Roles = "Gestor")]
        [SwaggerOperation(
            Summary = "Editar produto",
            Description = "Realiza o cadastro do produto, é necessario estar autenticado como gestor")]
        [SwaggerResponse(200, "Retorna produto ", typeof(ProdutoOutput))]
        [SwaggerResponse(404, "Caso não encontre o produto com o Id informado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Put([FromBody]ProdutoEditarInput produtoEditarInput)
        {
            try
            {
                var produto = await _produtoAppService.ObterPorId(produtoEditarInput.Id);
                if (produto == null) return NotFound("Produto não encontrado.");

                var produtoAtualizado = await _produtoAppService.AtualizarProduto(produtoEditarInput);

                return Ok(produtoAtualizado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                                          $"Erro ao tentar atualizar produto. Erro: {ex.Message}");
            }
        }

        [HttpDelete("excluir_produto/{id}")]
        [Authorize(Roles = "Gestor")]
        [SwaggerOperation(
            Summary = "Deletar produto",
            Description = "Realiza o cadastro do produto, é necessario estar autenticado como gestor")]
        [SwaggerResponse(200, "Em caso de remoção com sucesso")]
        [SwaggerResponse(404, "Caso não encontre o produto com o Id informado")]
        [SwaggerResponse(500, "Caso algo inesperado aconteça")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            try
            {
                var produto = await _produtoAppService.ObterPorId(id);
                if (produto == null) return NotFound("Produto não encontrado.");

                await _produtoAppService.RemoverProduto(id);

                return Ok("Produto excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                                          $"Erro ao tentar excluir produto. Erro: {ex.Message}");
            }
        }
    }
}
