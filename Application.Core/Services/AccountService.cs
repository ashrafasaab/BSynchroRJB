using Application.Core.Entities;
using Application.Core.Interfaces.Reposotories;
using Application.Core.Interfaces.Services;

namespace Application.Core.Services
{
    public class AccountService : IAccountService
    {

        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Account> CreateAsync(Account account, CancellationToken cancellationToken = default)
        {
            _unitOfWork.CreateTransaction();

            var newAccount = await _unitOfWork.Accounts.CreateAsync(account, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);

            if (account.CreditBalance != 0)
            {
                var transaction = new Transaction(newAccount.ID, account.CreditBalance)
                {
                    CreationDate = DateTime.UtcNow,
                };

                await _unitOfWork.Transactions.CreateAsync(transaction);
                await _unitOfWork.SaveChanges(cancellationToken);
            }

            _unitOfWork.Commit();

            return newAccount;
        }

        public async Task<Account> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Accounts.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Account> GetByCustomerIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var getByCustomerIdQuery = _unitOfWork.Accounts.Table
                .Where(x => x.CustomerId == id);

            return await _unitOfWork.Accounts.GetFirstOrDefaultAsync(getByCustomerIdQuery, cancellationToken);
        }

        public async Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Accounts.UpdateAsync(account, new[] { "CreditBalance", "CreationDate" });
            await _unitOfWork.SaveChanges(cancellationToken);
        }

        public async Task<decimal> AddToCreditBalance(int accountId, decimal value, CancellationToken cancellationToken = default)
        {
            var account = await _unitOfWork.Accounts.GetByIdAsync(accountId, cancellationToken);
            account.CreditBalance += value;
            await _unitOfWork.Accounts.UpdateAsync(account);
            await _unitOfWork.SaveChanges(cancellationToken);
            return account.CreditBalance;
        }

    }
}
