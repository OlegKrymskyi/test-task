using Microsoft.EntityFrameworkCore;

namespace AngelCo.Domain
{
    public class DomainContext: DbContext
    {
        public DomainContext(DbContextOptions<DomainContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Education> Educations { get; set; }

        public DbSet<Experience> Experiences { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Market> Markets { get; set; }
    }
}
