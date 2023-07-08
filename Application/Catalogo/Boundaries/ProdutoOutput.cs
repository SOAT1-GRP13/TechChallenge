using Swashbuckle.AspNetCore.Annotations;

namespace Application.Catalogo.Boundaries
{
    public class ProdutoOutput
    {

        [SwaggerSchema(
            Title = "Id",
            Description = "Id do produto",
            Format = "Guid")]
        public Guid Id { get; set; }

        [SwaggerSchema(
            Title = "CategoriaId",
            Description = "Id da categoria",
            Format = "Guid")]
        public Guid CategoriaId { get; set; }

        [SwaggerSchema(
            Title = "Nome",
            Description = "Nome do produto",
            Format = "string")]
        public string Nome { get; set; }

        [SwaggerSchema(
            Title = "Descricao",
            Description = "Descrição do produto",
            Format = "string")]
        public string Descricao { get; set; }

        [SwaggerSchema(
            Title = "Ativo",
            Description = "se o produto está ativo ou não",
            Format = "bool")]
        public bool Ativo { get; set; }

        [SwaggerSchema(
            Title = "Valor",
            Description = "valor do produto",
            Format = "decimal")]
        public decimal Valor { get; set; }

        [SwaggerSchema(
            Title = "Imagem",
            Description = "Base64 da imagem",
            Format = "string")]
        public string Imagem { get; set; }
    }

}
