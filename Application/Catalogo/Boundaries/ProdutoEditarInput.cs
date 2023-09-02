using Swashbuckle.AspNetCore.Annotations;

namespace Application.Catalogo.Boundaries
{
    public class ProdutoEditarInput
    {
        public ProdutoEditarInput()
        {
            Id = Guid.NewGuid();
            CategoriaId = Guid.NewGuid();
            Nome = string.Empty;
            Ativo = false;
            Valor = 0;
            Imagem = string.Empty;
            Descricao = string.Empty;
        }

        [SwaggerSchema(
            Title = "Id",
            Description = "Id do produto que vai ser editado",
            Format = "Guid")]
        public Guid Id { get; set; }

        [SwaggerSchema(
            Title = "CategoriaId",
            Description = "Preencha com o uuid da categoria",
            Format = "Guid")]
        public Guid CategoriaId { get; set; }

        [SwaggerSchema(
            Title = "Nome",
            Description = "Preencha com o nome do produto",
            Format = "string")]
        public string Nome { get; set; }

        [SwaggerSchema(
            Title = "Descricao",
            Description = "Preencha com a descrição da produto",
            Format = "string")]
        public string Descricao { get; set; }

        [SwaggerSchema(
            Title = "Ativo",
            Description = "Define se está ativo ou não",
            Format = "bool")]
        public bool Ativo { get; set; }

        [SwaggerSchema(
            Title = "Valor",
            Description = "Preencha com o valor do produto",
            Format = "decimal")]
        public decimal Valor { get; set; }

        [SwaggerSchema(
            Title = "Imagem",
            Description = "Base64 para salvar a imagem",
            Format = "string")]
        public string Imagem { get; set; }
    }

}
