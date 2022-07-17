using Application.Core.Entities;
using Application.Core.Interfaces.Reposotories;
using Application.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService AccountService;

        public TransactionService(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            AccountService = accountService;
        }

        public async Task<Transaction> CreateAsync(Transaction transaction, CancellationToken cancellationToken = default)
        {
            _unitOfWork.CreateTransaction();

            var newTransaction = await _unitOfWork.Transactions.CreateAsync(transaction, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);

            await AccountService.AddToCreditBalance(newTransaction.AccountId, newTransaction.Credit);

            _unitOfWork.Commit();

            return newTransaction;
        }

        public async Task DeleteAsync(Transaction entity, CancellationToken cancellationToken = default)
        {
            _unitOfWork.CreateTransaction();

            var transaction = await _unitOfWork.Transactions.GetByIdAsync(entity.ID);

            await _unitOfWork.Transactions.DeleteAsync(entity);
            await _unitOfWork.SaveChanges(cancellationToken);

            await AccountService.AddToCreditBalance(transaction.AccountId, transaction.Credit * -1);

            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var getbyAccountIdQuery = _unitOfWork.Transactions.Table
                .Where(t => t.AccountId == id);

            return await _unitOfWork.Transactions.GetAsync(getbyAccountIdQuery, cancellationToken);
        }

        public async Task<Transaction> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Transactions.GetByIdAsync(id, cancellationToken);
        }
    }
}
