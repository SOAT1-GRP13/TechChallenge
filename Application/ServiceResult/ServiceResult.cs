using Swashbuckle.AspNetCore.Annotations;

namespace Application.ServiceResult
{
    public class ServiceResultBase
    {
        public ServiceResultBase()
        {
            Sucesso = false;
            Mensagem = string.Empty;
        }

        [SwaggerSchema(
            Title = "Sucesso",
            Description = "Retorna true caso todas as regras tenham passado",
            Format = "bool")]
        public bool Sucesso { get; set; }

        [SwaggerSchema(
            Title = "Mensagem",
            Description = "Caso Sucesso seja false retorna a mensagem referente ao erro",
            Format = "string")]
        public string Mensagem { get; set; }
    }
}
