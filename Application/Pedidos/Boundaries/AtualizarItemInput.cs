using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Application.Pedidos.Boundaries
{
    public class AtualizarItemInput
    {
        [Required]
        [SwaggerSchema(
            Title = "Guid do produto",
            Format = "Guid")]
        public Guid Id { get; set; }

        [Required]
        [SwaggerSchema(
            Title = "Quantidade",
            Format = "int")]
        public int Quantidade { get; set; }
    }
}
