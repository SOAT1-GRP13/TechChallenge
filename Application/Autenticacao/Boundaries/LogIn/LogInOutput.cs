using Application.ServiceResult;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Autenticacao.Boundaries.LogIn
{
    public class LogInUsuarioOutput : ServiceResultBase
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
            Sucesso = true;
            Mensagem = string.Empty;
        }

        public LogInUsuarioOutput(bool sucesso, string mensagem)
        {
            TokenAcesso = string.Empty;
            NomeUsuario = string.Empty;
            Sucesso = sucesso;
            Mensagem = mensagem;
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
