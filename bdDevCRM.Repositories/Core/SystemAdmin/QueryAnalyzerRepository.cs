using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class QueryAnalyzerRepository : RepositoryBase<ReportBuilder>, IQueryAnalyzerRepository
{
  private const string SELECT_GROUPPERMISSION_BY_GROUPID = "Select * from GROUPPERMISSION where GROUPID = {0}";

  public QueryAnalyzerRepository(CRMContext context) : base(context) { }

  // summary data must be returned in a specific format like this.
  public async Task<IEnumerable<QueryAnalyzerRepositoryDto>> CustomizedReportInfo(bool trackChanges)
  {
    string queryAnalyzerQuery = $"SELECT tblQueryAnalyzer.ReportHeader ,tblQueryAnalyzer.ReportTitle ,tblQueryAnalyzer.ReportHeaderId\r\nFROM (\r\n\tSELECT ReportHeader + ' (Report)' AS ReportHeader ,ReportTitle ,ReportHeaderId ,1 AS SortOrder\r\n\tFROM ReportBuilder\r\n\tWHERE IsActive = 1 AND QueryType = 1\r\n\t\r\n\tUNION ALL\r\n\t\r\n\tSELECT ReportHeader + ' (Document)' AS ReportHeader ,ReportTitle ,ReportHeaderId ,2 AS SortOrder\r\n\tFROM ReportBuilder\r\n\tWHERE IsActive = 1 AND QueryType = 4\r\n\t) tblQueryAnalyzer\r\nORDER BY tblQueryAnalyzer.SortOrder ,ReportHeader";
    IEnumerable<QueryAnalyzerRepositoryDto> queryAnalyzers = await ExecuteListQuery<QueryAnalyzerRepositoryDto>(queryAnalyzerQuery);
    return queryAnalyzers;
  }





}
