using System;
using System.Collections.Generic;

namespace QRA.Entities.Entities
{
    public partial class TenantsRole
    {
        public long IdTenantRole { get; set; }
        public long IdTenant { get; set; }
        public string RoleDescription { get; set; } = null!;
        public long RoleFather { get; set; }

        public virtual Tenant IdTenantNavigation { get; set; } = null!;
    }
}
