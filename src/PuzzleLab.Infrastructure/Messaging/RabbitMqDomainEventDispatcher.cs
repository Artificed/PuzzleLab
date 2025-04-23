using System.Text.Json;
using RabbitMQ.Client;
using PuzzleLab.Domain.Common;

namespace PuzzleLab.Infrastructure.Messaging;

public class RabbitMqDomainEventDispatcher : IDomainEventDispatcher, IAsyncDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    private bool _disposed;

    private RabbitMqDomainEventDispatcher(IConnection connection, IChannel channel)
    {
        _connection = connection;
        _channel = channel;

        _channel.ExchangeDeclareAsync("domain-events", ExchangeType.Fanout, durable: true);
        _channel.QueueDeclareAsync("domain-events-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBindAsync("domain-events-queue", "domain-events", string.Empty);
    }

    public static async Task<RabbitMqDomainEventDispatcher> CreateAsync(string rabbitMqConnectionString)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(rabbitMqConnectionString),
        };

        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        return new RabbitMqDomainEventDispatcher(connection, channel);
    }

    public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(domainEvent, domainEvent.GetType());
        await _channel.BasicPublishAsync(exchange: "domain-events", routingKey: string.Empty, body: body,
            cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        if (_channel?.IsOpen == true)
        {
            try
            {
                await _channel.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error closing RabbitMQ channel: {ex.Message}");
            }
        }

        _channel?.Dispose();

        if (_connection?.IsOpen == true)
        {
            try
            {
                await _connection.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error closing RabbitMQ connection: {ex.Message}");
            }
        }

        _connection?.Dispose();

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}