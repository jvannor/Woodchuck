using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UtilityLib.Models;

namespace UtilityLib.Data
{
    public partial class WoodchuckDbContext : DbContext
    {
        public WoodchuckDbContext()
        {
        }

        public WoodchuckDbContext(DbContextOptions<WoodchuckDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CameraSettings> CameraSettings { get; set; }
        public virtual DbSet<MonitorSettings> MonitorSettings { get; set; }
        public virtual DbSet<NotificationSettings> NotificationSettings { get; set; }
        public virtual DbSet<WorkerSettings> WorkerSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CameraSettings>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Password).HasMaxLength(256);

                entity.Property(e => e.Uri)
                    .IsRequired()
                    .HasColumnName("URI")
                    .HasMaxLength(1024);

                entity.Property(e => e.User).HasMaxLength(128);
            });

            modelBuilder.Entity<MonitorSettings>(entity =>
            {
                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Value).HasMaxLength(1024);
            });

            modelBuilder.Entity<NotificationSettings>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(24);
            });

            modelBuilder.Entity<WorkerSettings>(entity =>
            {
                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Value).HasMaxLength(1024);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
