using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.contracts
{
    public interface IGetTenantQuery
    {
        List<Tenant> GetAllTetenants();
        Tenant GetTenantbyId(long id);
        List<Tenant> GetUsersofTenant(long id);
        bool UserExist(string email);
    }
}
