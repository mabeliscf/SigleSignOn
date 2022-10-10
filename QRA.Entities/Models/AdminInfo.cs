using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.Entities.Models
{


    public class AdminInfo : UserBasics
    {

        public List<TenantInfo> Tenants { get; set; }


    }
}
