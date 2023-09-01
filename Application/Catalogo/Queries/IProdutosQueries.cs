using Application.Catalogo.Boundaries;
using Application.Catalogo.Dto;

namespace Application.Catalogo.Queries
{
    public interface IProdutosQueries : IDisposable
    {
        Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo);
        Task<ProdutoDto> ObterPorId(Guid id);
        Task<IEnumerable<ProdutoDto>> ObterTodos();
        Task<IEnumerable<CategoriaDto>> ObterCategorias();

    }
}
