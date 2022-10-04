using System;
using System.Collections.Generic;

namespace QRA.Entities.Entities
{
    public partial class Tenant
    {
        public Tenant()
        {
            DbAccesses = new HashSet<DbAccess>();
            TenantsLogins = new HashSet<TenantsLogin>();
            TenantsRoles = new HashSet<TenantsRole>();
            Users = new HashSet<User>();
        }

        public long IdTenant { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual ICollection<DbAccess> DbAccesses { get; set; }
        public virtual ICollection<TenantsLogin> TenantsLogins { get; set; }
        public virtual ICollection<TenantsRole> TenantsRoles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
