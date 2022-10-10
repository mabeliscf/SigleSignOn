using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.DTOs
{
    public  class RegisterUserDTO : RegisterDTO
    {
        //if tenant
        public List<DbAccess> Databases { get; set; }

        //if user or tenant
        public List<TenantsRole> Roles { get; set; }

        //if user
        public long IdTenantFather { get; set; }


    }
}
