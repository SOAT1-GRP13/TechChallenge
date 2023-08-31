namespace Domain.ValueObjects
{
    public class ConfiguracaoMercadoPago
    {
        public ConfiguracaoMercadoPago()
        {
            UserId = string.Empty;
            AccesToken = string.Empty;
            Notification_url = string.Empty;
            External_Pos_Id = string.Empty;
        }

        public const string Configuration = "ConfiguracaoMercadoPago";
        public string UserId { get; set; }
        public string AccesToken { get; set; }
        public string Notification_url { get; set; }
        public string External_Pos_Id { get; set; }
    }
}
