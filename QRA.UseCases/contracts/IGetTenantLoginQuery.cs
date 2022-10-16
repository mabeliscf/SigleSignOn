using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.contracts
{
    public interface IGetTenantLoginQuery
    {
        long getTenantFather(long id);
        bool isAdmin(long id);
        bool adminExist();
        TenantsLogin GetFather(long id);
        TenantsLogin getLoginbyID(long id);
    }
}
