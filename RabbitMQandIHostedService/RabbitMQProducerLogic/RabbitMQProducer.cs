using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQandIHostedService.RabbitMQProducerLogic
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IConfiguration _configuration;

        public RabbitMQProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendMessage<T>(T message,string queueName="product")
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration.GetValue<string>("RbbitMQHostName")
            };

            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.QueueDeclare(queueName, durable:true, exclusive: false,autoDelete:false);

            var json= JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "product", body: body);

        }
    }
}
