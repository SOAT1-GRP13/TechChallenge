using Application.Catalogo.Boundaries;
using Application.Catalogo.Commands;
using AutoMapper;
using Domain.Base.Communication.Mediator;
using Domain.Base.DomainObjects;
using Domain.Base.Messages.CommonMessages.Notifications;
using Domain.Catalogo;
using MediatR;

namespace Application.Catalogo.Handlers
{

    public class AdicionarProdutoCommandHandler : IRequestHandler<AdicionarProdutoCommand, ProdutoOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public AdicionarProdutoCommandHandler(
            IMediatorHandler mediatorHandler,
            IProdutoRepository produtoRepository,
            IMapper mapper
        )
        {
            _mediatorHandler = mediatorHandler;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<ProdutoOutput> Handle(AdicionarProdutoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                foreach (var error in message.ValidationResult.Errors)
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            try
            {
                var produto = new Produto(message.Input.Nome, message.Input.Descricao, message.Input.Ativo, message.Input.Valor, message.Input.CategoriaId, DateTime.UtcNow, message.Input.Imagem);

                await _produtoRepository.Adicionar(produto);

                var produtoAdicionado = await _produtoRepository.ObterPorId(produto.Id);

                var produtoOutput = _mapper.Map<ProdutoOutput>(produtoAdicionado);
                return produtoOutput;

            }
            catch (DomainException ex)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, ex.Message));
            }

            return new ProdutoOutput();
        }
    }
}