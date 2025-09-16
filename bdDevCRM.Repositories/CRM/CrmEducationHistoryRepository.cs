using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.RepositoryDtos.CRM;
using bdDevCRM.Sql.Context;
using Microsoft.Data.SqlClient;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmEducationHistoryRepository : RepositoryBase<CrmEducationHistory>, ICrmEducationHistoryRepository
{
  public CrmEducationHistoryRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmEducationHistory>> GetActiveEducationHistoriesAsync(bool track) =>
      await ListAsync(c => c.EducationHistoryId, track);

  public async Task<IEnumerable<CrmEducationHistory>> GetEducationHistoriesAsync(bool track) =>
      await ListAsync(c => c.EducationHistoryId, track);

  public async Task<CrmEducationHistory?> GetEducationHistoryAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.EducationHistoryId == id, track);

  public async Task<IEnumerable<CrmEducationHistory>> GetEducationHistoriesByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.EducationHistoryId, track);

  public async Task<CrmEducationHistory?> GetEducationHistoryByInstitutionAsync(string institution, bool track) =>
      await FirstOrDefaultAsync(c => c.Institution != null && c.Institution.ToLower() == institution.ToLower(), track);

  public async Task<IEnumerable<EducationHistoryRepositoryDto>> EducationHistoryByApplicantId(int applicantId)
  {
    string sql = string.Format(@"
     SELECT [EducationHistoryId]
      ,[ApplicantId]
      ,[Institution]
      ,[Qualification]
      ,[PassingYear]
      ,[Grade]
      ,doc.FilePath as [DocumentPath]
      ,[DocumentName]
      ,[CreatedDate]
      ,[CreatedBy]
      ,[UpdatedDate]
      ,[UpdatedBy]
  FROM [dbDevCRM].[dbo].[CrmEducationHistory]
  OUTER APPLY(
        Select top 1 * 
        From DmsDocument doc
        where ReferenceEntityType = 'EducationHistory'
        and doc.ReferenceEntityId = CrmEducationHistory.ApplicantId
        Order by doc.UploadDate desc
    ) doc
WHERE ApplicantId = @ApplicantId", applicantId);

    // Execute the query using RepositoryBase method
    var parameters = new SqlParameter[]
    {
        new SqlParameter("@ApplicantId", applicantId)
    };


    return await ExecuteListQuery<EducationHistoryRepositoryDto>(sql, parameters);
  }
}