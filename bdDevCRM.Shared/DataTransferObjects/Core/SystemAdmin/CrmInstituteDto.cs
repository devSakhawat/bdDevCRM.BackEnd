using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

public class CrmInstituteDto
{
  // === Primary Keys / Foreign Keys ===
  public int InstituteId { get; set; }

  // === Basic Info ===
  [Required, StringLength(200)]
  public string InstituteName { get; set; }
  [StringLength(100)]
  public string? InstituteCode { get; set; }
  [EmailAddress]
  public string? InstituteEmail { get; set; }
  [StringLength(300)]
  public string? InstituteAddress { get; set; }
  [Phone]
  public string? InstitutePhoneNo { get; set; }
  [Phone]
  public string? InstituteMobileNo { get; set; }
  public string? Campus { get; set; }
  [Url]
  public string? Website { get; set; }

  // === Financial / Visa ===
  public decimal? MonthlyLivingCost { get; set; }

  public string? FundsRequirementforVisa { get; set; }

  public decimal? ApplicationFee { get; set; }

  // === Language & Academic ===
  public bool? IsLanguageMandatory { get; set; }
  public string? LanguagesRequirement { get; set; }

  // === Descriptive Info ===
  public string? InstitutionalBenefits { get; set; }
  public string? PartTimeWorkDetails { get; set; }
  public string? ScholarshipsPolicy { get; set; }
  public string? InstitutionStatusNotes { get; set; }

  // === File Paths / URLs ===
  public string? InstitutionLogo { get; set; }
  public string? InstitutionProspectus { get; set; }

  // === Status ===
  public bool? Status { get; set; }



  // === Dropdown Item ===
  public int CountryId { get; set; }
  //[Required(ErrorMessage = "InstituteType is required")]
  public int? InstituteTypeId { get; set; }
  public int? CurrencyId { get; set; }


  // === Dropdown (Name) ===
  public string CountryName { get; set; }
  public string InstituteType { get; set; }
  public string CurrencyName { get; set; }

  // RowIndex for grid
  public int RowIndex { get; set; }

}

