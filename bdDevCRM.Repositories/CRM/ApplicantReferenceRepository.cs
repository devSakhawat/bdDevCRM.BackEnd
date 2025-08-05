using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmApplicantReferenceRepository : RepositoryBase<CrmApplicantReference>, ICrmApplicantReferenceRepository
{
  public CrmApplicantReferenceRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmApplicantReference>> GetActiveApplicantReferencesAsync(bool track) =>
      await ListAsync(c => c.ApplicantReferenceId, track);

  public async Task<IEnumerable<CrmApplicantReference>> GetApplicantReferencesAsync(bool track) =>
      await ListAsync(c => c.ApplicantReferenceId, track);

  public async Task<CrmApplicantReference?> GetApplicantReferenceAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantReferenceId == id, track);

  public async Task<IEnumerable<CrmApplicantReference>> GetApplicantReferencesByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.ApplicantReferenceId, track);
}