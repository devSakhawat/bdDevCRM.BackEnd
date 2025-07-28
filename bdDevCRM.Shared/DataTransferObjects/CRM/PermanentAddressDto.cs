using System;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

public class PermanentAddressDto
{
  public int PermanentAddressId { get; set; }

  public int ApplicantId { get; set; }

  public string? Address { get; set; }

  public string? City { get; set; }

  public string? State { get; set; }

  public int CountryId { get; set; }

  public string? CountryName { get; set; }

  public string? PostalCode { get; set; }

  public DateTime CreatedDate { get; set; }

  public int CreatedBy { get; set; }

  public DateTime? UpdatedDate { get; set; }

  public int? UpdatedBy { get; set; }

  // Premanent Full Address
  public string PremanentFullAddress => $"{Address} {City} {CountryName} {PostalCode}".Trim();
}