using Application.Core.Entities;

namespace Application.Core.Interfaces.Services
{
    public interface ITransactionService
    {
        /// <summary>
        /// Create a new transaction and update the account balance of it
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Transaction> CreateAsync(Transaction transaction, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a transaction and update the account balance of it
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(Transaction entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get transaction by account id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Transaction>> GetByAccountIdAsync(int id, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Get a transaction by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Transaction> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
