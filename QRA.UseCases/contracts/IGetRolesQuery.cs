using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.contracts
{
    public interface IGetRolesQuery
    {
        List<Role> GetRoles();
        Role GetRolesbyID(long id);
        List<Role> GetRolesbyUser(long id);
    }
}
