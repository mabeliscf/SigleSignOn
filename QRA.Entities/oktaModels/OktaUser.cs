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
        public string FirstName  { get; set; } = null!;
        public string LastName  { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string MobilePhone { get; set; } = null!;
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
