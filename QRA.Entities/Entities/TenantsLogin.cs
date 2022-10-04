using System;
using System.Collections.Generic;

namespace QRA.Entities.Entities
{
    public partial class TenantsLogin
    {
        public long IdTenantLogin { get; set; }
        public long IdTenant { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordEncrypted { get; set; } = null!;
        public int LoginType { get; set; }
        public string? Token { get; set; }

        public virtual Tenant IdTenantNavigation { get; set; } = null!;
    }
}
