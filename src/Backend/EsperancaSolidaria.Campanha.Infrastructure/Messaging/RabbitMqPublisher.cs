using EsperancaSolidaria.Campanha.Domain.Events;
using EsperancaSolidaria.Campanha.Domain.Messaging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace EsperancaSolidaria.Campanha.Infrastructure.Messaging
{
    public class RabbitMqPublisher : IMessagePublisher, IAsyncDisposable
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private const string QUEUE_NAME = "DonationReceivedEvent";

        private RabbitMqPublisher(IConnection connection, IChannel channel)
        {
            _connection = connection;
            _channel = channel;
        }

        public static async Task<RabbitMqPublisher> CreateAsync(ConnectionFactory factory)
        {
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: QUEUE_NAME,
                durable: true,
                exclusive: false,
                autoDelete: false);

            return new RabbitMqPublisher(connection, channel);
        }

        public async Task PublishDonationReceived(DonationReceivedEvent donationEvent)
        {
            var json = JsonSerializer.Serialize(donationEvent);
            var body = Encoding.UTF8.GetBytes(json);

            var properties = new BasicProperties
            {
                Persistent = true
            };

            await _channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: QUEUE_NAME,
                mandatory: false,
                basicProperties: properties,
                body: body);
        }

        public async ValueTask DisposeAsync()
        {
            await _channel.CloseAsync();
            await _connection.CloseAsync();
        }
    }
}
