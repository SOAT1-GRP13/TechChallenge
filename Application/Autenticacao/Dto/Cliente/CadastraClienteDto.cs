using Application.Autenticacao.Boundaries.Cliente;

namespace Application.Autenticacao.Dto.Cliente
{
    public class CadastraClienteDto
    {
        public CadastraClienteDto(string senha, CadastraClienteInput input)
        {
            CPF = input.CPF.Trim().Replace(".", "").Replace("-", "");
            Senha = senha;
            Email = input.Email;
            Nome = input.Nome;
        }


        public string CPF { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
    }
}
