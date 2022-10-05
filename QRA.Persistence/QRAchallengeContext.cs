using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using QRA.Entities.Entities;
using QRA.Persistence.Setup;

namespace QRA.Persistence
{
    public partial class QRAchallengeContext : DbContext
    {
        private readonly string _connectionString;
        private readonly int _timeout;
        public QRAchallengeContext()
        {
        }

        public QRAchallengeContext(DbContextOptions<QRAchallengeContext> options)
            : base(options)
        {
        }

        public QRAchallengeContext(string connectionString, int timeout)
        {
            _connectionString = connectionString;
            _timeout = timeout;
        }
        #region properties
        public virtual DbSet<Db> Dbs { get; set; } = null!;
        public virtual DbSet<DbAccess> DbAccesses { get; set; } = null!;
        public virtual DbSet<Tenant> Tenants { get; set; } = null!;
        public virtual DbSet<TenantsLogin> TenantsLogins { get; set; } = null!;
        public virtual DbSet<TenantsRole> TenantsRoles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;


        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            optionsBuilder.UseSqlServer(_connectionString, sql => sql.CommandTimeout(_timeout))
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureTenant();
            modelBuilder.ConfigureDB();
            modelBuilder.ConfigureDbAccess();
            modelBuilder.ConfigureTenantsLogin();
            modelBuilder.ConfigureTenantsRole();
            modelBuilder.ConfigureUser();
            modelBuilder.ConfigureUserRole();
            modelBuilder.ConfigureRole();


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
