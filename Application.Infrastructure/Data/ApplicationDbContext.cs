using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Application.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {

        #region Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        #endregion

        #region DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
