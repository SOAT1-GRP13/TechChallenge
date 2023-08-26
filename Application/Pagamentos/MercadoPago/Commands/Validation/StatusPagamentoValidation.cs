using Application.Pagamentos.MercadoPago.Boundaries;
using FluentValidation;

namespace Application.Pagamentos.Commands;

public class StatusPagamentoValidation : AbstractValidator<WebHookInput>
{
    public StatusPagamentoValidation()
    {
        RuleFor(x => x.Action)
        .NotEmpty()
        .WithMessage("Action é obrigatório");

        RuleFor(x => x.Id)
        .NotEmpty()
        .WithMessage("Id é obrigatório");

        RuleFor(x => x.Data.Id)
        .NotEmpty()
        .WithMessage("Data Id é obrigatório");
    }

}
