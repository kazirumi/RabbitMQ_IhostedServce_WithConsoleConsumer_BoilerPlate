using Microsoft.EntityFrameworkCore;
using RabbitMQandIHostedService.Models;

namespace RabbitMQandIHostedService.Data
{
    public class DBContextClass:DbContext
    {
        protected readonly IConfiguration Configuration;
        public DBContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Product> Products { get; set;}
    }
}
