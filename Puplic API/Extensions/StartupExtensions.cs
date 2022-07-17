
using Application.Core.Interfaces.Reposotories;
using Application.Core.Interfaces.Services;
using Application.Core.Services;
using Application.Infrastructure.Data;
using Application.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Puplic_API.Extensions
{
    public static class StartupExtensions
    {
        public static void AddReporitories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();
        }

        public static void AddDbContextConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                configuration.GetConnectionString("DbConnection")));
        }

        public static async Task ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}
