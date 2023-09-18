using System.Linq.Expressions;

namespace ECommerce.DAL.Reposatory.Repo
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync( int id );
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync( T entity );
        Task UpdateAsync( T entity );
        Task RemoveAsync( T entity );

        Task<IEnumerable<T>> GetAllAsync( );
        Task<IEnumerable<T>> GetWhereAsync( Expression<Func<T , bool>> predicate , params Expression<Func<T , object>>[ ] includeProperties );

        Task<int> CountAllAsync( );
        Task<int> CountWhereAsync( Expression<Func<T , bool>> predicate );
    }
}
