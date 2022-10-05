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
                    .HasName("PK__tenantsR__0A37AE6812EC312D");

                entity.ToTable("tenantsRole", "qra");

                entity.Property(e => e.IdTenantRole).HasColumnName("idTenantRole");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.IdTenant).HasColumnName("idTenant");
            });

        }

    }
}
