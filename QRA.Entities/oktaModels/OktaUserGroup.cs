using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.Entities.oktaModels
{
    public class OktaUserGroup : OktaUser
    {
        public string[] groupIds { get; set; }    

    }
}
