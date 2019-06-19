using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AngelCo.Domain
{
    public class DesignTimeDomainContextFactory : IDesignTimeDbContextFactory<DomainContext>
    {
        public DomainContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DomainContext>();

            var connectionString = configuration.GetConnectionString("Database");

            builder.UseSqlServer(connectionString);

            return new DomainContext(builder.Options);
        }
    }
}
