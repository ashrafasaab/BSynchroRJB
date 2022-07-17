using Application.Core.Entities;
using Application.Core.Interfaces.Reposotories;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Data.Repositories
{
    public class EfRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<TEntity> Table => _context.Set<TEntity>().AsQueryable<TEntity>();

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(entity, cancellationToken);
            return entity;
        }

        public Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            return Task.CompletedTask;
        }


        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().Where(x => true).ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default)
        {
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().Where(x => x.ID == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(IQueryable<TEntity> query, CancellationToken cancellationToken = default)
        {
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, string[] exceptedFields)
        {
            _context.Attach(entity);
            foreach (var exceptedField in exceptedFields)
            {
                _context.Entry(entity).Property(exceptedField).IsModified = false;
            }
        }
    }
}
