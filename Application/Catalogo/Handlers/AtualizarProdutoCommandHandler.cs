using Application.Catalogo.Boundaries;
using Application.Catalogo.Commands;
using Application.Catalogo.Dto;
using AutoMapper;
using Domain.Base.Communication.Mediator;
using Domain.Base.DomainObjects;
using Domain.Base.Messages.CommonMessages.Notifications;
using Domain.Catalogo;
using MediatR;


namespace Application.Catalogo.Handlers
{

    public class AtualizarProdutoCommandHandler : IRequestHandler<AtualizarProdutoCommand, ProdutoOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public AtualizarProdutoCommandHandler(
            IMediatorHandler mediatorHandler,
            IProdutoRepository produtoRepository,
            IMapper mapper
        )
        {
            _mediatorHandler = mediatorHandler;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<ProdutoOutput> Handle(AtualizarProdutoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                foreach (var error in message.ValidationResult.Errors)
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            try
            {
                var produto = _mapper.Map<Produto>(message.Input);
                await _produtoRepository.Atualizar(produto);
                await _produtoRepository.UnitOfWork.Commit();

                var produtoAtualizado = await _produtoRepository.ObterPorId(produto.Id);

                var produtoOutput = _mapper.Map<ProdutoOutput>(produtoAtualizado);
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
