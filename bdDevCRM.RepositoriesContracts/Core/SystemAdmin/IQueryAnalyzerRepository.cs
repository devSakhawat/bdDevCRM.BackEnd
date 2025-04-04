using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface IQueryAnalyzerRepository : IRepositoryBase<ReportBuilder>
{

  Task<IEnumerable<QueryAnalyzerRepositoryDto>> CustomizedReportInfo(bool trackChanges);

}
