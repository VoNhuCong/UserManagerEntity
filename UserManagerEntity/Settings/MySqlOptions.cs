namespace UserManagerEntity.Settings
{
    public class MySqlOptions
    {
        public const string SectionName = nameof(MySqlOptions);

        public string Host { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Port { get; set; }
        public int MaximumPoolSize { get; set; }
        public int CommandTimeout { get; set; }
        public int CancellationTimeout { get; set; }

        public string GetConnectionString()
        {
            return $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password};Maximum Pool Size={MaximumPoolSize};Command Timeout={CommandTimeout}";
        }
    }
}
