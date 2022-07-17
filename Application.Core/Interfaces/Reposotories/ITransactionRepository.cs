using Application.Core.Entities;

namespace Application.Core.Interfaces.Reposotories
{
    public interface ITransactionRepository : IAsyncRepository<Transaction>
    {
        public Task<decimal> GetCreditBalanceByAccountIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
