using Application.Catalogo.Boundaries;
using Application.Catalogo.Dto;

namespace Application.Catalogo.Services
{
    public interface IProdutoAppService : IDisposable
    {
        Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo);
        Task<ProdutoDto> ObterPorId(Guid id);
        Task<IEnumerable<ProdutoDto>> ObterTodos();
        Task<IEnumerable<CategoriaDto>> ObterCategorias();

        Task<ProdutoOutput> AdicionarProduto(ProdutoInput produtoInput);
        Task<ProdutoDto> AtualizarProduto(ProdutoEditarInput produtoEditarInput);

        Task RemoverProduto(Guid id);
    }
}