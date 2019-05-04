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

        public virtual DbSet<CurrentServiceStatus> CurrentServiceStatus { get; set; }
        public virtual DbSet<CurrentServiceStatusFactor> CurrentServiceStatusFactor { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<DriverBreakRule> DriverBreakRule { get; set; }
        public virtual DbSet<DriverPerformanceSummary> DriverPerformanceSummary { get; set; }
        public virtual DbSet<DriverWaiver> DriverWaiver { get; set; }
        public virtual DbSet<DriverWorkLog> DriverWorkLog { get; set; }
        public virtual DbSet<LogEvent> LogEvent { get; set; }
        public virtual DbSet<LogEventAnnotation> LogEventAnnotation { get; set; }
        public virtual DbSet<ServiceStatusEvent> ServiceStatusEvent { get; set; }
        public virtual DbSet<ServiceStatusEventFactor> ServiceStatusEventFactor { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleFlaggedEvent> VehicleFlaggedEvent { get; set; }
        public virtual DbSet<VehicleFaultCodeEvent> VehicleFaultCodeEvent { get; set; }
        public virtual DbSet<VehiclePerformanceEvent> VehiclePerformanceEvent { get; set; }
        public virtual DbSet<VehiclePerformanceThreshold> VehiclePerformanceThreshold { get; set; }
        public virtual DbSet<VehicleLocationTimeHistory> VehicleLocationTimeHistory { get; set; }
        public virtual DbSet<StopGeographicDetails> StopGeographicDetails { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<VehicleStopXRef> VehicleStopXRef { get; set; }
        public virtual DbSet<VehicleMessage> VehicleMessage { get; set; }
        public virtual DbSet<HwyDataPoint> HwyDataPoints { get; set; }
        public virtual DbSet<TokenTranslation> TokenTranslation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<CurrentServiceStatus>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CurrentServiceStatusFactor>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.Factor)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.driverHomeTerminal).HasMaxLength(50);

                entity.Property(e => e.driverLicenseNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.enabled).HasDefaultValueSql("((1))");

                entity.Property(e => e.password).HasMaxLength(250);

                entity.Property(e => e.region)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.username)
                    .IsRequired()
                    .HasMaxLength(250);
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

                entity.Property(e => e.cruiseTime).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.distance).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.engineLoadPercent).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.fuel).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.overRpmTime).HasColumnType("numeric(18, 5)");
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

            modelBuilder.Entity<DriverWorkLog>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.driverId).IsRequired();

                entity.Property(e => e.workDate).IsRequired();

                entity.Property(e => e.hoursWorked).HasColumnType("numeric(18, 4)");

            });

            modelBuilder.Entity<ServiceStatusEvent>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ServiceStatusEventFactor>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.Factor)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.cmvVIN)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.licensePlate)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.name).HasMaxLength(100);

                entity.Property(e => e.sequence).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VehicleFlaggedEvent>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.accelerationPercent).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.ecmSpeed).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.engineRPM).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.eventComment).HasMaxLength(1000);

                entity.Property(e => e.forwardVehicleDistance).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.forwardVehicleElapsed).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.forwardVehicleSpeed).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.gpsHeading).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.gpsQuality).HasMaxLength(50);

                entity.Property(e => e.gpsSpeed).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.ignitionStatus).IsRequired();

                entity.Property(e => e.trigger)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<VehicleFaultCodeEvent>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.clearType).HasMaxLength(50);

                entity.Property(e => e.eventComment).HasMaxLength(500);

                entity.Property(e => e.gpsQuality)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.latitude).HasColumnType("numeric(18, 8)");

                entity.Property(e => e.longitude).HasColumnType("numeric(18, 8)");

                entity.Property(e => e.parameterOrSubsystemIdType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VehicleLocationTimeHistory>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.latitude).HasColumnType("numeric(18, 8)");

                entity.Property(e => e.longitude).HasColumnType("numeric(18, 8)");

                entity.Property(e => e.sequence).ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<VehiclePerformanceThreshold>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

            });

            modelBuilder.Entity<VehiclePerformanceEvent>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.eventComment).HasMaxLength(1000);

                entity.Property(e => e.engineLoadStopped).HasColumnType("numeric(3, 2)");

                entity.Property(e => e.engineLoadMoving).HasColumnType("numeric(3, 2)");

                entity.Property(e => e.particulateFilterStatus).HasMaxLength(50);

            });

            modelBuilder.Entity<VehiclePerformanceEvent>()
                .HasOne(t => t.thresholds)
                .WithOne();

            modelBuilder.Entity<StopGeographicDetails>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.stopName).HasMaxLength(500);

                entity.Property(e => e.address).HasMaxLength(500);

                entity.Property(e => e.latitude).HasColumnType("numeric(18, 8)");

                entity.Property(e => e.longitude).HasColumnType("numeric(18, 8)");

            });


            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("newid()");

                entity.Property(e => e.latitude).IsRequired().HasColumnType("numeric(18, 8)");

                entity.Property(e => e.longitude).IsRequired().HasColumnType("numeric(18, 8)");

                entity.Property(e => e.identifiedPlace).HasMaxLength(100);

                entity.Property(e => e.identifiedState).HasMaxLength(50);

                entity.Property(e => e.distanceFrom).HasColumnType("numeric(18, 3)");

                entity.Property(e => e.directionFrom).HasMaxLength(10);

            });

            modelBuilder.Entity<LogEvent>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.coDrivers).HasMaxLength(1000);

                entity.Property(e => e.comment)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.distanceLastValid).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.eventDataChecksum)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.eventType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.origin)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.state)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.location)
                    .WithOne();
            });

            modelBuilder.Entity<LogEventAnnotation>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.comment)
                    .IsRequired()
                    .HasMaxLength(4000);

            });

            modelBuilder.Entity<TokenTranslation>(entity =>
            {
                entity.Property(e => e.msgid).IsRequired().HasMaxLength(100);

                entity.Property(e => e.origin).HasMaxLength(500);

                entity.Property(e => e.msgstr).HasMaxLength(1000);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}