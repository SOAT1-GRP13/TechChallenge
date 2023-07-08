using Application.Catalogo.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace API.SwaggerExamples
{
    public class ProdutoDtoExample : IMultipleExamplesProvider<ProdutoDto>, IMultipleExamplesProvider<IEnumerable<ProdutoDto>>
    {
        public IEnumerable<SwaggerExample<ProdutoDto>> GetExamples()
        {
            yield return SwaggerExample.Create("Sucesso", new ProdutoDto
            {
                Id = Guid.NewGuid(),
                Ativo = true,
                CategoriaId = new Guid("5a996f0a-e253-497b-ab62-272c84899de5"),
                Descricao = "Hamburguer artesanal com 5 fatias de bacon e muito cheddar",
                Nome = "X-Bacon com Cheddar",
                Valor = 35.9m,
                DataCadastro = DateTime.UtcNow.AddDays(-7),
                Imagem = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAP1vXl75ugp5U39nxz/9k="
            });
        }

        IEnumerable<SwaggerExample<IEnumerable<ProdutoDto>>> IMultipleExamplesProvider<IEnumerable<ProdutoDto>>.GetExamples()
        {
            yield return SwaggerExample.Create<IEnumerable<ProdutoDto>>("Sucesso", new[]
            {
                new ProdutoDto
                {
                    Id = Guid.NewGuid(),
                    Ativo = true,
                    CategoriaId = new Guid("5a996f0a-e253-497b-ab62-272c84899de5"),
                    Descricao = "Hamburguer artesanal com 5 fatias de bacon e muito cheddar",
                    Nome = "X-Bacon com Cheddar",
                    Valor = 35.9m,
                    DataCadastro = DateTime.UtcNow.AddDays(-7),
                    Imagem = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAP1vXl75ugp5U39nxz/9k="
                }
            });
        }
    }
}
