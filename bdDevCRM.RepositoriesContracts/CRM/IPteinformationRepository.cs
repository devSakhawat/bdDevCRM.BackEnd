using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IPTEInformationRepository : IRepositoryBase<PTEInformation>
{
  Task<IEnumerable<PTEInformation>> GetActivePteinformationsAsync(bool track);
  Task<IEnumerable<PTEInformation>> GetPteinformationsAsync(bool track);
  Task<PTEInformation?> GetPteinformationAsync(int id, bool track);
  Task<IEnumerable<PTEInformation>> GetPteinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<PTEInformation?> GetPteinformationByApplicantIdAsync(int applicantId, bool track);
}