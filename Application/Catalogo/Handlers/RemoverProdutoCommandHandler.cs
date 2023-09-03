using Application.Catalogo.Boundaries;
using Application.Catalogo.Commands;
using AutoMapper;
using Domain.Base.Communication.Mediator;
using Domain.Base.DomainObjects;
using Domain.Base.Messages.CommonMessages.Notifications;
using Domain.Catalogo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalogo.Handlers
{
    public class RemoverProdutoCommandHandler : IRequestHandler<RemoverProdutoCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public RemoverProdutoCommandHandler(
            IMediatorHandler mediatorHandler,
            IProdutoRepository produtoRepository,
            IMapper mapper
        )
        {
            _mediatorHandler = mediatorHandler;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(RemoverProdutoCommand message, CancellationToken cancellationToken)
        {

            try
            {

                var produto = _mapper.Map<Produto>(await _produtoRepository.ObterPorId(message.idProduto));
                await _produtoRepository.Remover(produto);

                return await _produtoRepository.UnitOfWork.Commit();

            }
            catch (DomainException ex)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, ex.Message));
                return false;
            }


        }
    }
}
