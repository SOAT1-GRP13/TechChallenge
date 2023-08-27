using Domain.Pedidos;

namespace Application.Pedidos.Queries.DTO
{
    public class PedidoDto
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCadastro { get; set; }
        public PedidoStatus PedidoStatus { get; set; }
    }
}
