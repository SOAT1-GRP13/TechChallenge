namespace Domain.Configuration
{
    public class Secrets
    {
        public Secrets()
        {
            MercadoPagoUserId = string.Empty;
            AccesToken = string.Empty;
            Notification_url = string.Empty;
            External_Pos_Id = string.Empty;
            ClientSecret = string.Empty;
            PreSalt = string.Empty;
            PosSalt = string.Empty;
            ConnectionString = string.Empty;
        }

        public string MercadoPagoUserId { get; set; }
        public string AccesToken { get; set; }
        public string Notification_url { get; set; }
        public string External_Pos_Id { get; set; }
        public string ClientSecret { get; set; }
        public string PreSalt { get; set; }
        public string PosSalt { get; set; }
        public string ConnectionString { get; set; }
    }
}