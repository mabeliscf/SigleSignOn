using Microsoft.EntityFrameworkCore;
using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.Persistence.Setup
{
    public static  class TenantsLoginSetup
    {

        public static void ConfigureTenantsLogin(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TenantsLogin>(entity =>
            {
                entity.HasKey(e => e.IdTenantLogin)
                    .HasName("PK__tenantsL__80D605B21C8A66BE");

                entity.ToTable("tenantsLogin", "qra");

                entity.Property(e => e.IdTenantLogin).HasColumnName("idTenantLogin");

                entity.Property(e => e.IdTenant).HasColumnName("idTenant");

                entity.Property(e => e.LoginType).HasColumnName("loginType");

                entity.Property(e => e.PasswordEncrypted).HasColumnName("passwordEncrypted");

                entity.Property(e => e.PasswordSalt).HasColumnName("passwordSalt");
            });

        }

    }
}
