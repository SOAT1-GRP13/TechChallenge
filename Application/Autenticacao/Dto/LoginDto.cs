namespace Application.Autenticacao.Dto
{
    public class LoginUsuarioDto
    {
        #region Construtores
        public LoginUsuarioDto()
        {
            NomeUsuario = string.Empty;
            Senha = string.Empty;
        }
        public LoginUsuarioDto(string nomeUsuario, string senha)
        {
            NomeUsuario = nomeUsuario;
            Senha = senha;
        }
        #endregion

        public string NomeUsuario { get; private set; }
        public string Senha { get; private set; }
    }
}
