using QRA.Entities.Entities;
using QRA.Persistence;
using QRA.UseCases.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.Queries
{
    public  class GetRolesQuery : IGetRolesQuery
    {

        private QRAchallengeContext _context;

        public GetRolesQuery(QRAchallengeContext qR)
        {
            _context = qR;
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public Role GetRolesbyID(long id)
        {
            return _context.Roles.Where(a=> a.IdRole==id).FirstOrDefault();
        }

        public List<Role> GetRolesbyUser(long id)
        {
            var roles = (from a in _context.TenantsRoles
                         join b in _context.Roles
                         on a.IdRole equals b.IdRole
                         where a.IdTenant == id
                         select b).ToList();

            return roles;
        }
    }
}
