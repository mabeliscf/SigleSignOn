using QRA.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.contracts
{
    public interface IUserQuery
    {
        AdminInfo GetAdminbyId(long id);
        AdminInfo GetAdminWTenants(long id);
        List<TenantInfo> GetAllTenants();
        TenantInfo GetTenatInfobyId(long id);
        UserInfo GetUserInfoByID(long idUser);
        List<UserInfo> GetUsersbyTenantID(long id);
        bool GetAllAdmins();
    }
}
