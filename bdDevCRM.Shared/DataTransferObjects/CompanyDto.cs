namespace bdDevCRM.Shared.DataTransferObjects;

[Serializable]
public class CompanyDto
{
  public int CompanyId { get; init; }
  public string? CompanyCode { get; init; }
  public string CompanyName { get; init; } = string.Empty;
  public string? Address { get; init; }
  public string? Phone { get; init; }
  public string? Fax { get; init; }
  public string? Email { get; init; }
  public string? FullLogoPath { get; init; }
  public string? PrimaryContact { get; init; }
  public int Flag { get; init; }
  public int FiscalYearStart { get; init; }
  public int? MotherId { get; init; }
  public int? IsCostCentre { get; init; }
  public int? IsActive { get; init; }
  public DateOnly? GratuityStartDate { get; init; }
  public string? FullLogoPathForReport { get; init; }
  public string? CompanyTin { get; init; }
  public bool? IsPfApplicable { get; init; }
  public bool? IsEwfApplicable { get; init; }
  public int? IsPfApplicabe { get; init; }
  public int? IsEwfApplicabe { get; init; }
  public string? CompanyAlias { get; init; }
  public string? CompanyZone { get; init; }
  public string? CompanyCircle { get; init; }
  public int? IsCompanyContributionDisable { get; init; }
  public int? CreateBy { get; init; }
  public DateTime? CreateDate { get; init; }
  public int? UpdateBy { get; init; }
  public DateTime? UpdateDate { get; init; }
  public int? IsElautoAddedForCurrentYear { get; init; }
  public int? IsRosterAutoCarryForward { get; init; }
  public string? LetterHeader { get; init; }
  public string? LetterFooter { get; init; }
  public string? CompanyRegisterNo { get; init; }
  public int? CompanySortOrder { get; init; }
  public int? CompanyAccessGroupNo { get; init; }
  public int? IsSentGreetingsOrWishNotification { get; init; }
  public int? IsNotifyForNextMonIncEligible { get; init; }
}
