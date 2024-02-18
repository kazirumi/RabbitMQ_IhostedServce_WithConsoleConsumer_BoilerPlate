using RabbitMQandIHostedService.BackGroundTask;
using RabbitMQandIHostedService.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IWorker,Worker>();
builder.Services.AddHostedService<FirstBackGroundTask>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
