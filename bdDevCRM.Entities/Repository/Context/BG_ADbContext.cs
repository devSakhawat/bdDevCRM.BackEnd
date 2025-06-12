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

    public virtual DbSet<Dmsdocument> Dmsdocument { get; set; }

    public virtual DbSet<DmsdocumentAccessLog> DmsdocumentAccessLog { get; set; }

    public virtual DbSet<DmsdocumentFolder> DmsdocumentFolder { get; set; }

    public virtual DbSet<DmsdocumentTag> DmsdocumentTag { get; set; }

    public virtual DbSet<DmsdocumentType> DmsdocumentType { get; set; }

    public virtual DbSet<DmsdocumentVersion> DmsdocumentVersion { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-657HR8U;User ID=sa;Password=@BIr3011;Database=BG_A;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dmsdocument>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__DMSDocum__1ABEEF0FA80AC05E");

            entity.ToTable("DMSDocument");

            entity.Property(e => e.FileExtension).HasMaxLength(10);
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.ReferenceEntityId).HasMaxLength(50);
            entity.Property(e => e.ReferenceEntityType).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UploadDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UploadedByUserId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.DocumentType).WithMany(p => p.Dmsdocument)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Document_DocumentType");

            entity.HasMany(d => d.Tag).WithMany(p => p.Document)
                .UsingEntity<Dictionary<string, object>>(
                    "DmsdocumentTagMap",
                    r => r.HasOne<DmsdocumentTag>().WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_DocumentTagMap_DocumentTag"),
                    l => l.HasOne<Dmsdocument>().WithMany()
                        .HasForeignKey("DocumentId")
                        .HasConstraintName("FK_DocumentTagMap_Document"),
                    j =>
                    {
                        j.HasKey("DocumentId", "TagId").HasName("PK_DocumentTagMap");
                        j.ToTable("DMSDocumentTagMap");
                    });
        });

        modelBuilder.Entity<DmsdocumentAccessLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__DMSDocum__5E548648EC7B75AF");

            entity.ToTable("DMSDocumentAccessLog");

            entity.Property(e => e.AccessDateTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Action).HasMaxLength(50);

            entity.HasOne(d => d.Document).WithMany(p => p.DmsdocumentAccessLog)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("FK_DocumentAccessLog_Document");
        });

        modelBuilder.Entity<DmsdocumentFolder>(entity =>
        {
            entity.HasKey(e => e.FolderId).HasName("PK__DMSDocum__ACD7107FD7A53641");

            entity.ToTable("DMSDocumentFolder");

            entity.Property(e => e.FolderName).HasMaxLength(255);
            entity.Property(e => e.ReferenceEntityId).HasMaxLength(50);
            entity.Property(e => e.ReferenceEntityType).HasMaxLength(50);

            entity.HasOne(d => d.ParentFolder).WithMany(p => p.InverseParentFolder)
                .HasForeignKey(d => d.ParentFolderId)
                .HasConstraintName("FK_DocumentFolder_ParentFolder");
        });

        modelBuilder.Entity<DmsdocumentTag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__DMSDocum__657CF9AC43C5B9E0");

            entity.ToTable("DMSDocumentTag");

            entity.HasIndex(e => e.Name, "UQ__DMSDocum__737584F60E3D41BA").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<DmsdocumentType>(entity =>
        {
            entity.HasKey(e => e.DocumentTypeId).HasName("PK__DMSDocum__DBA390E192D25821");

            entity.ToTable("DMSDocumentType");

            entity.Property(e => e.AcceptedExtensions).HasMaxLength(255);
            entity.Property(e => e.EntityType).HasMaxLength(50);
            entity.Property(e => e.MaxFileSizeMb).HasColumnName("MaxFileSizeMB");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<DmsdocumentVersion>(entity =>
        {
            entity.HasKey(e => e.VersionId).HasName("PK__DMSDocum__16C6400F35C8BCE0");

            entity.ToTable("DMSDocumentVersion");

            entity.HasIndex(e => new { e.DocumentId, e.VersionNumber }, "UQ_DocumentVersion_DocumentId_VersionNumber").IsUnique();

            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.UploadedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Document).WithMany(p => p.DmsdocumentVersion)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("FK_DocumentVersion_Document");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
