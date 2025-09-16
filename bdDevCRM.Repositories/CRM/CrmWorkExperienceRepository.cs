using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.RepositoryDtos.CRM;
using bdDevCRM.Sql.Context;
using Microsoft.Data.SqlClient;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmWorkExperienceRepository : RepositoryBase<CrmWorkExperience>, ICrmWorkExperienceRepository
{
  public CrmWorkExperienceRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmWorkExperience>> GetActiveWorkExperiencesAsync(bool track) =>
      await ListAsync(c => c.WorkExperienceId, track);

  public async Task<IEnumerable<CrmWorkExperience>> GetWorkExperiencesAsync(bool track) =>
      await ListAsync(c => c.WorkExperienceId, track);

  public async Task<CrmWorkExperience?> GetWorkExperienceAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.WorkExperienceId == id, track);

  public async Task<IEnumerable<CrmWorkExperience>> GetWorkExperiencesByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.WorkExperienceId, track);

  public async Task<IEnumerable<WorkExperienceHistoryRepositoryDto>> WorkExperiencesByApplicantId(int applicantId)
  {
    string sql = string.Format(@"
     SELECT [WorkExperienceId]
      ,[ApplicantId]
      ,[NameOfEmployer]
      ,[Position]
      ,[StartDate]
      ,[EndDate]
      ,[Period]
      ,[MainResponsibility]
      ,doc.FilePath as ScannedCopyPath
      ,doc.FileName as ScannedCopyFileName
      ,[DocumentName]
      ,[CreatedDate]
      ,[CreatedBy]
      ,[UpdatedDate]
      ,[UpdatedBy]
  FROM [dbDevCRM].[dbo].[CrmWorkExperience]
   OUTER APPLY(
        Select top 1 * 
        From DmsDocument doc
        where ReferenceEntityType = 'WorkExperience'
        and doc.ReferenceEntityId = CrmWorkExperience.ApplicantId
        Order by doc.UploadDate desc
    ) doc
WHERE ApplicantId = @ApplicantId", applicantId);

    // Execute the query using RepositoryBase method
    var parameters = new SqlParameter[]
    {
        new SqlParameter("@ApplicantId", applicantId)
    };


    return await ExecuteListQuery<WorkExperienceHistoryRepositoryDto>(sql, parameters);
  }
}