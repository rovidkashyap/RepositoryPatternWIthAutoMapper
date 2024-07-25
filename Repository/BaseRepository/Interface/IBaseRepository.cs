using System.Linq.Expressions;

namespace TestDtoInApi.Repository.BaseRepository.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        // This Repository will output the result with include the properties you mentioned
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task SaveChangesAsync();
    }
}
