using bdDevCRM.Entities.Entities.Core;
using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.Entities.Entities.DMS;
using bdDevCRM.Entities.Entities.System;
using Microsoft.EntityFrameworkCore;

namespace bdDevCRM.Sql.Context;

public partial class CRMContext : DbContext
{
  public CRMContext()
  {
  }

  public CRMContext(DbContextOptions<CRMContext> options) : base(options)
  {
  }


  public virtual DbSet<AboutUsLicense> AboutUsLicense { get; set; }

  public virtual DbSet<AccessRestriction> AccessRestriction { get; set; }

  public virtual DbSet<Accesscontrol> Accesscontrol { get; set; }

  public virtual DbSet<ApproverDetails> ApproverDetails { get; set; }

  public virtual DbSet<ApproverHistory> ApproverHistory { get; set; }

  public virtual DbSet<ApproverOrder> ApproverOrder { get; set; }

  public virtual DbSet<ApproverType> ApproverType { get; set; }

  public virtual DbSet<ApproverTypeToGroupMapping> ApproverTypeToGroupMapping { get; set; }

  public virtual DbSet<AppsTokenInfo> AppsTokenInfo { get; set; }

  public virtual DbSet<AppsTransactionLog> AppsTransactionLog { get; set; }

  public virtual DbSet<AssemblyInfo> AssemblyInfo { get; set; }

  public virtual DbSet<AssignApprover> AssignApprover { get; set; }

  public virtual DbSet<AuditLog> AuditLog { get; set; }

  public virtual DbSet<AuditTrail> AuditTrail { get; set; }

  public virtual DbSet<AuditType> AuditType { get; set; }

  public virtual DbSet<BoardInstitute> BoardInstitute { get; set; }

  public virtual DbSet<Company> Company { get; set; }

  public virtual DbSet<Competencies> Competencies { get; set; }

  public virtual DbSet<CompetencyLevel> CompetencyLevel { get; set; }

  public virtual DbSet<Country> Country { get; set; }

  public virtual DbSet<Course> Course { get; set; }

  public virtual DbSet<CrmapplicantCourseDetials> CrmapplicantCourseDetials { get; set; }

  public virtual DbSet<Crmcourse> Crmcourse { get; set; }

  public virtual DbSet<CrmcourseIntake> CrmcourseIntake { get; set; }

  public virtual DbSet<Crminstitute> Crminstitute { get; set; }

  public virtual DbSet<CrminstituteType> CrminstituteType { get; set; }

  public virtual DbSet<Crmmonth> Crmmonth { get; set; }

  public virtual DbSet<Crmyear> Crmyear { get; set; }

  public virtual DbSet<CurencyRate> CurencyRate { get; set; }

  public virtual DbSet<Currency> Currency { get; set; }

  public virtual DbSet<CurrencyInfo> CurrencyInfo { get; set; }

  public virtual DbSet<DeligationInfo> DeligationInfo { get; set; }

  public virtual DbSet<Dmsdocument> Dmsdocument { get; set; }

  public virtual DbSet<DmsdocumentAccessLog> DmsdocumentAccessLog { get; set; }

  public virtual DbSet<DmsdocumentFolder> DmsdocumentFolder { get; set; }

  public virtual DbSet<DmsdocumentTag> DmsdocumentTag { get; set; }

  public virtual DbSet<DmsdocumentTagMap> DmsdocumentTagMap { get; set; }

  public virtual DbSet<DmsdocumentType> DmsdocumentType { get; set; }

  public virtual DbSet<DmsdocumentVersion> DmsdocumentVersion { get; set; }

  public virtual DbSet<Docmdetails> Docmdetails { get; set; }

  public virtual DbSet<Docmdetailshistory> Docmdetailshistory { get; set; }

  public virtual DbSet<Documanttype> Documanttype { get; set; }

  public virtual DbSet<Document> Document { get; set; }

  public virtual DbSet<DocumentParameter> DocumentParameter { get; set; }

  public virtual DbSet<DocumentParameterMapping> DocumentParameterMapping { get; set; }

  public virtual DbSet<DocumentQueryMapping> DocumentQueryMapping { get; set; }

  public virtual DbSet<DocumentTemplate> DocumentTemplate { get; set; }

  public virtual DbSet<Employee> Employee { get; set; }

  public virtual DbSet<Employeetype> Employeetype { get; set; }

  public virtual DbSet<Employment> Employment { get; set; }

  public virtual DbSet<GroupMember> GroupMember { get; set; }

  public virtual DbSet<GroupPermission> GroupPermission { get; set; }

  public virtual DbSet<Groups> Groups { get; set; }

  public virtual DbSet<Holiday> Holiday { get; set; }

  public virtual DbSet<Menu> Menu { get; set; }

  public virtual DbSet<Module> Module { get; set; }

  public virtual DbSet<PasswordHistory> PasswordHistory { get; set; }

  public virtual DbSet<ReportBuilder> ReportBuilder { get; set; }

  public virtual DbSet<SystemSettings> SystemSettings { get; set; }

  public virtual DbSet<Thana> Thana { get; set; }

  public virtual DbSet<Timesheet> Timesheet { get; set; }

  public virtual DbSet<Users> Users { get; set; }

  public virtual DbSet<Wfaction> Wfaction { get; set; }

  public virtual DbSet<Wfstate> Wfstate { get; set; }
  public virtual DbSet<TokenBlacklist> TokenBlacklist { get; set; }
  public virtual DbSet<Branch> Branch { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<AboutUsLicense>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AboutUsLicenseId)
          .ValueGeneratedOnAdd()
          .HasColumnName("AboutUsLicenseID");
      entity.Property(e => e.CodeBaseVersion).HasMaxLength(50);
      entity.Property(e => e.LicenseFor).HasMaxLength(50);
      entity.Property(e => e.LicenseNumber).HasMaxLength(50);
      entity.Property(e => e.LicenseType).HasMaxLength(50);
      entity.Property(e => e.LocationLicense).HasMaxLength(50);
      entity.Property(e => e.ProductCode).HasMaxLength(50);
      entity.Property(e => e.Sbulicense)
          .HasMaxLength(50)
          .HasColumnName("SBULicense");
      entity.Property(e => e.ServerId)
          .HasMaxLength(50)
          .HasColumnName("ServerID");
      entity.Property(e => e.UserLicense).HasMaxLength(50);
    });

    modelBuilder.Entity<AccessRestriction>(entity =>
    {
      entity.Property(e => e.AccessDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Accesscontrol>(entity =>
    {
      entity.HasKey(e => e.AccessId);

      entity.ToTable("ACCESSCONTROL");

      entity.Property(e => e.AccessName).HasMaxLength(50);
    });

    modelBuilder.Entity<ApproverDetails>(entity =>
    {
      entity.HasKey(e => e.RemarksId).HasName("PK_Remarks");

      entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
    });

    modelBuilder.Entity<ApproverHistory>(entity =>
    {
      entity.HasKey(e => e.AssignApproverId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.DeleteDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ApproverOrder>(entity =>
    {
      entity.Property(e => e.ApproverOrderId).ValueGeneratedNever();
      entity.Property(e => e.OrderTitle)
          .HasMaxLength(50)
          .IsUnicode(false);
    });

    modelBuilder.Entity<ApproverType>(entity =>
    {
      entity.Property(e => e.ApproverTypeId).ValueGeneratedNever();
      entity.Property(e => e.ApproverTypeName)
          .HasMaxLength(50)
          .IsUnicode(false);
    });

    modelBuilder.Entity<ApproverTypeToGroupMapping>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ApproverTypeMapId).ValueGeneratedOnAdd();

      entity.HasOne(d => d.ApproverType).WithMany()
          .HasForeignKey(d => d.ApproverTypeId)
          .HasConstraintName("FK_ApproverTypeToGroupMapping_ApproverType");
    });

    modelBuilder.Entity<AppsTokenInfo>(entity =>
    {
      entity.Property(e => e.AppsUserId)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.EmployeeId)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
      entity.Property(e => e.IssueDate).HasColumnType("datetime");
      entity.Property(e => e.TokenNumber)
          .HasMaxLength(200)
          .IsUnicode(false);
    });

    modelBuilder.Entity<AppsTransactionLog>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AppsUserId).HasMaxLength(50);
      entity.Property(e => e.EmployeeId).HasMaxLength(100);
      entity.Property(e => e.Remarks).HasMaxLength(1000);
      entity.Property(e => e.Request).HasMaxLength(200);
      entity.Property(e => e.Response).HasMaxLength(2000);
      entity.Property(e => e.TransactionDate).HasColumnType("datetime");
      entity.Property(e => e.TransactionLogId).ValueGeneratedOnAdd();
      entity.Property(e => e.TransactionType).HasMaxLength(100);
    });

    modelBuilder.Entity<AssemblyInfo>(entity =>
    {
      entity.Property(e => e.AssemblyInfoId)
          .ValueGeneratedNever()
          .HasComment("");
      entity.Property(e => e.AssemblyCompany)
          .HasMaxLength(100)
          .IsUnicode(false);
      entity.Property(e => e.AssemblyCopyright)
          .HasMaxLength(150)
          .IsUnicode(false);
      entity.Property(e => e.AssemblyDescription)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.AssemblyProduct)
          .HasMaxLength(100)
          .IsUnicode(false);
      entity.Property(e => e.AssemblyTitle)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.AssemblyVersion)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.CvBankPath).HasMaxLength(250);
      entity.Property(e => e.IsAttendanceByLogin).HasComment("false=Attedance by login inactive feature");
      entity.Property(e => e.PoweredBy)
          .HasMaxLength(150)
          .IsUnicode(false);
      entity.Property(e => e.PoweredByUrl)
          .HasMaxLength(250)
          .IsUnicode(false);
      entity.Property(e => e.ProductBanner)
          .HasMaxLength(250)
          .IsUnicode(false);
      entity.Property(e => e.ProductStyleSheet)
          .HasMaxLength(250)
          .IsUnicode(false);
    });

    modelBuilder.Entity<AssignApprover>(entity =>
    {
      entity.HasNoKey();

      entity.HasIndex(e => new { e.HrRecordId, e.ModuleId, e.SortOrder, e.Type }, "IX_DuplicateApprover").IsUnique();

      entity.Property(e => e.AssignApproverId).ValueGeneratedOnAdd();
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<AuditLog>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ActionName)
          .HasMaxLength(150)
          .IsUnicode(false);
      entity.Property(e => e.AuditDate).HasColumnType("datetime");
      entity.Property(e => e.AuditId).ValueGeneratedOnAdd();
      entity.Property(e => e.AuditStatus)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.BrowserInfo)
          .HasMaxLength(500)
          .IsUnicode(false);
      entity.Property(e => e.ClientIp)
          .HasMaxLength(50)
          .HasColumnName("ClientIP");
      entity.Property(e => e.ClientUser).HasMaxLength(500);
      entity.Property(e => e.ControllerName)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.DomainName)
          .HasMaxLength(150)
          .IsUnicode(false);
      entity.Property(e => e.ExceptionLog).IsUnicode(false);
      entity.Property(e => e.MacAddress).HasMaxLength(500);
      entity.Property(e => e.ReferrerUrl)
          .HasMaxLength(250)
          .IsUnicode(false);
      entity.Property(e => e.RequestedParams).IsUnicode(false);
      entity.Property(e => e.RequestedUrl).IsUnicode(false);
      entity.Property(e => e.TableName)
          .HasMaxLength(50)
          .IsUnicode(false);
    });

    modelBuilder.Entity<AuditTrail>(entity =>
    {
      entity
          .HasNoKey()
          .ToTable("AUDIT_TRAIL");

      entity.Property(e => e.ActionDate)
          .HasColumnType("datetime")
          .HasColumnName("ACTION_DATE");
      entity.Property(e => e.AuditDescription).HasColumnName("AUDIT_DESCRIPTION");
      entity.Property(e => e.AuditId)
          .ValueGeneratedOnAdd()
          .HasColumnName("AUDIT_ID");
      entity.Property(e => e.AuditStatus)
          .HasMaxLength(50)
          .HasColumnName("Audit_Status");
      entity.Property(e => e.AuditType)
          .HasMaxLength(500)
          .HasColumnName("AUDIT_TYPE");
      entity.Property(e => e.ClientIp)
          .HasMaxLength(50)
          .HasColumnName("CLIENT_IP");
      entity.Property(e => e.ClientUser)
          .HasMaxLength(500)
          .HasColumnName("CLIENT_USER");
      entity.Property(e => e.RequestedUrl).HasColumnName("Requested_Url");
      entity.Property(e => e.Shortdescription).HasColumnName("SHORTDESCRIPTION");
      entity.Property(e => e.UserId).HasColumnName("USER_ID");
    });

    modelBuilder.Entity<AuditType>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.AuditType1)
          .HasMaxLength(250)
          .IsUnicode(false)
          .HasColumnName("AuditType");
    });

    modelBuilder.Entity<BoardInstitute>(entity =>
    {
      entity.Property(e => e.BoardInstituteName)
          .HasMaxLength(500)
          .IsUnicode(false);
    });

    modelBuilder.Entity<Company>(entity =>
    {
      entity.Property(e => e.Address).HasMaxLength(1000);
      entity.Property(e => e.CompanyAlias).HasMaxLength(50);
      entity.Property(e => e.CompanyCircle).HasMaxLength(200);
      entity.Property(e => e.CompanyCode).HasMaxLength(50);
      entity.Property(e => e.CompanyName).HasMaxLength(50);
      entity.Property(e => e.CompanyRegisterNo).HasMaxLength(250);
      entity.Property(e => e.CompanyTin).HasMaxLength(50);
      entity.Property(e => e.CompanyZone).HasMaxLength(200);
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.Email).HasMaxLength(100);
      entity.Property(e => e.Fax).HasMaxLength(50);
      entity.Property(e => e.FullLogoPath).HasMaxLength(1000);
      entity.Property(e => e.FullLogoPathForReport).HasMaxLength(1000);
      entity.Property(e => e.IsElautoAddedForCurrentYear).HasColumnName("IsELAutoAddedForCurrentYear");
      entity.Property(e => e.LetterFooter).HasMaxLength(1000);
      entity.Property(e => e.LetterHeader).HasMaxLength(1000);
      entity.Property(e => e.Phone).HasMaxLength(50);
      entity.Property(e => e.PrimaryContact).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Competencies>(entity =>
    {
      entity.Property(e => e.CompetencyName).HasMaxLength(50);
    });

    modelBuilder.Entity<CompetencyLevel>(entity =>
    {
      entity.HasKey(e => e.LevelId);

      entity.Property(e => e.LevelTitle).HasMaxLength(50);
      entity.Property(e => e.Remarks)
          .HasMaxLength(150)
          .IsUnicode(false);
    });

    modelBuilder.Entity<Country>(entity =>
    {
      entity.Property(e => e.CountryCode).HasMaxLength(50);
      entity.Property(e => e.CountryName).HasMaxLength(100);
    });

    modelBuilder.Entity<Course>(entity =>
    {
      entity.HasNoKey();

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
      entity.Property(e => e.CampusLocation)
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

    modelBuilder.Entity<CrmapplicantCourseDetials>(entity =>
    {
      entity
          .HasNoKey()
          .ToTable("CRMApplicantCourseDetials");

      entity.Property(e => e.ApplicantCourseDetailsId).ValueGeneratedOnAdd();
      entity.Property(e => e.ApplicationFee).HasColumnType("decimal(18, 2)");
      entity.Property(e => e.CourseRemarks)
          .HasMaxLength(500)
          .IsUnicode(false);
      entity.Property(e => e.PaymentDate).HasColumnType("datetime");
      entity.Property(e => e.PaymentReferenceNumber)
          .HasMaxLength(50)
          .IsUnicode(false);
    });

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
      entity.HasKey(e => e.InstituteId);
      entity.ToTable("CRMInstitute");

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
      entity.Property(e => e.InstituteName)
          .HasMaxLength(100)
          .IsUnicode(false);
      entity.Property(e => e.InstitutePhoneNo)
          .HasMaxLength(20)
          .IsUnicode(false)
          .HasColumnName("InstitutePhoneNO");
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

    modelBuilder.Entity<CrminstituteType>(entity =>
    {
      entity.HasKey(e => e.InstituteTypeId);

      entity.ToTable("CRMInstituteType");

      entity.Property(e => e.InstituteTypeName)
          .HasMaxLength(150)
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

    modelBuilder.Entity<CurencyRate>(entity =>
    {
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CurrencyMonth).HasColumnType("datetime");
      entity.Property(e => e.CurrencyRateRation).HasColumnType("decimal(18, 2)");
    });

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

    modelBuilder.Entity<CurrencyInfo>(entity =>
    {
      entity.HasKey(e => e.CurrencyId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.CurrencyName).HasMaxLength(50);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<DeligationInfo>(entity =>
    {
      entity.HasKey(e => e.DeligationId).HasName("PK_Deligation");
    });

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
      entity.Property(e => e.UploadedByUserId).HasMaxLength(50).IsUnicode(false);

      entity.HasOne(d => d.DocumentType).WithMany(p => p.Dmsdocument)
          .HasForeignKey(d => d.DocumentTypeId)
          .OnDelete(DeleteBehavior.ClientSetNull)
          .HasConstraintName("FK_Document_DocumentType");
      entity.Property(e => e.SystemTag)
          .HasMaxLength(200)
          .HasColumnName("SystemTag");
    });

    modelBuilder.Entity<DmsdocumentAccessLog>(entity =>
    {
      entity.HasKey(e => e.LogId).HasName("PK__DMSDocum__5E548648EC7B75AF");

      entity.ToTable("DMSDocumentAccessLog");

      entity.Property(e => e.AccessDateTime).HasDefaultValueSql("(getdate())");
      entity.Property(e => e.AccessedByUserId)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.Action).HasMaxLength(50);
      entity.Property(e => e.DeviceInfo)
          .HasMaxLength(200)
          .IsUnicode(false);
      entity.Property(e => e.IpAddress)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.MacAddress)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.Notes)
          .HasMaxLength(150)
          .IsUnicode(false);

      entity.HasOne(d => d.Document).WithMany(p => p.DmsdocumentAccessLog)
          .HasForeignKey(d => d.DocumentId)
          .HasConstraintName("FK_DocumentAccessLog_Document");
    });

    modelBuilder.Entity<DmsdocumentFolder>(entity =>
    {
      entity.HasKey(e => e.FolderId).HasName("PK__DMSDocum__ACD7107FD7A53641");
      entity.ToTable("DMSDocumentFolder");

      entity.Property(e => e.FolderName).HasMaxLength(255);
      entity.Property(e => e.OwnerId).HasMaxLength(50).IsUnicode(false).IsRequired(false);
      entity.Property(e => e.ReferenceEntityId).HasMaxLength(50).IsRequired(false); ;
      entity.Property(e => e.ReferenceEntityType).HasMaxLength(150).IsRequired(false); ;

      entity.HasOne(d => d.ParentFolder).WithMany(p => p.InverseParentFolder)
          .HasForeignKey(d => d.ParentFolderId)
          .HasConstraintName("FK_DocumentFolder_ParentFolder");
    });

    modelBuilder.Entity<DmsdocumentTag>(entity =>
    {
      entity.HasKey(e => e.TagId).HasName("PK__DMSDocum__657CF9AC43C5B9E0");

      entity.ToTable("DMSDocumentTag");

      entity.HasIndex(e => e.DocumentTagName, "UQ__DMSDocum__737584F60E3D41BA").IsUnique();

      entity.Property(e => e.DocumentTagName).HasMaxLength(200);
    });

    modelBuilder.Entity<DmsdocumentTagMap>(entity =>
    {
      entity.HasKey(e => e.TagMapId);
      entity.ToTable("DMSDocumentTagMap");

      entity.Property(e => e.TagMapId).ValueGeneratedOnAdd();

      entity.HasOne(d => d.Document).WithMany(p => p.DmsdocumentTagMap)
          .HasForeignKey(d => d.DocumentId)
          .HasConstraintName("FK_DocumentTagMap_Document");

      entity.HasOne(d => d.Tag).WithMany(p => p.DmsdocumentTagMap)
          .HasForeignKey(d => d.TagId)
          .HasConstraintName("FK_DocumentTagMap_DocumentTag");
    });

    modelBuilder.Entity<DmsdocumentType>(entity =>
    {
      entity.HasKey(e => e.DocumentTypeId).HasName("PK__DMSDocum__DBA390E192D25821");

      entity.ToTable("DMSDocumentType");

      entity.Property(e => e.AcceptedExtensions).HasMaxLength(255);
      entity.Property(e => e.DocumentType).HasMaxLength(100);
      entity.Property(e => e.MaxFileSizeMb).HasColumnName("MaxFileSizeMB");
      entity.Property(e => e.Name).HasMaxLength(100);
    });

    modelBuilder.Entity<DmsdocumentVersion>(entity =>
    {
      entity.HasKey(e => e.VersionId).HasName("PK__DMSDocum__16C6400F35C8BCE0");

      entity.ToTable("DMSDocumentVersion");

      entity.HasIndex(e => new { e.DocumentId, e.VersionNumber }, "UQ_DocumentVersion_DocumentId_VersionNumber").IsUnique();

      entity.Property(e => e.FileName).HasMaxLength(255);
      entity.Property(e => e.UploadedBy)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.UploadedDate).HasDefaultValueSql("(getdate())");

      entity.HasOne(d => d.Document).WithMany(p => p.DmsdocumentVersion)
          .HasForeignKey(d => d.DocumentId)
          .HasConstraintName("FK_DocumentVersion_Document");
    });

    modelBuilder.Entity<Docmdetails>(entity =>
    {
      entity.HasKey(e => e.DocumentId);

      entity.ToTable("DOCMDETAILS");

      entity.Property(e => e.DocumentId).HasColumnName("DOCUMENT_ID");
      entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");
      entity.Property(e => e.Filedescription)
          .HasMaxLength(500)
          .HasColumnName("FILEDESCRIPTION");
      entity.Property(e => e.Filename)
          .HasMaxLength(500)
          .HasColumnName("FILENAME");
      entity.Property(e => e.Fullpath)
          .HasMaxLength(1000)
          .HasColumnName("FULLPATH");
      entity.Property(e => e.Lastopenorclosebyid).HasColumnName("LASTOPENORCLOSEBYID");
      entity.Property(e => e.Lastupdate)
          .HasColumnType("datetime")
          .HasColumnName("LASTUPDATE");
      entity.Property(e => e.Remarks)
          .HasMaxLength(1000)
          .HasColumnName("REMARKS");
      entity.Property(e => e.Responsiblepersonto).HasColumnName("RESPONSIBLEPERSONTO");
      entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");
      entity.Property(e => e.Subject)
          .HasMaxLength(500)
          .HasColumnName("SUBJECT");
      entity.Property(e => e.UploadedBy).HasColumnName("UPLOADED_BY");
      entity.Property(e => e.UploadedDate)
          .HasColumnType("datetime")
          .HasColumnName("UPLOADED_DATE");
    });

    modelBuilder.Entity<Docmdetailshistory>(entity =>
    {
      entity.HasKey(e => e.DocumentHistoryId);

      entity.ToTable("DOCMDETAILSHISTORY");

      entity.Property(e => e.DocumentHistoryId).HasColumnName("DOCUMENT_HISTORY_ID");
      entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");
      entity.Property(e => e.DocumentId).HasColumnName("DOCUMENT_ID");
      entity.Property(e => e.Filedescription)
          .HasMaxLength(500)
          .HasColumnName("FILEDESCRIPTION");
      entity.Property(e => e.Filename)
          .HasMaxLength(500)
          .HasColumnName("FILENAME");
      entity.Property(e => e.Fullpath)
          .HasMaxLength(1000)
          .HasColumnName("FULLPATH");
      entity.Property(e => e.Lastopenorclosebyid).HasColumnName("LASTOPENORCLOSEBYID");
      entity.Property(e => e.Lastupdate)
          .HasDefaultValueSql("(getdate())")
          .HasColumnType("datetime")
          .HasColumnName("LASTUPDATE");
      entity.Property(e => e.Remarks)
          .HasMaxLength(1000)
          .HasColumnName("REMARKS");
      entity.Property(e => e.Responsiblepersonto).HasColumnName("RESPONSIBLEPERSONTO");
      entity.Property(e => e.Status).HasColumnName("STATUS");
      entity.Property(e => e.Subject)
          .HasMaxLength(500)
          .HasColumnName("SUBJECT");
      entity.Property(e => e.UploadedBy).HasColumnName("UPLOADED_BY");
      entity.Property(e => e.UploadedDate)
          .HasColumnType("datetime")
          .HasColumnName("UPLOADED_DATE");
    });

    modelBuilder.Entity<Documanttype>(entity =>
    {
      entity.HasKey(e => e.Documenttypeid);

      entity.ToTable("DOCUMANTTYPE");

      entity.Property(e => e.Documenttypeid).HasColumnName("DOCUMENTTYPEID");
      entity.Property(e => e.Description)
          .HasColumnType("text")
          .HasColumnName("DESCRIPTION");
      entity.Property(e => e.Documentname)
          .HasMaxLength(100)
          .HasColumnName("DOCUMENTNAME");
      entity.Property(e => e.Initiationdate)
          .HasColumnType("datetime")
          .HasColumnName("INITIATIONDATE");
      entity.Property(e => e.UseType)
          .HasDefaultValue(1)
          .HasComment("1=Personal Document,2=Applicant Document");
    });

    modelBuilder.Entity<Document>(entity =>
    {
      entity.ToTable("DOCUMENT");

      entity.Property(e => e.Documentid).HasColumnName("DOCUMENTID");
      entity.Property(e => e.Attacheddocument)
          .HasMaxLength(200)
          .HasColumnName("ATTACHEDDOCUMENT");
      entity.Property(e => e.Documenttypeid).HasColumnName("DOCUMENTTYPEID");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
      entity.Property(e => e.Summary)
          .HasMaxLength(2000)
          .HasColumnName("SUMMARY");
      entity.Property(e => e.Titleofdocument)
          .HasMaxLength(200)
          .HasColumnName("TITLEOFDOCUMENT");
    });

    modelBuilder.Entity<DocumentParameter>(entity =>
    {
      entity.HasKey(e => e.ParameterId);

      entity.Property(e => e.CaseCading)
          .HasMaxLength(100)
          .IsUnicode(false);
      entity.Property(e => e.ControlRole)
          .HasMaxLength(50)
          .IsUnicode(false)
          .HasColumnName("Control_Role");
      entity.Property(e => e.DataSource)
          .HasMaxLength(250)
          .IsUnicode(false);
      entity.Property(e => e.DataTextField)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.DataValueField)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.ParameterKey)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.ParameterName)
          .HasMaxLength(100)
          .IsUnicode(false);
    });

    modelBuilder.Entity<DocumentParameterMapping>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.IsVisible).HasDefaultValue(true);
      entity.Property(e => e.MappingId).ValueGeneratedOnAdd();

      entity.HasOne(d => d.DocumentType).WithMany()
          .HasForeignKey(d => d.DocumentTypeId)
          .HasConstraintName("FK_DocumentParameterMapping_DOCUMANTTYPE");

      entity.HasOne(d => d.Parameter).WithMany()
          .HasForeignKey(d => d.ParameterId)
          .HasConstraintName("FK_DocumentParameterMapping_DocumentParameter");
    });

    modelBuilder.Entity<DocumentQueryMapping>(entity =>
    {
      entity.HasKey(e => e.DocumentQueryId);

      entity.HasIndex(e => new { e.DocumentTypeId, e.ReportHeaderId }, "IX_DocumentQueryMapping").IsUnique();

      entity.Property(e => e.ParameterDefination)
          .HasMaxLength(1000)
          .IsUnicode(false);
    });

    modelBuilder.Entity<DocumentTemplate>(entity =>
    {
      entity.HasKey(e => e.DocumentId);

      entity.HasIndex(e => e.TemplateName, "IX_DocumentTemplate").IsUnique();

      entity.Property(e => e.DocumentText).IsUnicode(false);
      entity.Property(e => e.DocumentTitle).HasMaxLength(200);
      entity.Property(e => e.TemplateName).HasMaxLength(100);
    });

    modelBuilder.Entity<Employee>(entity =>
    {
      entity.HasKey(e => e.HrrecordId);

      entity.Property(e => e.HrrecordId).HasColumnName("HRRecordId");
      entity.Property(e => e.AdditionalInfo).HasMaxLength(50);
      entity.Property(e => e.ApproveDate).HasColumnType("smalldatetime");
      entity.Property(e => e.Birthidentification)
          .HasMaxLength(100)
          .HasColumnName("BIRTHIDENTIFICATION");
      entity.Property(e => e.BloodGroup).HasMaxLength(50);
      entity.Property(e => e.DateofBirth).HasColumnType("datetime");
      entity.Property(e => e.DateofMarriage).HasColumnType("datetime");
      entity.Property(e => e.FatherName).HasMaxLength(500);
      entity.Property(e => e.FullName).HasMaxLength(500);
      entity.Property(e => e.Height)
          .HasMaxLength(50)
          .HasColumnName("HEIGHT");
      entity.Property(e => e.Hobby)
          .HasMaxLength(2000)
          .HasColumnName("HOBBY");
      entity.Property(e => e.HomePhone).HasMaxLength(50);
      entity.Property(e => e.Identificationmark)
          .HasMaxLength(1000)
          .HasColumnName("IDENTIFICATIONMARK");
      entity.Property(e => e.Investmentamount).HasColumnName("INVESTMENTAMOUNT");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.Meritialstatus).HasColumnName("MERITIALSTATUS");
      entity.Property(e => e.MobileNo).HasMaxLength(500);
      entity.Property(e => e.MotherName).HasMaxLength(500);
      entity.Property(e => e.NationalId)
          .HasMaxLength(250)
          .HasColumnName("NationalID");
      entity.Property(e => e.OriginalBirthDay).HasMaxLength(50);
      entity.Property(e => e.PassportNo).HasMaxLength(250);
      entity.Property(e => e.Passportexpiredate)
          .HasColumnType("datetime")
          .HasColumnName("PASSPORTEXPIREDATE");
      entity.Property(e => e.Passportissuedate)
          .HasColumnType("datetime")
          .HasColumnName("PASSPORTISSUEDATE");
      entity.Property(e => e.PermanentPostCode).HasMaxLength(50);
      entity.Property(e => e.PersonalEmail).HasMaxLength(250);
      entity.Property(e => e.Placeofpassportissue).HasColumnName("PLACEOFPASSPORTISSUE");
      entity.Property(e => e.PresentPostCode).HasMaxLength(50);
      entity.Property(e => e.Profilepicture)
          .HasMaxLength(2000)
          .HasColumnName("PROFILEPICTURE");
      entity.Property(e => e.Refempid)
          .HasMaxLength(50)
          .HasColumnName("REFEMPID");
      entity.Property(e => e.ShortName).HasMaxLength(50);
      entity.Property(e => e.Signature)
          .HasMaxLength(2000)
          .HasColumnName("SIGNATURE");
      entity.Property(e => e.SpouseName).HasMaxLength(500);
      entity.Property(e => e.Taxexamption).HasColumnName("TAXEXAMPTION");
      entity.Property(e => e.Weight)
          .HasMaxLength(50)
          .HasColumnName("WEIGHT");
    });

    modelBuilder.Entity<Employeetype>(entity =>
    {
      entity.ToTable("EMPLOYEETYPE");

      entity.Property(e => e.Employeetypeid).HasColumnName("EMPLOYEETYPEID");
      entity.Property(e => e.EmployeeTypeCode).HasMaxLength(50);
      entity.Property(e => e.Employeetypename)
          .HasMaxLength(50)
          .HasColumnName("EMPLOYEETYPENAME");
      entity.Property(e => e.IsContract).HasDefaultValue(false);
    });

    modelBuilder.Entity<Employment>(entity =>
    {
      entity.HasKey(e => e.HrrecordId);

      entity.Property(e => e.HrrecordId)
          .ValueGeneratedNever()
          .HasColumnName("HRRecordId");
      entity.Property(e => e.AttendanceCardNo).HasMaxLength(50);
      entity.Property(e => e.BankAccountNo).HasMaxLength(50);
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.ContactAddress)
          .HasMaxLength(300)
          .IsUnicode(false);
      entity.Property(e => e.Designationid).HasColumnName("DESIGNATIONID");
      entity.Property(e => e.EmergencyContactName).HasMaxLength(250);
      entity.Property(e => e.EmergencyContactNo).HasMaxLength(250);
      entity.Property(e => e.EmployeeId).HasMaxLength(50);
      entity.Property(e => e.Experience)
          .HasMaxLength(2000)
          .HasColumnName("EXPERIENCE");
      entity.Property(e => e.FuncId).HasColumnName("Func_Id");
      entity.Property(e => e.Gpfno)
          .HasMaxLength(100)
          .HasColumnName("GPFNO");
      entity.Property(e => e.IsOteligible).HasColumnName("IsOTEligible");
      entity.Property(e => e.Joiningpost).HasColumnName("JOININGPOST");
      entity.Property(e => e.LastUpdateDate).HasColumnType("smalldatetime");
      entity.Property(e => e.OfficialEmail).HasMaxLength(250);
      entity.Property(e => e.Reportdepid).HasColumnName("REPORTDEPID");
      entity.Property(e => e.SeparationRemarks).HasMaxLength(250);
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
      entity.Property(e => e.StartDate).HasColumnType("datetime");
      entity.Property(e => e.TelephoneExtension).HasMaxLength(50);
      entity.Property(e => e.TinNumber).HasMaxLength(50);
    });

    modelBuilder.Entity<GroupMember>(entity =>
    {
      entity.HasKey(e => new { e.GroupId, e.UserId });

      entity.Property(e => e.GroupOption).HasMaxLength(50);
    });

    modelBuilder.Entity<GroupPermission>(entity =>
    {
      entity.HasKey(e => e.PermissionId);

      entity.Property(e => e.Groupid).HasColumnName("GROUPID");
      entity.Property(e => e.Parentpermission).HasColumnName("PARENTPERMISSION");
      entity.Property(e => e.Permissiontablename)
          .HasMaxLength(50)
          .HasColumnName("PERMISSIONTABLENAME");
      entity.Property(e => e.Referenceid).HasColumnName("REFERENCEID");
    });

    modelBuilder.Entity<Groups>(entity =>
    {
      entity.HasKey(e => e.GroupId);

      entity.Property(e => e.GroupName).HasMaxLength(100);
    });

    modelBuilder.Entity<Holiday>(entity =>
    {
      entity.Property(e => e.HolidayId).HasColumnName("HolidayID");
      entity.Property(e => e.DayName)
          .HasMaxLength(20)
          .IsUnicode(false);
      entity.Property(e => e.Description).HasMaxLength(500);
      entity.Property(e => e.Lastupdatedate)
          .HasColumnType("datetime")
          .HasColumnName("LASTUPDATEDATE");
      entity.Property(e => e.MonthName)
          .HasMaxLength(50)
          .IsUnicode(false);
      entity.Property(e => e.Shiftid).HasColumnName("SHIFTID");
    });

    modelBuilder.Entity<Menu>(entity =>
    {
      entity.Property(e => e.MenuId).HasColumnName("MenuID");
      entity.Property(e => e.MenuName).HasMaxLength(50);
      entity.Property(e => e.MenuPath).HasMaxLength(200);
      entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
      entity.Property(e => e.Sororder).HasColumnName("SORORDER");
      entity.Property(e => e.Todo).HasColumnName("TODO");
    });

    modelBuilder.Entity<Module>(entity =>
    {
      entity.Property(e => e.ModuleName).HasMaxLength(50);
    });

    modelBuilder.Entity<PasswordHistory>(entity =>
    {
      entity.HasKey(e => e.HistoryId);

      entity.Property(e => e.OldPassword).HasMaxLength(50);
      entity.Property(e => e.PasswordChangeDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<ReportBuilder>(entity =>
    {
      entity.HasKey(e => e.ReportHeaderId);

      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.OrderByColumn)
          .HasMaxLength(500)
          .IsUnicode(false);
      entity.Property(e => e.ReportHeader).HasMaxLength(250);
      entity.Property(e => e.ReportTitle).HasMaxLength(250);
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<SystemSettings>(entity =>
    {
      entity.HasKey(e => e.SettingsId);

      entity.Property(e => e.CasualWorkerAmount).HasColumnType("decimal(18, 0)");
      entity.Property(e => e.CustomStatusForNoOutPunch).HasMaxLength(250);
      entity.Property(e => e.IsOtcalculateOnHolidayWekend).HasColumnName("IsOTCalculateOnHolidayWekend");
      entity.Property(e => e.IsWebLoginEnable)
          .HasDefaultValue(0)
          .HasComment("0=Disable,1=Enable");
      entity.Property(e => e.Language).HasMaxLength(50);
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.OdbcClientList).HasComment("0=Native SQL, 1=ODBC");
      entity.Property(e => e.PassResetBy).HasComment("1=SysAdmin, 2=User");
      entity.Property(e => e.PassType).HasComment("1=Alphanumeric, 2=Alphabetic, 3=Numeric");
      entity.Property(e => e.ResetPass).HasMaxLength(100);
      entity.Property(e => e.Theme).HasMaxLength(50);
    });

    modelBuilder.Entity<Thana>(entity =>
    {
      entity.Property(e => e.ThanaCode).HasMaxLength(50);
      entity.Property(e => e.ThanaName).HasMaxLength(100);
      entity.Property(e => e.ThanaNameBn)
          .HasMaxLength(100)
          .HasColumnName("ThanaName_bn");
    });

    modelBuilder.Entity<Timesheet>(entity =>
    {
      entity.ToTable("TIMESHEET");

      entity.Property(e => e.Timesheetid).HasColumnName("TIMESHEETID");
      entity.Property(e => e.ApproveDate)
          .HasColumnType("datetime")
          .HasColumnName("APPROVE_DATE");
      entity.Property(e => e.ApproveRhRrecordid).HasColumnName("APPROVE_RH_RRECORDID");
      entity.Property(e => e.BillStatus).HasColumnName("BILL_STATUS");
      entity.Property(e => e.BillableLogHour).HasColumnName("BILLABLE_LOG_HOUR");
      entity.Property(e => e.Hrrecordid).HasColumnName("HRRECORDID");
      entity.Property(e => e.Isapprove).HasColumnName("ISAPPROVE");
      entity.Property(e => e.LogEntryDate)
          .HasColumnType("datetime")
          .HasColumnName("LOG_ENTRY_DATE");
      entity.Property(e => e.NoBillableLogHour).HasColumnName("NO_BILLABLE_LOG_HOUR");
      entity.Property(e => e.Projectid).HasColumnName("PROJECTID");
      entity.Property(e => e.Taskid).HasColumnName("TASKID");
      entity.Property(e => e.WorkedLogHour).HasColumnName("WORKED_LOG_HOUR");
      entity.Property(e => e.WorkingLogDate)
          .HasColumnType("datetime")
          .HasColumnName("WORKING_LOG_DATE");
    });

    modelBuilder.Entity<Users>(entity =>
    {
      entity.HasKey(e => e.UserId).HasName("PK_User");

      entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
      entity.Property(e => e.CreatedDate).HasColumnType("datetime");
      entity.Property(e => e.EmployeeId).HasComment("EmployeeId As HrRecordId");
      entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
      entity.Property(e => e.LastUpdateDate).HasColumnType("datetime");
      entity.Property(e => e.LoginId).HasMaxLength(50);
      entity.Property(e => e.Password).HasMaxLength(100);
      entity.Property(e => e.Theme)
          .HasMaxLength(100)
          .HasColumnName("THEME");
      entity.Property(e => e.UserName).HasMaxLength(500);
    });

    modelBuilder.Entity<Wfaction>(entity =>
    {
      entity.ToTable("WFAction");

      entity.Property(e => e.WfactionId).HasColumnName("WFActionId");
      entity.Property(e => e.ActionName).HasMaxLength(50);
      entity.Property(e => e.EmailAlert).HasColumnName("EMAIL_ALERT");
      entity.Property(e => e.SmsAlert).HasColumnName("SMS_ALERT");
      entity.Property(e => e.WfstateId).HasColumnName("WFStateId");
    });

    modelBuilder.Entity<Wfstate>(entity =>
    {
      entity.ToTable("WFState");

      entity.Property(e => e.WfstateId).HasColumnName("WFStateId");
      entity.Property(e => e.Sequence).HasColumnName("sequence");
      entity.Property(e => e.StateName).HasMaxLength(50);
    });

    modelBuilder.Entity<TokenBlacklist>(entity =>
    {
      entity.HasNoKey();

      entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
    });

    modelBuilder.Entity<Branch>(entity =>
    {
      entity.Property(e => e.Branchid).HasColumnName("BRANCHID");
      entity.Property(e => e.BranchAddress)
          .HasMaxLength(250)
          .IsUnicode(false);
      entity.Property(e => e.Branchcode)
          .HasMaxLength(50)
          .HasColumnName("BRANCHCODE");
      entity.Property(e => e.Branchdescription)
          .HasMaxLength(2000)
          .HasColumnName("BRANCHDESCRIPTION");
      entity.Property(e => e.Branchname)
          .HasMaxLength(100)
          .HasColumnName("BRANCHNAME");
      entity.Property(e => e.CreateDate).HasColumnType("datetime");
      entity.Property(e => e.UpdateDate).HasColumnType("datetime");
    });


  }


  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
