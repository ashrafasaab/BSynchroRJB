using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Infrastructure.Data.Config
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasOne(x => x.Account)
                .WithOne(x => x.Customer)
                .HasForeignKey<Account>(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
