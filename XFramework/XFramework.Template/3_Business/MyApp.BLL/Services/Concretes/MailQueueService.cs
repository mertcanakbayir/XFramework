using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using XFramework.Extensions.Configurations;

namespace MyApp.BLL.Services.Concretes
{
    public class MailQueueService : IAsyncDisposable
    {
        private readonly RabbitMqOptions _options;
        private IConnection? _connection;
        private IChannel? _channel;

        public MailQueueService(IOptions<RabbitMqOptions> options)
        {
            _options = options.Value;
        }

        private async Task EnsureConnectionAsync()
        {
            if (!_options.Enabled)
                throw new InvalidOperationException("RabbitMQ is disabled or not configured.");

            if (_connection != null)
                return;

            var factory = new ConnectionFactory
            {
                HostName = _options.Hostname,
                UserName = _options.Username,
                Password = _options.Password
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(
                exchange: _options.ExchangeName,
                type: ExchangeType.Direct,
                durable: true
            );

            await _channel.QueueDeclareAsync(
                queue: _options.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            await _channel.QueueBindAsync(
                queue: _options.QueueName,
                exchange: _options.ExchangeName,
                routingKey: _options.RoutingKey
            );
        }

        public async Task EnqueueMailAsync(string to, string subject, string body, int settingId = 0)
        {
            await EnsureConnectionAsync();

            var messageObj = new
            {
                To = to,
                Subject = subject,
                Body = body,
                SettingId = settingId
            };

            var jsonBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(messageObj));

            var props = new BasicProperties
            {
                ContentType = "application/json",
                DeliveryMode = DeliveryModes.Persistent
            };

            await _channel!.BasicPublishAsync(
                exchange: _options.ExchangeName,
                routingKey: _options.RoutingKey,
                mandatory: false,
                basicProperties: props,
                body: jsonBytes
            );
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel != null)
                await _channel.DisposeAsync();

            if (_connection != null)
                await _connection.DisposeAsync();
        }
    }
}
