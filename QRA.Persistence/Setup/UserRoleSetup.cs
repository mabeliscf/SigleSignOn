using Microsoft.EntityFrameworkCore;
using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.Persistence.Setup
{
    public static  class UserRoleSetup
    {

        public static void ConfigureUserRole(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.IdUserRole)
                    .HasName("PK__userRole__2E1B15A5AE073CE3");

                entity.ToTable("userRole", "qra");

                entity.Property(e => e.IdUserRole).HasColumnName("idUserRole");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.IdUser).HasColumnName("idUser");
            });
        }

    }
}
