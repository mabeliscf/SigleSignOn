
using QRA.Entities.Entities;
using QRA.Persistence;
using QRA.UseCases.contracts;

namespace QRA.UseCases.Queries
{
    public class GetTenantQuery : IGetTenantQuery
    {
        private QRAchallengeContext _context;

        public GetTenantQuery(QRAchallengeContext qR)
        {
            _context = qR;
        }

        /// <summary>
        /// given and id returns basic user info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tenant GetTenantbyId(long id)
        {
            return _context.Tenants.Where(a => a.IdTenant == id).FirstOrDefault();
        }
       

        /// <summary>
        /// return all tenants, no user or admin
        /// </summary>
        /// <returns></returns>
        public List<Tenant> GetAllTetenants()
        {
            return (from a in _context.Tenants
                    join b in _context.TenantsLogins
                    on a.IdTenant equals b.IdTenant
                    where b.Administrator == false
                    && b.TenantFather == null || b.TenantFather == 0
                    select a).ToList();
        }

        public List<Tenant> GetUsersofTenant(long id)
        {
            return (from a in _context.TenantsLogins
                                  join b in _context.Tenants
                                  on a.IdTenant equals b.IdTenant
                                  where a.TenantFather == id
                                  select b).ToList();
        }
    }
}
