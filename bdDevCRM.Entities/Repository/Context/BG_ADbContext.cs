using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using bdDevCRM.Entities.Entities.Entities.CRMM;

namespace bdDevCRM.Entities.Repository.Context;

public partial class BG_ADbContext : DbContext
{
    public BG_ADbContext()
    {
    }

    public BG_ADbContext(DbContextOptions<BG_ADbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Currency> Currency { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-657HR8U;User ID=sa;Password=@BIr3011;Database=BG_A;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.CurrencyId).ValueGeneratedOnAdd();
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
