using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        protected ControllerBase(INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao(); // se tem alguma notificacao de problema, retorna operacao invalida.
        }

        protected IEnumerable<string> ObterMensagensErro()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }

        protected Guid ObterClienteId()
        {

            //TODO - Verificar se o cliente está autenticado e recupera o id
            if (!string.IsNullOrEmpty(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }

            //Caso não esteja autenticado, retorna o id de cliente visitante
            return Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");
        }
    }
}
