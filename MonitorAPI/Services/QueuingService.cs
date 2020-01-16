using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using UtilityLib.Data;

namespace MonitorApi.Services
{
    public class QueueService : IQueueProvider
    {
        public QueueService(ILogger<QueueService> logger, WoodchuckDbContext context)
        {
            _logger = logger;

            _hostName = (from setting in context.MonitorSettings
                           where setting.Key == "RabbitMQ-HostName"
                           select setting.Value).FirstOrDefault();

            _exchangeName = (from setting in context.MonitorSettings
                               where setting.Key == "RabbitMQ-ExchangeName"
                               select setting.Value).FirstOrDefault();

            _queueName = (from setting in context.MonitorSettings
                            where setting.Key == "RabbitMQ-QueueName"
                            select setting.Value).FirstOrDefault();

            _routingKey = (from setting in context.MonitorSettings
                             where setting.Key == "RabbitMQ-RoutingKey"
                             select setting.Value).FirstOrDefault();
        }

        public void PublishMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(
                    exchange: _exchangeName,
                    type: "direct");
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(
                    exchange: _exchangeName,
                    routingKey: _routingKey,
                    basicProperties: null,
                    body: body);            }
        }

        private ILogger<QueueService> _logger;
        private string _hostName;
        private string _exchangeName;
        private string _queueName;
        private string _routingKey;
        
    }
}
