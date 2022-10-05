using System;
using System.Collections.Generic;

namespace QRA.Entities.Entities
{
    public partial class TenantsRole
    {
        public long IdTenantRole { get; set; }
        public long IdTenant { get; set; }
        public long IdRole { get; set; }
    }
}
