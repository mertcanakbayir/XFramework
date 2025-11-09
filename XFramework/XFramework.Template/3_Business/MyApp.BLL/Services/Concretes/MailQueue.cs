using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace MyApp.BLL.Services.Concretes
{
    public class MailQueueService
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;

        public MailQueueService(string hostname, string username, string password)
        {
            _hostname = hostname;
            _username = username;
            _password = password;
        }

        public async Task EnqueueMailAsync(string to, string subject, string body, int settingId = 0)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "mail_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var message = new
            {
                To = to,
                Subject = subject,
                Body = body,
                SettingId = settingId
            };

            var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            var props = new BasicProperties
            {
                ContentType = "application/json",
                DeliveryMode = DeliveryModes.Persistent
            };

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "mail_queue",
                mandatory: false,
                basicProperties: props,
                body: messageBytes
            );
        }
    }
}
