using Application.Autenticacao.Boundaries.Cliente;
using Application.Autenticacao.Commands;
using Application.Autenticacao.Dto.Cliente;
using Application.Autenticacao.UseCases;
using Domain.Base.Communication.Mediator;
using Domain.Base.DomainObjects;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;

namespace Application.Autenticacao.Handlers
{
    public class AutenticaClienteCommandHandler :
        IRequestHandler<AutenticaClienteCommand, AutenticaClienteOutput>
    {
        private readonly IAutenticacaoUseCase _autenticacaoUseCase;
        private readonly IMediatorHandler _mediatorHandler;
        public AutenticaClienteCommandHandler(IAutenticacaoUseCase autenticacaoUseCase, IMediatorHandler mediatorHandler)
        {
            _autenticacaoUseCase = autenticacaoUseCase;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<AutenticaClienteOutput> Handle(AutenticaClienteCommand request, CancellationToken cancellationToken)
        {
            if (request.EhValido())
            {
                try
                {
                    AutenticaClienteInput input = request.Input;
                    var identificaDto = new IdentificaDto(input.CPF, _autenticacaoUseCase.EncryptPassword(input.Senha));
                    var autenticado = await _autenticacaoUseCase.AutenticaCliente(identificaDto);

                    if (string.IsNullOrEmpty(autenticado.Nome))
                    {
                        await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "CPF ou senha inválidos"));
                    }
                    else
                    {
                        return autenticado;
                    }
                }
                catch (DomainException ex)
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, ex.Message));
                }
            }
            else
            {
                foreach (var error in request.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, error.ErrorMessage));
                }
            }

            return new AutenticaClienteOutput();
        }
    }
}
