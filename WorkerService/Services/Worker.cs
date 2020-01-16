using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UtilityLib.Data;
using UtilityLib.Models;

namespace WorkerService.Services
{
    public class Worker : IHostedService
    {
        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;

            _woodchuck = config.GetConnectionString("WoodchuckDb");
            _initialized = false;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Worker] Worker starting at {0}", DateTime.Now);

            await Initialize();

            var factory = new ConnectionFactory() { HostName = _config["RabbitMQ-HostName"], DispatchConsumersAsync = true };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(
                exchange: _config["RabbitMQ-ExchangeName"],
                type: "direct");
            channel.QueueDeclare(_config["RabbitMQ-QueueName"]);
            channel.QueueBind(
                queue: _config["RabbitMQ-QueueName"],
                exchange: _config["RabbitMQ-ExchangeName"],
                routingKey: _config["RabbitMQ-RoutingKey"]);
            consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(
                queue: _config["RabbitMQ-QueueName"],
                autoAck: true,
                consumer: consumer);
        }

        private async Task Initialize()
        {
            if (_initialized) return;

            for (int i=0; i<5; i++)
            {
                try
                {
                    var optionsBuilder = new DbContextOptionsBuilder<WoodchuckDbContext>();
                    optionsBuilder.UseNpgsql(_woodchuck);

                    using (var context = new WoodchuckDbContext(optionsBuilder.Options))
                    {
                        _config = context.WorkerSettings.ToDictionary(x => x.Key, x => x.Value);
                    }

                    _initialized = true;
                    break;
                }
                catch(Exception)
                {
                    if (i >= 4) throw;

                    await Task.Delay(10000);
                }
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            _logger.LogInformation("[Worker] Worker processing message at {0}", DateTime.Now);

            var optionsBuilder = new DbContextOptionsBuilder<WoodchuckDbContext>();
            optionsBuilder.UseNpgsql(_woodchuck);

            using (var context = new WoodchuckDbContext(optionsBuilder.Options))
            {
                var photographer = new Photographer(
                    context.CameraSettings.ToList<CameraSettings>());
                var photos = await photographer.GetPhotos();

                var scribe = new Scribe(
                    _config["Storage_Uri"]);
                var recordedPhotos = await scribe.SaveStreams(photos);

                var messenger = new Messenger(
                    _config["Twilio_User"],
                    _config["Twilio_Password"],
                    _config["Twilio_Telephone"],
                    context.NotificationSettings.Where(
                        x => x.Enabled == true).
                        ToList<NotificationSettings>());

                messenger.SendMessage("Doorbell alert!", recordedPhotos);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Worker] Worker stopping at {0}", DateTime.Now);

            consumer.Received -= Consumer_Received;
            consumer = null;

            channel.Close();
            channel = null;

            connection.Close();
            connection = null;
        }

        private ILogger<Worker> _logger;
        private string _woodchuck;
        private Dictionary<string, string> _config;

        private bool _initialized;
        private IConnection connection;
        private IModel channel;
        private AsyncEventingBasicConsumer consumer;
    }
}
