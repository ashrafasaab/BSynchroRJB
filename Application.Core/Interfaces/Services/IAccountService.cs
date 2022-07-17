using Application.Core.Entities;

namespace Application.Core.Interfaces.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Create a new account with an initial credit balance
        /// </summary>
        /// <param name="account"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Account> CreateAsync(Account account, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update Account information without the ability to change the creidit balance;
        /// </summary>
        /// <param name="account"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(Account account, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Account> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Get By Customer Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Account> GetByCustomerIdAsync(int id, CancellationToken cancellationToken = default);
        /// <summary>
        /// Add a value the Account Credit Balance
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<decimal> AddToCreditBalance(int id, decimal value, CancellationToken cancellationToken = default);
    }
}
