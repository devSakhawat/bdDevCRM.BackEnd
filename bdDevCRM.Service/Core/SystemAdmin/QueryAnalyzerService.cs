using bdDevCRM.Entities.CRMGrid.GRID;

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


}
