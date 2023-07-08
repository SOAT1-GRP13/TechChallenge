using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Catalogo.Boundaries
{
    public class ProdutoEditarInput
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [SwaggerSchema(
            Title = "Id",
            Description = "Id do produto que vai ser editado",
            Format = "Guid")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [SwaggerSchema(
            Title = "CategoriaId",
            Description = "Preencha com o uuid da categoria",
            Format = "Guid")]
        public Guid CategoriaId { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [SwaggerSchema(
            Title = "Nome",
            Description = "Preencha com o nome do produto",
            Format = "string")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [SwaggerSchema(
            Title = "Descricao",
            Description = "Preencha com a descrição da produto",
            Format = "string")]
        public string Descricao { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [SwaggerSchema(
            Title = "Ativo",
            Description = "Define se está ativo ou não",
            Format = "bool")]
        public bool Ativo { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [SwaggerSchema(
            Title = "Valor",
            Description = "Preencha com o valor do produto",
            Format = "decimal")]
        public decimal Valor { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [SwaggerSchema(
            Title = "Imagem",
            Description = "Base64 para salvar a imagem",
            Format = "string")]
        public string Imagem { get; set; }
    }

}
