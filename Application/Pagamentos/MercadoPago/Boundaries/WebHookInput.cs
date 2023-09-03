namespace Application.Pagamentos.MercadoPago.Boundaries
{
    public class WebHookInput
    {
        public WebHookInput()
        {
            Action = string.Empty;
            Api_version = string.Empty;
            Application_id = string.Empty;
            Date_created = DateTime.Now;
            Id = 0;
            Live_mode = false;
            Type = string.Empty;
            User_id = 0;
            Data = new WebHookInputData();
        }

        public string Action { get; set; }
        public string Api_version { get; set; }
        public string Application_id { get; set; }
        public DateTime Date_created { get; set; }
        public int Id { get; set; }
        public bool Live_mode { get; set; }
        public string Type { get; set; }
        public int User_id { get; set; }
        public WebHookInputData Data { get; set; }
    }
}
