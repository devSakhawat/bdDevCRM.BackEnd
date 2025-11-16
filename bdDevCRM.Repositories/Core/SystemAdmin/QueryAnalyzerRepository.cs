using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
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
  
  public async Task<IEnumerable<QueryAnalyzerRepositoryDto>> CustomizedReportByPermission(Users currentUser ,string condition ,bool trackChanges)
  {
    string queryAnalyzerQuery = string.Format(@"SELECT t.ReportHeader ,t.ReportTitle ,t.ReportHeaderId
FROM (
	SELECT ReportHeader + ' (Report)' AS ReportHeader ,ReportTitle ,ReportHeaderId ,1 AS SortOrder
	FROM ReportBuilder
	WHERE IsActive = 1 AND QueryType = 1
	
	UNION ALL
	
	SELECT ReportHeader + ' (Document)' AS ReportHeader ,ReportTitle ,ReportHeaderId ,2 AS SortOrder
	FROM ReportBuilder
	WHERE IsActive = 1 AND QueryType = 4
	) T
{0}
ORDER BY t.SortOrder ,ReportHeader", condition);
   
    IEnumerable<QueryAnalyzerRepositoryDto> queryAnalyzers = await ExecuteListQuery<QueryAnalyzerRepositoryDto>(queryAnalyzerQuery);
    return queryAnalyzers;
  }

  public async Task<IEnumerable<QueryAnalyzerRepositoryDto>> GroupPermissionForQueryAnalyzerReport(Users currentUser)
  {
    string queryAnalyzerQuery = string.Format(@"SELECT DISTINCT REFERENCEID AS ReportHeaderId
FROM GroupPermission
INNER JOIN GroupMember ON GroupMember.GroupId = GroupPermission.GROUPID
WHERE UserId = {0} AND PERMISSIONTABLENAME = 'Customized Report'", currentUser.UserId);
    var groupPermissionList = await ExecuteListQuery<QueryAnalyzerRepositoryDto>(queryAnalyzerQuery);
    return groupPermissionList;
  }
}
