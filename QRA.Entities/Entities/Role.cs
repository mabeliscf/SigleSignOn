using System;
using System.Collections.Generic;

namespace QRA.Entities.Entities
{
    public partial class Role
    {
        public long IdRole { get; set; }
        public string RoleDescription { get; set; } = null!;
        public long RoleFather { get; set; }
    }
}
