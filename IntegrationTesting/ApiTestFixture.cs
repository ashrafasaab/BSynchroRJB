using Application.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace IntegrationTesting
{
    public class ApiTestFixture : WebApplicationFactory<Program>
    {
        public string SqlConnectionForTestin =>
            "Server=.\\SQLEXPRESS;Database=BsynchroRjpDbTest;Trusted_Connection=True";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(SqlConnectionForTestin);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                    //var logger = scopedServices
                    //    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    //db.Database.EnsureCreated();
                }
            });
        }
    }
}
