
using RabbitMQandIHostedService.Worker;

namespace RabbitMQandIHostedService.BackGroundTask
{
    public class FirstBackGroundTask : BackgroundService
    {
        private readonly IServiceProvider _provider;

        public FirstBackGroundTask(IServiceProvider provider)
        {
            _provider = provider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _provider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IWorker>();

                    service.Run();

                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }
    }
}
