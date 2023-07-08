using Domain.Autenticacao;
using Domain.Base.Data;
using Domain.Catalogo;
using Microsoft.EntityFrameworkCore;

namespace Infra.Autenticacao.Repository
{
    public class AutenticacaoRepository : IAutenticacaoRepository
    {
        private readonly AutenticacaoContext _context;

        public AutenticacaoRepository(AutenticacaoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<AcessoUsuario> AutenticaUsuario(AcessoUsuario login)
        {
            var usuario = await _context.AcessoUsuario.Where(x => x.NomeUsuario == login.NomeUsuario && x.Senha == login.Senha).AsNoTracking().FirstOrDefaultAsync();

            if (usuario is null)
            {
                return new AcessoUsuario();
            }
            else
            {
                return usuario;
            }
        }

        public async Task<AcessoCliente> AutenticaCliente(AcessoCliente login)
        {
            var usuario = await _context.AcessoCliente.Where(x => x.CPF == login.CPF && x.Senha == login.Senha).AsNoTracking().FirstOrDefaultAsync();

            if (usuario is null)
            {
                return new AcessoCliente();
            }
            else
            {
                return usuario;
            }
        }

        public void CadastraCliente(AcessoCliente cliente)
        {
            _context.AcessoCliente.Add(cliente);
        }

        public async Task<bool> ClienteJaExiste(AcessoCliente login)
        {
            return await _context.AcessoCliente.Where(x => x.CPF == login.CPF || x.Email == login.Email).AnyAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}