using Application.Core.Entities;

namespace Application.Core.Interfaces.Reposotories
{
    public interface IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default);
        Task<TEntity> GetFirstOrDefaultAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);


        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity,string[] ExceptedFields);
        Task DeleteAsync(TEntity entity);

        IQueryable<TEntity> Table { get; }
    }
}
