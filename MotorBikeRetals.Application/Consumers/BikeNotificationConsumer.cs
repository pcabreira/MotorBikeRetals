using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MotorBikeRetals.Core.Entities;
using MotorBikeRetals.Core.IntegrationEvents;
using MotorBikeRetals.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MotorBikeRetals.Application.Consumers
{
    public class BikeNotificationConsumer : BackgroundService
    {
        private const string BIKE_CREATED_QUEUE = "bike-created";
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public BikeNotificationConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: BIKE_CREATED_QUEUE,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var bikeCreatedBytes = eventArgs.Body.ToArray();
                var bikeCreatedJson = Encoding.UTF8.GetString(bikeCreatedBytes);
                var bikeCreatedIntegrationEvent = JsonSerializer.Deserialize<BikeCreatedIntegrationEvent>(bikeCreatedJson);
                
                Console.WriteLine($"Message received: {bikeCreatedJson}");

                if (bikeCreatedIntegrationEvent.Year == DateTime.Now.Year)
                {
                    var notification = new BikeNotification(bikeCreatedIntegrationEvent.Id, $"Motorcycle of the year {bikeCreatedIntegrationEvent.Year}");
                    await NotificationBikeCreated(notification);
                }

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(BIKE_CREATED_QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        public async Task NotificationBikeCreated(BikeNotification bikeNotification)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var bikeRepository = scope.ServiceProvider.GetRequiredService<IBikeNotificationsRepository>();
                await bikeRepository.AddAsync(bikeNotification);
            }
        }
    }
}
