using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QRA.Entities.contracts;

namespace QRA.Persistence.Services
{

    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dBContext;


        public GenericRepository(DbContext dBContext)
        {
            _dBContext = dBContext;
        }



        #region GET

        public bool Any(Expression<Func<TEntity, bool>> query = null)
        {
            bool result = (query != null) ?
                _dBContext.Set<TEntity>().Any(query) :
                _dBContext.Set<TEntity>().Any();

            return result;
        }

        public int Count(Expression<Func<TEntity, bool>> query)
        {
            return _dBContext.Set<TEntity>().Where(query).Count();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> query)
        {
            return _dBContext.Set<TEntity>().FirstOrDefault(query);
        }

        public IQueryable<TEntity> GetDataSet()
        {
            return _dBContext.Set<TEntity>();
        }

        public List<TEntity> Query(Expression<Func<TEntity, bool>> query,
            Expression<Func<TEntity, object>> orderBy = null,
            ListSortDirection sortDirection = ListSortDirection.Ascending,
            int? pageNumber = null,
            int? pageSize = null)
        {

            IQueryable<TEntity> context = _dBContext.Set<TEntity>().Where(query);

            if (orderBy != null)
            {
                if (sortDirection == ListSortDirection.Ascending)
                    context = context.OrderBy(orderBy);
                else
                    context = context.OrderByDescending(orderBy);
            }

            int take = pageSize ?? 50;
            int skip = (pageSize != null && pageNumber != null) ? pageSize.Value * (pageNumber.Value - 1) : 0;

            context = context.Skip(skip).Take(take);

            return context.ToList();
        }

        #endregion

        #region POST
        public void Add(TEntity entity)
        {
            _dBContext.Set<TEntity>().Add(entity);
        }

        public void AddGroup(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                _dBContext.Set<TEntity>().Add(entity);
        }

        #endregion

        #region Delete

        public void Delete(TEntity entity)
        {
            _dBContext.Set<TEntity>().Remove(entity);
        }

        public void DeleteGroup(Expression<Func<TEntity, bool>> query)
        {
            IQueryable<TEntity> entities = _dBContext.Set<TEntity>().Where(query);

            foreach (TEntity entity in entities)
                _dBContext.Set<TEntity>().Remove(entity);
        }

        #endregion
    }
}
