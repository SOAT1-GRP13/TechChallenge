

using Application.Catalogo.Boundaries;
using Application.Catalogo.Dto;
using AutoMapper;
using Domain.Catalogo;

namespace Application.Catalogo.Queries
{
    public class ProdutosQueries : IProdutosQueries
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutosQueries(IProdutoRepository produtoRepository,
                                IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo)
        {
            return _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterPorCategoria(codigo));
        }

        public async Task<ProdutoDto> ObterPorId(Guid id)
        {
            return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id));
        }

        public async Task<IEnumerable<ProdutoDto>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterTodos());
        }

        public async Task<IEnumerable<CategoriaDto>> ObterCategorias()
        {
            return _mapper.Map<IEnumerable<CategoriaDto>>(await _produtoRepository.ObterCategorias());
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
