using Domain.Pedidos;

namespace Domain.MercadoPago
{
    public class OrderItem
    {
        public OrderItem()
        {
            Title = string.Empty;
            Description = string.Empty;
            Unit_price = 0;
            Quantity = 0;
            Unit_measure = "";
            Total_amount = 0;
        }

        public OrderItem(PedidoItem item)
        {
            Title = item.ProdutoNome;
            Description = "Observação do item"; //TODO implementar isto no pedidoItem
            Unit_price = item.ValorUnitario;
            Quantity = item.Quantidade;
            Unit_measure = "unit";
            Total_amount = item.Quantidade * item.ValorUnitario; //Pela descrição da documentação esse seria o total destes itens
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Unit_price { get; set; }
        public int Quantity { get; set; }
        public string Unit_measure { get; set; }
        public decimal Total_amount { get; set; }
    }
}