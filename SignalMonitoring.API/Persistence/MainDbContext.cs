using Microsoft.EntityFrameworkCore;
using System.Configuration;

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
            optionsBuilder.UseSqlServer(@"Server = QGJHV6Y2\SQLEXPRESS; Initial catalog = FG; Trusted_Connection=True;");
        }

        public DbSet<SignalDataModel> Signals { get; set; }

        public DbSet<TermsModel> Terms { get; set; }
    }
}
