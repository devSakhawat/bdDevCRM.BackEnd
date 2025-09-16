using bdDevCRM.Entities.Entities.CRM;
using bdDevCRM.RepositoriesContracts.CRM;
using bdDevCRM.RepositoryDtos.CRM;
using bdDevCRM.Sql.Context;
using Microsoft.Data.SqlClient;

namespace bdDevCRM.Repositories.CRM;

public sealed class CrmAdditionalDocumentRepository : RepositoryBase<CrmAdditionalDocument>, ICrmAdditionalDocumentRepository
{
  public CrmAdditionalDocumentRepository(CRMContext context) : base(context) { }

  public async Task<IEnumerable<CrmAdditionalDocument>> GetActiveAdditionalDocumentsAsync(bool track) =>
      await ListAsync(c => c.AdditionalDocumentId, track);

  public async Task<IEnumerable<CrmAdditionalDocument>> GetAdditionalDocumentsAsync(bool track) =>
      await ListAsync(c => c.AdditionalDocumentId, track);

  public async Task<CrmAdditionalDocument?> GetAdditionalDocumentAsync(int id, bool track) =>
      await FirstOrDefaultAsync(c => c.AdditionalDocumentId == id, track);

  public async Task<IEnumerable<CrmAdditionalDocument>> GetAdditionalDocumentsByApplicantIdAsync(int applicantId, bool track) =>
      await ListByConditionAsync(x => x.ApplicantId == applicantId, c => c.AdditionalDocumentId, track);


  public async Task<IEnumerable<AdditionalDocumentRepositoryDto>> AdditionalDocumentsByApplicantId(int applicantId)
  {
    string sql = string.Format(@"
     SELECT [AdditionalDocumentId]
      ,[DocumentTitle]
      ,[DocumentPath]
      ,[DocumentName]
      ,[RecordType]
      ,[CreatedDate]
      ,[CreatedBy]
      ,[UpdatedDate]
      ,[UpdatedBy]
      ,[ApplicantId]
      ,doc.FilePath as AttachedDocument
  FROM [dbDevCRM].[dbo].[CrmAdditionalDocument]
  OUTER APPLY(
        Select top 1 * 
        From DmsDocument doc
        where ReferenceEntityType = 'AdditionalDocument'
        and doc.ReferenceEntityId = CrmAdditionalDocument.ApplicantId
        Order by doc.UploadDate desc
    ) doc
WHERE ApplicantId = @ApplicantId", applicantId);

    // Execute the query using RepositoryBase method
    var parameters = new SqlParameter[]
    {
        new SqlParameter("@ApplicantId", applicantId)
    };


    return await ExecuteListQuery<AdditionalDocumentRepositoryDto>(sql, parameters);
  }
}