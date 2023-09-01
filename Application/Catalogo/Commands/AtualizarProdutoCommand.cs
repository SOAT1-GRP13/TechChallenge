using Application.Catalogo.Boundaries;
using Application.Catalogo.Commands.Validation;
using Domain.Base.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalogo.Commands
{

    public class AtualizarProdutoCommand : Command<ProdutoOutput>
    {
        public AtualizarProdutoCommand(ProdutoEditarInput input)
        {
            Input = input;
        }

        public ProdutoEditarInput Input { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarProdutoValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
