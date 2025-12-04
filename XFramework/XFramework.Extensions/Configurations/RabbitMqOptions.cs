namespace XFramework.Extensions.Configurations
{
    public class RabbitMqOptions
    {
        public string? Hostname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }

        public bool Enabled =>
            !string.IsNullOrWhiteSpace(Hostname) &&
            !string.IsNullOrWhiteSpace(Username) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(ExchangeName) &&
            !string.IsNullOrWhiteSpace(QueueName) &&
            !string.IsNullOrWhiteSpace(RoutingKey);
    }
}
