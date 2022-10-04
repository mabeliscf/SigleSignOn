using System;
using System.Collections.Generic;

namespace QRA.Entities.Entities
{
    public partial class Db
    {
        public Db()
        {
            DbAccesses = new HashSet<DbAccess>();
        }

        public long IdDb { get; set; }
        public string DbSchema { get; set; } = null!;
        public string DbName { get; set; } = null!;
        public string ServerName { get; set; } = null!;
        public string ServerRoute { get; set; } = null!;

        public virtual ICollection<DbAccess> DbAccesses { get; set; }
    }
}
