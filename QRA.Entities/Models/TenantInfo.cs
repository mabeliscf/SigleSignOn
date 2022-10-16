using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.Entities.Models
{
    public class TenantInfo : UserBasics
    {


        public List<Role> Roles { get; set; }
        public List<Db> Database { get; set; }

        public List<UserInfo> Users { get; set; }
    }
}
