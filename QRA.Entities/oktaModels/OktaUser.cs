using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.Entities.oktaModels
{
    public  class OktaUser
    {
       
        public Profile profile { get; set; } = null!;
        public Credentials credentials { get; set; } = null!;

    }
     public class Profile
    {
        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public string email { get; set; } = null!;
        public string login { get; set; } = null!;
        public string mobilePhone { get; set; } = null!;
    }


    public class Credentials
    {
        public Password password { get; set; } = null!;
    }
    public class Password
    {
        public string value { get; set; } = null!;
    }
}
