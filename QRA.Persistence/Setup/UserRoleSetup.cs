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
                    .HasName("PK__userRole__2E1B15A54D9D2C2A");

                entity.ToTable("userRole", "qra");

                entity.Property(e => e.IdUserRole).HasColumnName("idUserRole");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.RoleDescription)
                    .HasMaxLength(100)
                    .HasColumnName("roleDescription");

                entity.Property(e => e.RoleFather).HasColumnName("roleFather");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__userRole__idUser__38996AB5");
            });

        }

    }
}
