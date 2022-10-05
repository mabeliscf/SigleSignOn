using QRA.Entities.contracts;

namespace QRA.API.Factory
{
    public class RepositoryFactory //: IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

     
        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

      
        //public IGenericRepository<TEntity> Make<TEntity>(DbContexts type) where TEntity : class, IEntityBase
        //{
        //    IEnumerable<IGenericRepository<TEntity>> services = _serviceProvider.GetServices<IGenericRepository<TEntity>>();

        //    return services.First(service => service.DbContextType == type);
        //}
    }
}
