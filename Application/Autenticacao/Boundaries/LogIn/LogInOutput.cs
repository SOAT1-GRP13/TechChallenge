using Application.ServiceResult;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Autenticacao.Boundaries.LogIn
{
    public class LogInUsuarioOutput
    {
        #region construtores
        public LogInUsuarioOutput()
        {
            NomeUsuario = string.Empty;
            TokenAcesso = string.Empty;

        }
        public LogInUsuarioOutput(string nomeUsuario, string tokenAcesso)
        {
            TokenAcesso = tokenAcesso;
            NomeUsuario = nomeUsuario;
        }

        #endregion

        [SwaggerSchema(
            Title = "NomeUsuario",
            Description = "Nome do usuario cadastrado",
            Format = "string")]
        public string NomeUsuario { get; set; }

        [SwaggerSchema(
            Title = "TokenAcesso",
            Description = "Token de acesso do usuario",
            Format = "string")]
        public string TokenAcesso { get; set; }


        //Futuramente
        //public List<int> Pdvs { get; private set; }
    }
}
