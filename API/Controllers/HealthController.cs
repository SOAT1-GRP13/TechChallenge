using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Base.Communication.Mediator;
using Swashbuckle.AspNetCore.Annotations;
using Domain.Base.Messages.CommonMessages.Notifications;

namespace API.Controllers
{
    [ApiController]
    [Route("Health")]
    [SwaggerTag("Endpoints relacionados a pedidos, sendo necessário se autenticar")]
    public class HealthController : ControllerBase
    {

        public HealthController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Health check",
            Description = "testa se a API está no ar")]
        [SwaggerResponse(200, "Retorna se OK")]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}
