using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Prototype.OpenTelematics.DataAccess
{
    public partial class TelematicsContext : DbContext
    {
        public TelematicsContext()
        {
        }

        public TelematicsContext(DbContextOptions<TelematicsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<DriverBreakRule> DriverBreakRule { get; set; }
        public virtual DbSet<DriverPerformanceSummary> DriverPerformanceSummary { get; set; }
        public virtual DbSet<DriverWaiver> DriverWaiver { get; set; }

        public virtual DbSet<Vehicle> Vehicle { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.country).HasMaxLength(50);

                entity.Property(e => e.dirverLicenseNumber).HasMaxLength(50);

                entity.Property(e => e.driverHomeTerminal).HasMaxLength(10);

                entity.Property(e => e.region).HasMaxLength(50);

                entity.Property(e => e.username).HasMaxLength(250);
            });

            modelBuilder.Entity<DriverBreakRule>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.country).HasMaxLength(50);

                entity.Property(e => e.region).HasMaxLength(50);
            });

            modelBuilder.Entity<DriverPerformanceSummary>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.cruiseTime).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.distance).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.engineLoadPercent).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.fuel).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.overRpmTime).HasColumnType("numeric(18, 0)");
            });

            modelBuilder.Entity<DriverWaiver>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.region)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.providerId).HasMaxLength(100);

                entity.Property(e => e.name).HasMaxLength(100);

                entity.Property(e => e.cmvVIN).HasMaxLength(100);

                entity.Property(e => e.licensePlate).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}