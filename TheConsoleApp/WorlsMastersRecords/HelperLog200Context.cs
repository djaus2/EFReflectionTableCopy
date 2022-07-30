using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFReflectionTableCopy.WorldMastersRecords
{
    public partial class HelperLog200Context : DbContext
    {
        public HelperLog200Context()
        {
        }

        public HelperLog200Context(DbContextOptions<HelperLog200Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Wmarecord> Wmarecords { get; set; } = null!;
        public virtual DbSet<WorldMastersRecord> WorldMastersRecords { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source = localhost\\SQLEXPRESS; Initial Catalog = HelperLog200; Integrated Security = SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Wmarecord>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("WMARecords");

                entity.Property(e => e.Event)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EventType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Result)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.TrackType)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
