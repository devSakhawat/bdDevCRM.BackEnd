using bdDevCRM.Entities.CRMGrid;
using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Services.Core.SystemAdmin;


internal sealed class GroupService : IGroupService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public GroupService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  /// <summary>
  /// Menu crud
  /// </summary>
  /// <param name="trackChanges"></param>
  /// <param name="options"></param>
  /// <returns></returns>
  public async Task<GridEntity<GroupDto>> GroupSummary(bool trackChanges, CRMGridOptions options)
  {
    string query = "Select * from Groups";
    string orderBy = "GroupName asc";
    var gridEntity = await _repository.Groups.GridData<GroupDto>(query, options , orderBy , "");

    if (gridEntity.Items.Count == 0) throw new GenericListNotFoundException("Group");
    return gridEntity;
  }
}
