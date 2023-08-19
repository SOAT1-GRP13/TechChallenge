using Application.Autenticacao.Boundaries.LogIn;
using Application.Autenticacao.Commands;
using Application.Autenticacao.Dto;
using Application.Autenticacao.UseCases;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;

namespace Application.Autenticacao.Handlers
{
    public class AdminAutenticacaoCommandHandler :
        IRequestHandler<AdminAutenticaCommand, LogInUsuarioOutput>
    {
        private readonly IAutenticacaoUseCase _autenticacaoUseCase;
        private readonly IMediatorHandler _mediatorHandler;

        public AdminAutenticacaoCommandHandler(IAutenticacaoUseCase autenticacaoUseCase,
            IMediatorHandler mediatorHandler)
        {
            _autenticacaoUseCase = autenticacaoUseCase;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<LogInUsuarioOutput> Handle(AdminAutenticaCommand request, CancellationToken cancellationToken)
        {
            if (request.EhValido())
            {
                LogInUsuarioInput input = request.Input;
                var loginDto = new LoginUsuarioDto(input.NomeUsuario, _autenticacaoUseCase.EncryptPassword(input.Senha));

                var autenticado = await _autenticacaoUseCase.AutenticaUsuario(loginDto);

                if (string.IsNullOrEmpty(autenticado.NomeUsuario))
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, "Usuário ou senha inválidos"));
                }
                else
                {
                    return autenticado;
                }

            }
            else
            {
                foreach (var error in request.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(request.MessageType, error.ErrorMessage));
                }
            }

            return new LogInUsuarioOutput();
        }
    }
}
