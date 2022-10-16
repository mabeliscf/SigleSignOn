using System;
using System.Collections.Generic;

namespace QRA.Entities.Entities
{
    public partial class Tenant
    {
       

        public long IdTenant { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Username { get; set; } = null!;

        public string TenantSpaceID { get; set; } = null!;

    }
}
