using Microsoft.EntityFrameworkCore;
using SekChallenge.API.Infra.Mappings;

namespace SekChallenge.API.Infra
{
    public class SekContext : DbContext
    {
        protected SekContext() { }

        public SekContext(DbContextOptions<SekContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ScanMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
