using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.DataContext
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(){}
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options){}

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("TrongConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }  
        }
    } 
}