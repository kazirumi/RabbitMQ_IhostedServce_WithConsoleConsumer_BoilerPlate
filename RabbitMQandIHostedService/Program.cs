using RabbitMQandIHostedService.BackGroundTask;
using RabbitMQandIHostedService.Data;
using RabbitMQandIHostedService.RabbitMQProducerLogic;
using RabbitMQandIHostedService.Services;
using RabbitMQandIHostedService.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IWorker,Worker>();
builder.Services.AddHostedService<FirstBackGroundTask>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContext<DBContextClass>();
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
