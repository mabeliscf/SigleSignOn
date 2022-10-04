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
                    .HasName("PK__tenantsL__80D605B2472D7D70");

                entity.ToTable("tenantsLogin", "qra");

                entity.Property(e => e.IdTenantLogin).HasColumnName("idTenantLogin");

                entity.Property(e => e.IdTenant).HasColumnName("idTenant");

                entity.Property(e => e.LoginType).HasColumnName("loginType");

                entity.Property(e => e.PasswordEncrypted)
                    .HasMaxLength(50)
                    .HasColumnName("passwordEncrypted");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .HasColumnName("token");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.IdTenantNavigation)
                    .WithMany(p => p.TenantsLogins)
                    .HasForeignKey(d => d.IdTenant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tenantsLo__token__2A4B4B5E");
            });
        }

    }
}
