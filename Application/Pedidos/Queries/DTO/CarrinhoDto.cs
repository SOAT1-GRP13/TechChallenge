using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Pedidos.Queries.DTO
{
    public class CarrinhoDto
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorTotal { get; set; }


        public List<CarrinhoItemDto> Items { get; set; } = new List<CarrinhoItemDto>();
    }
}
