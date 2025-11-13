using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Services.Core.SystemAdmin;


internal sealed class QueryAnalyzerService : IQueryAnalyzerService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public QueryAnalyzerService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }



  public async Task<IEnumerable<QueryAnalyzerDto>> CustomizedReportInfo(bool trackChanges)
  {
    IEnumerable<QueryAnalyzerRepositoryDto> queryAnalyzers = await _repository.QueryAnalyzers.CustomizedReportInfo(trackChanges);
    IEnumerable<QueryAnalyzerDto> queryAnalyzersDto = MyMapper.JsonCloneIEnumerableToList<QueryAnalyzerRepositoryDto, QueryAnalyzerDto>(queryAnalyzers);

    return queryAnalyzersDto;
  }



  public async Task<IEnumerable<QueryAnalyzerDto>> CustomizedReportByPermission(UsersDto currentUser, bool trackChanges)
  {
    if (currentUser == null) { return null; }
    string condition = "";

    Users usersEntity = await _repository.Users.GetByIdAsync(predicate: x => x.UserId.Equals(currentUser.UserId), trackChanges);
    if (usersEntity != null && usersEntity.IsSystemUser == true)
    {
      condition = "";
    }
    else
    {
      var groupPermissionList = await _repository.QueryAnalyzers.GroupPermissionForQueryAnalyzerReport(usersEntity);
      var ids = string.Join(",", groupPermissionList.Select(mn => mn.ReportHeaderId));

      condition = string.IsNullOrEmpty(ids) ? string.Empty : $"WHERE ReportHeaderId IN ({ids})";
    }
    IEnumerable<QueryAnalyzerRepositoryDto> queryAnalyzers = await _repository.QueryAnalyzers.CustomizedReportByPermission(usersEntity, condition, trackChanges);
    IEnumerable<QueryAnalyzerDto> queryAnalyzersDto = MyMapper.JsonCloneIEnumerableToList<QueryAnalyzerRepositoryDto, QueryAnalyzerDto>(queryAnalyzers);

    return queryAnalyzersDto;
  }



}
