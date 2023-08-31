namespace Domain.MercadoPago
{
public class MercadoPagoQrResponse
{
        public MercadoPagoQrResponse()
        {
            In_store_order_id = string.Empty;
            Qr_data = string.Empty;
        }

        public string In_store_order_id { get; set; }
        public string Qr_data { get; set; }
}
}