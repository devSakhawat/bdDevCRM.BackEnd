using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.CRM;

public sealed class ApplicantReferenceRepository : RepositoryBase<ApplicantReference>, IApplicantReferenceRepository
{
  public ApplicantReferenceRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<ApplicantReference>> GetActiveApplicantReferencesAsync(bool track) =>
      await ListAsync(c => c.ApplicantReferenceId, track);

  public async Task<IEnumerable<ApplicantReference>> GetApplicantReferencesAsync(bool track) =>
      await ListAsync(c => c.ApplicantReferenceId, track);

  public async Task<ApplicantReference?> GetApplicantReferenceAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.ApplicantReferenceId == id, track);

  public async Task<IEnumerable<ApplicantReference>> GetApplicantReferencesByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.ApplicantReferenceId, track);
}