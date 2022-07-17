using Application.Core.Entities;
using Application.Core.Interfaces.Reposotories;
using Application.Core.Interfaces.Services;

namespace Application.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            var newCutomer = await _unitOfWork.Customers.CreateAsync(customer, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);
            return newCutomer;
        }

        public async Task DeleteAsync(Customer entity, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Customers.DeleteAsync(entity);
            await _unitOfWork.SaveChanges(cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Customers.GetAllAsync(cancellationToken);
        }

        public Task<Customer> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _unitOfWork.Customers.GetByIdAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.Customers.UpdateAsync(customer);
            await _unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
