using Application.Autenticacao.Boundaries.Cliente;
using Application.Autenticacao.Boundaries.LogIn;

namespace Application.Autenticacao.Services
{
    public interface IAutenticacaoService : IDisposable
    {
        Task<LogInUsuarioOutput> AutenticaUsuario(LogInUsuarioInput input);
        Task<IdentificaOutput> AutenticaCliente(IdentificaInput input);
        Task<IdentificaOutput> CadastraCliente(CadastraClienteInput input);
    }
}
