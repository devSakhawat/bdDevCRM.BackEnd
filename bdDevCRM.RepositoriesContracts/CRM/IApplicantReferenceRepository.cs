using bdDevCRM.Entities.Entities.CRM;

namespace bdDevCRM.RepositoriesContracts.CRM;

public interface IApplicantReferenceRepository : IRepositoryBase<ApplicantReference>
{
  Task<IEnumerable<ApplicantReference>> GetActiveApplicantReferencesAsync(bool track);
  Task<IEnumerable<ApplicantReference>> GetApplicantReferencesAsync(bool track);
  Task<ApplicantReference?> GetApplicantReferenceAsync(int id, bool track);
  Task<IEnumerable<ApplicantReference>> GetApplicantReferencesByApplicantIdAsync(int applicantId, bool track);
}