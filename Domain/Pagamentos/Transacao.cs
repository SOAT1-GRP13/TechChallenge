using Domain.Base.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Pagamentos
{
    public class Transacao : Entity
    {
        public Guid PedidoId { get; set; }
        public Guid PagamentoId { get; set; }
        public decimal Total { get; set; }
        public StatusTransacao StatusTransacao { get; set; }

        // EF. Rel.
        public Pagamento Pagamento { get; set; }
    }
}
