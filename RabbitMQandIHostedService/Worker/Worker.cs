namespace RabbitMQandIHostedService.Worker
{
    public class Worker : IWorker
    {
        private readonly ILogger _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public void Run()
        {
            _logger.LogInformation("Hi From worker");
        }
    }
}
