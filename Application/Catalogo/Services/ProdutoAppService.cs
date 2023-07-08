using Application.Catalogo.Boundaries;
using Application.Catalogo.Dto;
using AutoMapper;
using Domain.Catalogo;

namespace Application.Catalogo.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

         public ProdutoAppService(IProdutoRepository produtoRepository,
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

        public async Task<ProdutoOutput> AdicionarProduto(ProdutoInput produtoInput)
        {
            var produto = _mapper.Map<Produto>(produtoInput);
            await _produtoRepository.Adicionar(produto);

            //await _produtoRepository.UnitOfWork.Commit();

            return _mapper.Map<ProdutoOutput>(await _produtoRepository.ObterPorId(produto.Id));
        }

        public async Task<ProdutoDto> AtualizarProduto(ProdutoEditarInput produtoEditarInput)
        {
            var produto = _mapper.Map<Produto>(produtoEditarInput);
            await _produtoRepository.Atualizar(produto);

            await _produtoRepository.UnitOfWork.Commit();

            return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(produto.Id));
        }

        public async Task RemoverProduto(Guid id)
        {
            var produto = _mapper.Map<Produto>(await _produtoRepository.ObterPorId(id));
             await _produtoRepository.Remover(produto);

            //await _produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}