using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.Entities.Models
{
    public  class UserInfo: UserBasics
    {
      
        public long IdTenantFather { get; set; }

        public List<Role> Roles { get; set; }
        public List<Db> Databases { get; set; }

      

    }

   

}
