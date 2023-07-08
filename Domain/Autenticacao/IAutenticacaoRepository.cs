using Domain.Base.Data;

namespace Domain.Autenticacao
{
    public interface IAutenticacaoRepository: IRepository<AcessoCliente>
    {
        Task<AcessoUsuario> AutenticaUsuario(AcessoUsuario login);

        Task<AcessoCliente> AutenticaCliente(AcessoCliente login);

        Task<bool> ClienteJaExiste(AcessoCliente login);

        void CadastraCliente (AcessoCliente login);
    }
}
