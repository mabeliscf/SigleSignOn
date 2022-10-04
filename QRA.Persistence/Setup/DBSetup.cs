using Microsoft.EntityFrameworkCore;
using QRA.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRA.Persistence.Setup
{
    public static class DBSetup
    {

        public static void ConfigureDB(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Db>(entity =>
            {
                entity.HasKey(e => e.IdDb)
                    .HasName("PK__db__9DB8AE41C2390F75");

                entity.ToTable("db", "qra");

                entity.Property(e => e.IdDb).HasColumnName("idDB");

                entity.Property(e => e.DbName)
                    .HasMaxLength(50)
                    .HasColumnName("dbName");

                entity.Property(e => e.DbSchema)
                    .HasMaxLength(10)
                    .HasColumnName("dbSchema");

                entity.Property(e => e.ServerName)
                    .HasMaxLength(50)
                    .HasColumnName("serverName");

                entity.Property(e => e.ServerRoute)
                    .HasMaxLength(100)
                    .HasColumnName("serverRoute");
            });
        }

    }
}
