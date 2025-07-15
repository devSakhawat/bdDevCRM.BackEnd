namespace bdDevCRM.Shared.DataTransferObjects.CRM;

/// <summary>
/// Education And English Language Form Section DTOs
/// </summary>
public class EducationInformationDto
{
  public EducationDetailsDto? EducationDetails { get; set; }
  public IELTSInformationDto? IeltsInformation { get; set; }
  public TOEFLInformationDto? ToeflInformation { get; set; }
  public PTEInformationDto? PteInformation { get; set; }
  public GMATInformationDto? GmatInformation { get; set; }
  public OTHERSInformationDto? OthersInformation { get; set; }
  public WorkExperienceDto? WorkExperience { get; set; }
}