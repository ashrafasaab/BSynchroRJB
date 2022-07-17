using Application.Core.Entities;
using Application.Core.Interfaces.Reposotories;
using Application.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly Lazy<IAsyncRepository<Customer>> CustomersLazy;
        private readonly Lazy<IAsyncRepository<Account>> AccountsLazy;
        private readonly Lazy<ITransactionRepository> TransactionsLazy;

        public IAsyncRepository<Customer> Customers => CustomersLazy.Value;
        public IAsyncRepository<Account> Accounts => AccountsLazy.Value;
        public ITransactionRepository Transactions => TransactionsLazy.Value;

        public readonly ApplicationDbContext _context;

        private IDbContextTransaction _objTran;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            CustomersLazy = new Lazy<IAsyncRepository<Customer>>(() => new EfRepository<Customer>(_context));
            AccountsLazy = new Lazy<IAsyncRepository<Account>>(() => new EfRepository<Account>(_context));
            TransactionsLazy = new Lazy<ITransactionRepository>(() => new TransactionRepository(_context));
        }


        public void CreateTransaction()
        {
            _objTran = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _objTran.Commit();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Rollback()
        {
            _objTran.Rollback();
        }

        public async Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
