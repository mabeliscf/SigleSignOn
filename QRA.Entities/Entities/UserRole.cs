using System;
using System.Collections.Generic;

namespace QRA.Entities.Entities
{
    public partial class UserRole
    {
        public long IdUserRole { get; set; }
        public long IdUser { get; set; }
        public long IdRole { get; set; }
    }
}
