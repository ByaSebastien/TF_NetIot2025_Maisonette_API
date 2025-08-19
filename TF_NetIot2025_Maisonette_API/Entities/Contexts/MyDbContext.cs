using Microsoft.EntityFrameworkCore;

namespace TF_NetIot2025_Maisonette_API.Entities.Contexts
{
    public class MyDbContext: DbContext
    {
        public DbSet<HouseState> HouseStates { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);
        }
    }
}
