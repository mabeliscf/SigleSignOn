using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.Entities.Models
{
   

    public class UserBasics
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int LoginType { get; set; }
        public bool isTenant { get; set; }
        public bool isUser { get; set; }
        public int TenantFather { get; set; }


        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }

        public bool IsAdmin { get; set; }

    }
}
