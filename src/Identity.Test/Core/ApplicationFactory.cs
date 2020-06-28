using Identity.Model.Database.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Identity.Test.Core
{
    public class ApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var databaseService = services.SingleOrDefault(x => x.ServiceType == typeof(DatabaseContext));
                var options = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<DatabaseContext>));
                if (databaseService != null)
                {
                    services.Remove(databaseService);
                    services.Remove(options);
                }

                services.AddDbContext<DatabaseContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                }, ServiceLifetime.Transient);

                var sp = services.BuildServiceProvider();
                DatabaseContext.SeedData(sp);
            });
        }
    }
}
