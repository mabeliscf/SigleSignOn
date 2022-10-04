using System;
using System.Collections.Generic;

namespace QRA.Entities.Entities
{
    public partial class UserRole
    {
        public long IdUserRole { get; set; }
        public long IdUser { get; set; }
        public string RoleDescription { get; set; } = null!;
        public long RoleFather { get; set; }

        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
