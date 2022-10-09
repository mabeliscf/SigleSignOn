using System;
using System.Collections.Generic;

namespace QRA.Entities.Entities
{
    public partial class TenantsLogin
    {
        public long IdTenantLogin { get; set; }
        public long IdTenant { get; set; }
        public byte[] PasswordEncrypted { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public int LoginType { get; set; }
        public bool Administrator { get; set; } = false!;
        public long? TenantFather { get; set; } 
    }
}
