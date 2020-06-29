using Microsoft.EntityFrameworkCore;

namespace SignalMonitoring.API.Persistence
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        public DbSet<SignalDataModel> Signals { get; set; }

        public DbSet<TermsModel> Terms { get; set; }
    }
}
