namespace Application.Pagamentos.MercadoPago.DTOs
{
    public class MercadoPagoOrderStatus
    {
        public MercadoPagoOrderStatus()
        {
            Id = 0;
            Status = string.Empty;
            External_reference = string.Empty;
        }

        public long Id { get; set; }
        public string Status { get; set; }
        public string External_reference { get; set; }

    }
}