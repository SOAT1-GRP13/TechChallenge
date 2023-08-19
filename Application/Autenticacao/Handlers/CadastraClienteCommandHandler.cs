using Application.Autenticacao.Boundaries.Cliente;
using Application.Autenticacao.Commands;
using Application.Autenticacao.Dto.Cliente;
using Application.Autenticacao.Queries;
using Application.Autenticacao.UseCases;
using Domain.Base.Communication.Mediator;
using Domain.Base.DomainObjects;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;

namespace Application.Autenticacao.Handlers
{
    public class CadastraClienteCommandHandler :
        IRequestHandler<CadastraClienteCommand, AutenticaClienteOutput>
    {
        private readonly IAutenticacaoUseCase _autenticacaoUseCase;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAutenticacaoQuery _autenticacaoQuery;

        public CadastraClienteCommandHandler(IMediatorHandler mediatorHandler, IAutenticacaoUseCase autenticacaoUseCase, IAutenticacaoQuery autenticacaoQuery)
        {
            _mediatorHandler = mediatorHandler;
            _autenticacaoUseCase = autenticacaoUseCase;
            _autenticacaoQuery = autenticacaoQuery;
        }

        public async Task<AutenticaClienteOutput> Handle(CadastraClienteCommand request, CancellationToken cancellationToken)
        {
            if (request.EhValido())
            {
                try
                {

                    CadastraClienteInput input = request.Input;

                    var clienteDto = new CadastraClienteDto(_autenticacaoUseCase.EncryptPassword(input.Senha), input);

                    if (await _autenticacaoQuery.ClienteJaExiste(clienteDto))
                    {
                        await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Cliente ja cadastrado"));
                        return new AutenticaClienteOutput();
                    }

                    await _autenticacaoQuery.CadastraCliente(clienteDto);

                    var identificaDto = new IdentificaDto(clienteDto.CPF, clienteDto.Senha);

                    var autenticado = await _autenticacaoUseCase.AutenticaCliente(identificaDto);

                    if (string.IsNullOrEmpty(autenticado.Nome))
                    {
                        await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Usuário ou senha inválidos"));
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
