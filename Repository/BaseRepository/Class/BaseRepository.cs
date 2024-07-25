using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestDtoInApi.DataContext;
using TestDtoInApi.Repository.BaseRepository.Interface;

namespace TestDtoInApi.Repository.BaseRepository.Class
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly MyDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public BaseRepository(MyDbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            // Attach the entity if it's not already tracked
            _entities.Attach(entity);

            // Mark the entity as Modified
            _context.Entry(entity).State = EntityState.Modified;

            // Save Changes Asynchronously
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                // Remove the Entity
                _entities.Remove(entity);

                // Save Changes Asynchronously
                int result = await _context.SaveChangesAsync();

                // Check if the deletion was successful
                return result > 0;
            }
            catch (Exception)
            {
                // Log the exception if needed
                return false;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _entities;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }
    }
}
