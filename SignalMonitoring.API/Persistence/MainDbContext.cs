using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace SignalMonitoring.API.Persistence
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            
        }

        public DbSet<SignalDataModel> Signals { get; set; }
    }
}
