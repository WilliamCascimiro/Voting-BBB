using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.FraudPreventionWorker.Application
{
    public class Producer
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public Producer(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration.GetSection("RabbitMQ")["HostName"],
                Port = 5672
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "vote2",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }


        public void Publica(string message)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                                    routingKey: "vote2",
                                    basicProperties: properties,
                                    body: body);

        }

        public async Task PublicaAsync(string message)
        {
            await Task.Run(() =>
            {
                var body = Encoding.UTF8.GetBytes(message);
                _channel.BasicPublish(exchange: "",
                                      routingKey: "vote2",
                                      basicProperties: null,
                                      body: body);
            });
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
