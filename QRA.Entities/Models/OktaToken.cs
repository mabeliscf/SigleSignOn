using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.Entities.Models
{
    public  class OktaToken
    {

        public string Token_type { get; set; }
        public string Expires_in { get; set; }
        public string Access_token { get; set; }
        public string Scope { get; set; }


    }

    public class tokenRequest
    {
        public string Grant_type { get; set; }
        public string Scope { get; set; }
    }
}
