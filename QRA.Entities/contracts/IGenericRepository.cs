using System.ComponentModel;
using System.Linq.Expressions;

namespace QRA.Entities.contracts
{
    public interface IGenericRepository<TEntity>
    {
        void Add(TEntity entity);
        void AddGroup(IEnumerable<TEntity> entities);
        bool Any(Expression<Func<TEntity, bool>> query = null);
        int Count(Expression<Func<TEntity, bool>> query);
        void Delete(TEntity entity);
        void DeleteGroup(Expression<Func<TEntity, bool>> query);
        TEntity Get(Expression<Func<TEntity, bool>> query);
        IQueryable<TEntity> GetDataSet();
        List<TEntity> Query(Expression<Func<TEntity, bool>> query, Expression<Func<TEntity, object>> orderBy = null, ListSortDirection sortDirection = ListSortDirection.Ascending, int? pageNumber = null, int? pageSize = null);
    }
}