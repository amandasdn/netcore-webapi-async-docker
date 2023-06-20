using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Models.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Blog.Infra.Data.Messages
{
    public class CommentMessageConsumer : BackgroundService
    {
        private IConnection _connection;
        private readonly ILogger<CommentMessageConsumer> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICommentRepository _commentRepository;
        private IModel _channel;

        public CommentMessageConsumer(ILogger<CommentMessageConsumer> logger, IConfiguration configuration, ICommentRepository commentRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _commentRepository = commentRepository;

            CreateConnection();

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: GetQueueName("Comments"), false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                var obj = JsonSerializer.Deserialize<CommentMessage>(content);
                
                SaveData(obj).GetAwaiter().GetResult();
                
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume(GetQueueName("Comments"), false, consumer);
            return Task.CompletedTask;
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
                _logger.LogError("[CommentMessageConsumer.CreateConnection] Error: {0}", ex.Message);
                throw;
            }
        }

        private string? GetQueueName(string key)
            => _configuration.GetSection("RabbitMQ").GetSection("Queues")[key];

        private async Task SaveData(CommentMessage obj)
        {
            try
            {
                CommentEntity entity = new()
                {
                    IdComment = obj.Id,
                    Content = obj.Content,
                    Author = obj.Author,
                    CreatedAt = obj.CreatedAt
                };

                _commentRepository.Insert(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError("[CommentMessageConsumer.SaveData] Error: {0}, Data: {1}", ex.Message, JsonSerializer.Serialize(obj));
                throw;
            }
        }
    }
}
