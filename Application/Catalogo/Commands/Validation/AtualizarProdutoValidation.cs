using Application.Catalogo.Boundaries;
using FluentValidation;

namespace Application.Catalogo.Commands.Validation
{
    public class AtualizarProdutoValidation : AbstractValidator<ProdutoEditarInput>
    {
        public static string IdCategoriaErroMsg => "Id da categoria inválida";
        public static string NomeErroMsg => "O nome do produto não foi informado";
        public static string ValorErroMsg => "O valor do item precisa ser maior que 0";

        public AtualizarProdutoValidation()
        {
            RuleFor(c => c.CategoriaId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdCategoriaErroMsg);

            RuleFor(c => c.CategoriaId)
                .NotEmpty()
                .WithMessage("Id da categoria é obrigatório");

            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Id do produto é obrigatório");

            RuleFor(c => c.Imagem)
                .NotEmpty()
                .WithMessage("Imagem é obrigatório");

            RuleFor(c => c.Descricao)
                .NotEmpty()
                .WithMessage("Descrição é obrigatório");


            RuleFor(c => c.Ativo)
                .NotNull()
                .WithMessage("Ativo é obrigatório");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(NomeErroMsg);

            RuleFor(c => c.Valor)
                .GreaterThan(0)
                .WithMessage(ValorErroMsg);
        }
    }
}
