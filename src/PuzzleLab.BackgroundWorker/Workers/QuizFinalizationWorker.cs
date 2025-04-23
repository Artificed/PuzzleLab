using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PuzzleLab.Domain.Events;
using PuzzleLab.Domain.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PuzzleLab.BackgroundWorker.Workers
{
    public class QuizFinalizationWorker : BackgroundService
    {
        private readonly ILogger<QuizFinalizationWorker> _logger;
        private readonly IQuizSessionRepository _sessionRepo;
        private readonly IQuizAnswerRepository _answerRepo;
        private readonly string _rabbitMqUri;

        private IConnection? _connection;
        private IChannel? _channel;

        private const string ExchangeName = "domain-events";
        private const string QueueName = "domain-events-queue";

        public QuizFinalizationWorker(
            ILogger<QuizFinalizationWorker> logger,
            IQuizSessionRepository sessionRepo,
            IQuizAnswerRepository answerRepo,
            string rabbitMqUri)
        {
            _logger = logger;
            _sessionRepo = sessionRepo;
            _answerRepo = answerRepo;
            _rabbitMqUri = rabbitMqUri;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            // 1) Async connect
            var factory = new ConnectionFactory { Uri = new Uri(_rabbitMqUri) };
            _connection = await factory.CreateConnectionAsync(cancellationToken);
            _channel = await _connection.CreateChannelAsync(options: null, cancellationToken);

            // 2) Async topology
            await _channel.ExchangeDeclareAsync(
                exchange: ExchangeName,
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken);

            await _channel.QueueDeclareAsync(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken);

            await _channel.QueueBindAsync(
                queue: QueueName,
                exchange: ExchangeName,
                routingKey: string.Empty,
                arguments: null,
                cancellationToken: cancellationToken);

            _logger.LogInformation("RabbitMQ async setup complete: exchange='{ex}', queue='{q}'",
                ExchangeName, QueueName);

            await base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel!);
            consumer.ReceivedAsync += async (_, ea) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var @event = JsonSerializer.Deserialize<QuizFinalizedEvent>(json);
                    if (@event == null) throw new Exception("Invalid event payload");

                    _logger.LogInformation("Received QuizFinalizedEvent for Session {SessionId}",
                        @event.SessionId);

                    var session = await _sessionRepo
                        .GetQuizSessionByIdAsync(@event.SessionId, stoppingToken)
                        .ConfigureAwait(false);
                    if (session == null) throw new Exception("Session not found");

                    var answers = await _answerRepo
                        .GetBySessionIdAsync(session.Id, stoppingToken)
                        .ConfigureAwait(false);
                    session.UpdateCorrectAnswers(answers.Count(a => a.IsCorrect));
                    await _sessionRepo.UpdateQuizSessionAsync(session, stoppingToken)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error handling QuizFinalizedEvent");
                }
                finally
                {
                    await _channel!.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
                }
            };

            _channel!.BasicConsumeAsync(
                queue: QueueName,
                autoAck: false,
                consumer: consumer,
                cancellationToken: stoppingToken);

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_channel != null) await _channel.CloseAsync(cancellationToken);
            if (_connection != null) await _connection.CloseAsync(cancellationToken);
            await base.StopAsync(cancellationToken);
        }
    }
}