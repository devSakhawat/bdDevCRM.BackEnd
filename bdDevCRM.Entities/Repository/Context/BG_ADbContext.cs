using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using bdDevCRM.Entities.Entities.Entities.CRMM;
using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.Entities.Repository.Context;

public partial class BG_ADbContext : DbContext
{
    public BG_ADbContext()
    {
    }

    public BG_ADbContext(DbContextOptions<BG_ADbContext> options) : base(options)
    {
    }

    public virtual DbSet<Crmcourse> Crmcourse { get; set; }

    public virtual DbSet<CrmcourseIntake> CrmcourseIntake { get; set; }

    public virtual DbSet<Crminstitute> Crminstitute { get; set; }

    public virtual DbSet<Crmmonth> Crmmonth { get; set; }

    public virtual DbSet<Crmyear> Crmyear { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-657HR8U;User ID=sa;Password=@BIr3011;Database=BG_A;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Crmcourse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CRMCourse");

            entity.Property(e => e.AdditionalInformationOfCourse)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.After2YearsPswcompletingCourse)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("After2YearsPSWCompletingCourse");
            entity.Property(e => e.ApplicationFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AwardingBody)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CountryBenefits)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CourseBenefits)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CourseCategory)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CourseDuration)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CourseFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CourseId).ValueGeneratedOnAdd();
            entity.Property(e => e.CourseLevel)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CourseTitle)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.DocumentId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.FundsRequirementforVisa)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.GeneralEligibility)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.InstitutionalBenefits)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.KeyModules)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.LanguagesRequirement)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.MonthlyLivingCost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PartTimeWorkDetails)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.VisaRequirement)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CrmcourseIntake>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CRMCourseIntake");

            entity.Property(e => e.CourseIntakeId).ValueGeneratedOnAdd();
            entity.Property(e => e.IntakeTitile)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Crminstitute>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CRMInstitute");

            entity.Property(e => e.ApplicationFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Campus)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FundsRequirementforVisa)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.InstituteAddress)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.InstituteCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InstituteEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.InstituteId).ValueGeneratedOnAdd();
            entity.Property(e => e.InstituteMobileNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.InstitutePhoneNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("InstitutePhoneNO");
            entity.Property(e => e.InstituteType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.InstitutionLogo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.InstitutionProspectus)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.InstitutionStatusNotes)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.InstitutionalBenefits)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.LanguagesRequirement)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.MonthlyLivingCost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PartTimeWorkDetails)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ScholarshipsPolicy)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Website)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Crmmonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CRMMonth");

            entity.Property(e => e.CrmmonthName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CRMMonthName");
            entity.Property(e => e.MonthCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MonthId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Crmyear>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CRMYear");

            entity.Property(e => e.YearCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.YearId).ValueGeneratedOnAdd();
            entity.Property(e => e.YearName)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
