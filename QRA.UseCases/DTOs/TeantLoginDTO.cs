using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.DTOs
{
    public  class TeantLoginDTO
    {
        public long IdTenant { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }

    }
}
