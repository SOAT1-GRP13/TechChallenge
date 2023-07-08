using Application.Catalogo.Boundaries;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class ProdutoInputExample : IExamplesProvider<ProdutoInput>
    {
        public ProdutoInput GetExamples()
        {
            return new ProdutoInput
            {
                Ativo = true,
                CategoriaId = new Guid("45dd547f-5209-49c6-8cd1-ee5c384e93f2"),
                Descricao = "Hamburguer artesanal com 5 fatias de bacon e muito cheddar",
                Nome = "X-Bacon com Cheddar",
                Valor = 35.9m,
                Imagem = "teste"
            };
        }
    }
}
