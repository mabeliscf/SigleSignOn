using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.Entities.oktaModels
{
    public class oktaGroup
    {
        public ProfileGroup profile { get; set; }

    }
    public class ProfileGroup {
        public string name { get; set; }
        public string description { get; set; }


    }

    public class OktaGroupResponse
    {
        public string id { get; set; }
        public string created { get; set; }
        public string lastUpdated { get; set; }
        public string type { get; set; }
        public ProfileGroup profile { get; set; }
    }




}
