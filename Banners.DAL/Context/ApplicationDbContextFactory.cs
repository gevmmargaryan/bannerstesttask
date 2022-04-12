using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Banners.DAL.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            /*IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();*/

            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();

            //string connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=Banners;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new ApplicationDbContext(optionsBuilder.Options);
        }

    }
}
