namespace Application.Autenticacao.Dto.Cliente
{
    public class IdentificaDto
    {
        #region Construtores
        public IdentificaDto()
        {
            CPF = string.Empty;
            Senha = string.Empty;
        }
        public IdentificaDto(string cPF, string senha)
        {
            CPF = cPF.Trim().Replace(".", "").Replace("-", "");
            Senha = senha;
        }
        #endregion

        public string CPF { get; private set; }
        public string Senha { get; private set; }
    }
}
