using Application.Catalogo.Boundaries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(NomeErroMsg);

            RuleFor(c => c.Valor)
                .GreaterThan(0)
                .WithMessage(ValorErroMsg);
        }
    }
}
