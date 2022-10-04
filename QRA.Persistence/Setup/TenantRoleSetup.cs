using Microsoft.EntityFrameworkCore;
using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.Persistence.Setup
{
    public static  class TenantsRoleSetup
    {

        public static void ConfigureTenantsRole(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TenantsRole>(entity =>
            {
                entity.HasKey(e => e.IdTenantRole)
                    .HasName("PK__tenantsR__0A37AE68228C114B");

                entity.ToTable("tenantsRole", "qra");

                entity.Property(e => e.IdTenantRole).HasColumnName("idTenantRole");

                entity.Property(e => e.IdTenant).HasColumnName("idTenant");

                entity.Property(e => e.RoleDescription)
                    .HasMaxLength(100)
                    .HasColumnName("roleDescription");

                entity.Property(e => e.RoleFather).HasColumnName("roleFather");

                entity.HasOne(d => d.IdTenantNavigation)
                    .WithMany(p => p.TenantsRoles)
                    .HasForeignKey(d => d.IdTenant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tenantsRo__idTen__2D27B809");
            });
        }

    }
}
