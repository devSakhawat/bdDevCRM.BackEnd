using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IQueryAnalyzerService
{
  Task<IEnumerable<QueryAnalyzerDto>> CustomizedReportInfo(bool trackChanges);
}
