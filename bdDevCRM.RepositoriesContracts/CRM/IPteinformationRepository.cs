using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IPteinformationRepository : IRepositoryBase<Pteinformation>
{
  Task<IEnumerable<Pteinformation>> GetActivePteinformationsAsync(bool track);
  Task<IEnumerable<Pteinformation>> GetPteinformationsAsync(bool track);
  Task<Pteinformation?> GetPteinformationAsync(int id, bool track);
  Task<IEnumerable<Pteinformation>> GetPteinformationsByApplicantIdAsync(int applicantId, bool track);
  Task<Pteinformation?> GetPteinformationByApplicantIdAsync(int applicantId, bool track);
}