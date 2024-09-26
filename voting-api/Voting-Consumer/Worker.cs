using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Voting_Consumer.DTOs;

namespace Voting_Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                RequestedConnectionTimeout = TimeSpan.FromSeconds(10),
                ContinuationTimeout = TimeSpan.FromSeconds(20)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "vote",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 10, global: false);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var voteRequest = JsonConvert.DeserializeObject<VoteRequest>(message);

                try
                {
                    ProcessVote(voteRequest);
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro ao processar voto: {ex.Message}");
                    _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                }

            };

            _channel.BasicConsume(queue: "vote", autoAck: false, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public void ProcessVote(VoteRequest voteRequest)
        {
            var aaa = voteRequest;
            var ttt = "";
            var bbb = "";
            // Lógica para processar o voto e salvar no banco de dados
        }
    }
}
