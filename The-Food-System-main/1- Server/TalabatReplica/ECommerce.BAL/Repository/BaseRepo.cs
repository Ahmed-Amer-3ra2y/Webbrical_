using ECommerce.DAL.Reposatory.Repo;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.BAL.Repository
{
    public class BaseRepo<T> : IAsyncRepository<T> where T : class
    {
        #region Injecting DbContext
        private readonly ApplicationDbContext context;

        public BaseRepo( ApplicationDbContext context )
        {
            this.context = context;
        }
        #endregion

        #region public methods
        public async Task AddAsync( T entity )
        {
            await context.AddAsync( entity );
            await context.SaveChangesAsync( );
        }
        public async Task<int> CountAllAsync( ) => await context.Set<T>( ).CountAsync( );
        public async Task<int> CountWhereAsync( Expression<Func<T , bool>> predicate ) => await context.Set<T>( ).CountAsync( predicate );
        public async Task<T> FirstOrDefaultAsync( Expression<Func<T , bool>> where , params Expression<Func<T , object>>[ ] includeProperties )
        {
            var data = await GetWhereAsync( where , includeProperties );
            return data.FirstOrDefault( );
        }
        public async Task<IEnumerable<T>> GetAllAsync( ) => await context.Set<T>( ).ToListAsync( );
        public async Task<T> GetByIdAsync( int id ) => await context.Set<T>( ).FindAsync( id );
        public async Task<IEnumerable<T>> GetWhereAsync( Expression<Func<T , bool>> predicate , params Expression<Func<T , object>>[ ] includeProperties )
        {
            var query = predicate == null ? context.Set<T>( ) : context.Set<T>( ).Where( predicate );
            var entities = includeProperties.Aggregate( query , ( current , includeProperty ) =>
                current.Include( includeProperty ) );
            return await entities.ToListAsync( );
        }
        public async Task RemoveAsync( T entity )
        {
            context.Set<T>( ).Remove( entity );
            await context.SaveChangesAsync( );
        }
        public async Task UpdateAsync( T entity )
        {
            // In case AsNoTracking is used
            context.Entry( entity ).State = EntityState.Modified;
            await context.SaveChangesAsync( );
        }


        #endregion
    }
}
