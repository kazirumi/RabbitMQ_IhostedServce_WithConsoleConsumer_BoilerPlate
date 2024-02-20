// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQandIHostedService.Data;
using RabbitMQandIHostedService.Models;
using RabbitMQandIHostedService.Services;
using Serilog;
using System.Text;

//Basic setup for using appsettings.json and serilog

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Build())
                                        .Enrich.FromLogContext()
                                        .WriteTo.Console()
                                        .CreateLogger();

IConfiguration configuration = builder.Build();


Log.Logger.Information("Application Product Consumer Starting ");

// This portion make a separate dependency injection for console
var host = Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddDbContext<DBContextClass>();
        })
        .UseSerilog()
        .Build();

var productService = ActivatorUtilities.CreateInstance<ProductService>(host.Services);


//Consumer Code Added here

var factory = new ConnectionFactory
{
    HostName = configuration.GetValue<string>("RbbitMQHostName")
};

var connection = factory.CreateConnection();

var channel = connection.CreateModel();

channel.QueueDeclare("product", durable: true, exclusive: false, autoDelete: false);

var consumer= new EventingBasicConsumer(channel);

consumer.Received += async (model, eventArgs) => {
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var ProductList = JsonConvert.DeserializeObject<List<Product>>(message);
    if (ProductList != null)
        await SaveProduct(ProductList, configuration);
    Console.WriteLine($"Product message received: {message}");
};

channel.BasicConsume("product", autoAck: true, consumer: consumer);
Console.ReadKey();


//Consumer Actions defined here
async Task<bool> SaveProduct(List<Product> products,IConfiguration configuration)
{
        

    //using (var context =new DBContextClass(configuration))
    //{
        foreach (var product in products)
        {
            try
            {
                productService.AddProduct(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            
        }
    //    context.SaveChanges();
    //}
    return true;
};