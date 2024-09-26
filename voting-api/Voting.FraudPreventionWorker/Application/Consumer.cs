using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.FraudPreventionWorker.DTOs.VoteRequest;

namespace Voting.FraudPreventionWorker.Application
{
    public class Consumer
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IConfiguration _configuration;
        private readonly Producer _producer;

        public Consumer(ILogger<Worker> logger, IConfiguration configuration, Producer producer) 
        {
            _logger = logger;
            _configuration = configuration;
            _producer = producer;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration.GetSection("RabbitMQ")["HostName"],
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

        public async Task ExecuteAsync(CancellationToken stoppingToken)
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

        private async Task ProcessVote(VoteRequest voteRequest)
        {
            var json = JsonConvert.SerializeObject(voteRequest);

            await _producer.PublicaAsync(json);
        }

    }
}
