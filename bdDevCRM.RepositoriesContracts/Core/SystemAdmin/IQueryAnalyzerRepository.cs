using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface IQueryAnalyzerRepository : IRepositoryBase<ReportBuilder>
{

  Task<IEnumerable<QueryAnalyzerRepositoryDto>> CustomizedReportInfo(bool trackChanges);

  Task<IEnumerable<QueryAnalyzerRepositoryDto>> CustomizedReportByPermission(Users currentUser, string condition, bool trackChanges);

  Task<IEnumerable<QueryAnalyzerRepositoryDto>> GroupPermissionForQueryAnalyzerReport(Users currentUser);

}
