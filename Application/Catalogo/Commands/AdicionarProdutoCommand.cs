using Application.Catalogo.Boundaries;
using Application.Catalogo.Commands.Validation;
using Domain.Base.Messages;


namespace Application.Catalogo.Commands
{
    public class AdicionarProdutoCommand : Command<ProdutoOutput>
    {
        public AdicionarProdutoCommand(ProdutoInput input)
        {
            Input = input;
        }

        public ProdutoInput Input { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarProdutoValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }

    

}
