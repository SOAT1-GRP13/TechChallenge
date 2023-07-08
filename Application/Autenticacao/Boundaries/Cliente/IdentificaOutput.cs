using Application.ServiceResult;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Autenticacao.Boundaries.Cliente
{
    public class IdentificaOutput : ServiceResultBase
    {
        #region construtores
        public IdentificaOutput()
        {
            TokenAcesso = string.Empty;
            Nome = string.Empty;

        }
        public IdentificaOutput(string nome, string tokenAcesso)
        {
            Nome = nome;
            TokenAcesso = tokenAcesso;
            Sucesso = true;
            Mensagem = string.Empty;
        }

        public IdentificaOutput(bool sucesso, string mensagem)
        {
            Nome = string.Empty;
            TokenAcesso = string.Empty;
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        #endregion

        [SwaggerSchema(
            Title = "Nome",
            Description = "Nome cadastrado pelo cliente",
            Format = "string")]
        public string Nome { get; set; }

        [SwaggerSchema(
            Title = "TokenAcesso",
            Description = "Token de acesso do cliente identificado",
            Format = "string")]
        public string TokenAcesso { get; set; }
    }
}
