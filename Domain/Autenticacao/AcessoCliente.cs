using Domain.Base.DomainObjects;

namespace Domain.Autenticacao
{
    public class AcessoCliente : Entity, IAggregateRoot
    {
        #region construtores
        public AcessoCliente()
        {
            CPF = string.Empty;
            Senha = string.Empty;
            Nome = string.Empty;
            Email = string.Empty;
        }

        public AcessoCliente(string cPF, string senha)
        {
            CPF = cPF;
            Senha = senha;
            Email = string.Empty;
            Nome = string.Empty;

            ValidarAutenticacao();
        }

        public AcessoCliente(string cPF, string senha, string email, string nome)
        {
            CPF = cPF;
            Senha = senha;
            Email = email;
            Nome = nome;

            ValidarCadastro();
        }

        #endregion

        public string CPF { get; private set; }

        public string Senha { get; private set; }

        public string Nome { get; private set; }

        public string Email { get; private set; }


        public void ValidarAutenticacao()
        {
            Validacoes.ValidarSeVazio(CPF, "O campo CPF não pode estar vazio");
            Validacoes.ValidarCPF(CPF, "CPF inválido");
            Validacoes.ValidarSeVazio(Senha, "O campo Senha não pode estar vazio");
        }

        public void ValidarCadastro()
        {
            Validacoes.ValidarSeVazio(CPF, "O campo CPF não pode estar vazio");
            Validacoes.ValidarCPF(CPF, "CPF inválido");
            Validacoes.ValidarSeVazio(Senha, "O campo Senha não pode estar vazio");
            Validacoes.ValidarSeVazio(Nome, "O campo Nome não pode estar vazio");
            Validacoes.ValidarSeVazio(Email, "O campo Email não pode estar vazio");
            Validacoes.ValidarEmail(Email, "Email inválido");
        }
    }
}