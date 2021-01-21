using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SignalMonitoring.API.Persistence
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
        {
            
        }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();

            var connectionString = configuration.GetSection("ConnectionString").Value;
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<SignalDataModel> Signals { get; set; }

        public DbSet<TermsModel> Terms { get; set; }
    }
}
