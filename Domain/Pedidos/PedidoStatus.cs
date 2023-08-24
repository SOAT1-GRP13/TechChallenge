using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pedidos
{
    public enum PedidoStatus
    {
        Rascunho = 0,
        Iniciado = 1,
        Pago = 2,
        Cancelado = 3,
        Pronto = 4,
        EmPreparacao = 5,
        Recebido = 6,
        Finalizado = 7
    }
}
