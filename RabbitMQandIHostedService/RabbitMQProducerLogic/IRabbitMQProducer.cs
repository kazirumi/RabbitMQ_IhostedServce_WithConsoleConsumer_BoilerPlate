namespace RabbitMQandIHostedService.RabbitMQProducerLogic
{
    public interface IRabbitMQProducer
    {
        public void SendMessage<T>(T message, string queueName);
    }
}
