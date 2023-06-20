using Blog.Domain.Interfaces.Integration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Blog.Infra.Data.Messages
{
    public class MessageSender : IMessageSender
    {
        private IConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MessageSender> _logger;

        public MessageSender(ILogger<MessageSender> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void SendMessage(byte[] message, string queueKey)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();

                var queueName = GetQueueName(queueKey);
                
                channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: message);
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();
            return _connection != null;
        }

        private void CreateConnection()
        {
            try
            {
                var connSection = _configuration.GetSection("RabbitMQ");

                var factory = new ConnectionFactory
                {
                    HostName = connSection["Hostname"],
                    Port = Convert.ToInt32(connSection["Port"]),
                    UserName = connSection["Username"],
                    Password = connSection["Password"]
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                _logger.LogError("[MessageSender.CreateConnection] Error: {0}", ex.Message);
                throw;
            }
        }

        private string? GetQueueName(string key)
            => _configuration.GetSection("RabbitMQ").GetSection("Queues")[key];
    }
}