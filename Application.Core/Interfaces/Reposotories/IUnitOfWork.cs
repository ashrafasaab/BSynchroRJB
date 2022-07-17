using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.Interfaces.Reposotories
{
    public interface IUnitOfWork : IDisposable
    {
        IAsyncRepository<Customer> Customers { get; }
        IAsyncRepository<Account> Accounts { get; }
        ITransactionRepository Transactions { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
