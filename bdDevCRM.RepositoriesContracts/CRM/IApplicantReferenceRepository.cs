using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface ICrmApplicantReferenceRepository : IRepositoryBase<CrmApplicantReference>
{
  Task<IEnumerable<CrmApplicantReference>> GetActiveApplicantReferencesAsync(bool track);
  Task<IEnumerable<CrmApplicantReference>> GetApplicantReferencesAsync(bool track);
  Task<CrmApplicantReference?> GetApplicantReferenceAsync(int id, bool track);
  Task<IEnumerable<CrmApplicantReference>> GetApplicantReferencesByApplicantIdAsync(int applicantId, bool track);
}