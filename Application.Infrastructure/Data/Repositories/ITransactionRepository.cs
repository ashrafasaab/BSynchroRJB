using Application.Core.Entities;
using Application.Core.Interfaces.Reposotories;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Data.Repositories
{
    public class TransactionRepository : EfRepository<Transaction>, ITransactionRepository
    {

        public TransactionRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<decimal> GetCreditBalanceByAccountIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var getCreditBalanceByAccountIdQuery = Table
                .Where(x => x.AccountId == id);

            return await getCreditBalanceByAccountIdQuery.SumAsync(x => x.Credit);
        }
    }
}
