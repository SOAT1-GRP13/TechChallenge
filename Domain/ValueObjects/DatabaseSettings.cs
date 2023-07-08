namespace Domain.ValueObjects
{
    public class DatabaseSettings
    {
        public DatabaseSettings()
        {
            ConnectionString = string.Empty;
        }

        public const string DatabaseConfiguration = "DatabaseSettings";
        public string ConnectionString { get; set; }
    }
}
