using System;
using System.Collections.Generic;

namespace QRA.Entities.Entities
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public long IdUser { get; set; }
        public long IdTenant { get; set; }
        public string Fullname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public virtual Tenant IdTenantNavigation { get; set; } = null!;
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
