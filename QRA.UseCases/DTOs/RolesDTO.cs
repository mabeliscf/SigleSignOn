using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.UseCases.DTOs
{
    public  class RolesDTO
    {
        public long IdRole { get; set; }
        public string RoleDescription { get; set; } = null!;
        public long RoleFather { get; set; }
    }
}
