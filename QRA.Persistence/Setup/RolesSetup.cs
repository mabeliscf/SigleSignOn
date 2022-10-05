using Microsoft.EntityFrameworkCore;
using QRA.Entities.Entities;

namespace QRA.Persistence.Setup
{
  
        public static class RolesSetup
        {

        public static void ConfigureRole(this ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole)
                .HasName("PK__roles__E5045C544DABE94E");

            entity.ToTable("roles", "qra");

            entity.Property(e => e.IdRole).HasColumnName("idRole");

            entity.Property(e => e.RoleDescription)
                            .HasMaxLength(100)
                            .HasColumnName("roleDescription");

            entity.Property(e => e.RoleFather).HasColumnName("roleFather");
        });
        }
    }
}
