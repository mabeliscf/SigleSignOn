using Microsoft.EntityFrameworkCore;
using QRA.Entities.Entities;


namespace QRA.Persistence.Setup
{
    public static  class DbAccessSetup
    {

        public static void ConfigureDbAccess(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbAccess>(entity =>
            {
                entity.HasKey(e => e.IdDbAccess)
                    .HasName("PK__dbAccess__0C5028FB40212340");

                entity.ToTable("dbAccess", "qra");

                entity.Property(e => e.IdDbAccess).HasColumnName("idDbAccess");

                entity.Property(e => e.IdDb).HasColumnName("idDB");

                entity.Property(e => e.IdTenant).HasColumnName("idTenant");

                entity.HasOne(d => d.IdDbNavigation)
                    .WithMany(p => p.DbAccesses)
                    .HasForeignKey(d => d.IdDb)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__dbAccess__idDB__32E0915F");

                //entity.HasOne(d => d.IdTenantNavigation)
                //    .WithMany(p => p.DbAccesses)
                //    .HasForeignKey(d => d.IdTenant)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__dbAccess__idTena__31EC6D26");
            });

        }

    }
}
