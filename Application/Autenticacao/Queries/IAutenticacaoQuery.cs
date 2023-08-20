using Application.Autenticacao.Dto.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Autenticacao.Queries
{
    public interface IAutenticacaoQuery
    {
        Task<bool> ClienteJaExiste(CadastraClienteDto dto);
        Task CadastraCliente(CadastraClienteDto dto);
    }
}
