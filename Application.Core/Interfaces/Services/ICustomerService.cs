using Application.Core.Entities;

namespace Application.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(Customer entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Customer> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
