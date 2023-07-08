using Domain.Autenticacao.Enums;
using Domain.Base.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Autenticacao
{
    [Table("acesso_usuario")]
    public class AcessoUsuario : Entity, IAggregateRoot
    {
        #region construtores
        public AcessoUsuario() 
        {
            NomeUsuario = string.Empty;
            Senha = string.Empty;
            Role = Roles.Cliente;
        }

        public AcessoUsuario(string nomeUsuario, string senha)
        {
            NomeUsuario = nomeUsuario;
            Senha = senha;

            Validar();
        }

        public AcessoUsuario(Guid id, string nomeUsuario, string senha, Roles role)
        {
            Id = id;
            NomeUsuario = nomeUsuario;
            Senha = senha;
            Role = role;
        }
        #endregion

        [Column("nome_usuario")]
        public string NomeUsuario { get; private set; }
        
        [Column("senha")]
        public string Senha { get; private set; }

        [Column("role_usuario")]
        public Roles Role { get; private set; }

        public void Validar()
        {
            Validacoes.ValidarSeVazio(NomeUsuario, "O campo NomeUsuario não pode estar vazio");
            Validacoes.ValidarSeVazio(Senha, "O campo Senha não pode estar vazio");
        }
    }
}
