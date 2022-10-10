using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.contracts
{
    public  interface IRolesService
    {
        long create(Role role);
        bool update(Role role);
        bool delete(Role role);
    }

}
