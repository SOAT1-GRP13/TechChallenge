using Application.Pagamentos.MercadoPago.Boundaries;
using FluentValidation;

namespace Application.Pagamentos.MercadoPago.Commands;

public class StatusPagamentoValidation : AbstractValidator<StatusPagamentoCommand>
{
    public StatusPagamentoValidation()
    {
        RuleFor(x => x.Id)
        .NotEmpty()
        .NotEqual(0)
        .WithMessage("Action é obrigatório");

        RuleFor(x => x.Topic)
        .NotEmpty()
        .WithMessage("Topic é obrigatório");
    }

}
