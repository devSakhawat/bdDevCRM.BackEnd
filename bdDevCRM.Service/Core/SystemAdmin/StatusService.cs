using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.Core.SystemAdmin;

internal sealed class StatusService : IStatusService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public StatusService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<WfstateDto>> StatusByMenuId(int menuId, bool trackChanges)
  {
    IEnumerable<WfStateRepositoryDto> wfstates = await _repository.WfState.StatusByMenuId(menuId, trackChanges);
    IEnumerable<WfstateDto> wfstatesDto = MyMapper.JsonCloneIEnumerableToList<WfStateRepositoryDto, WfstateDto>(wfstates);
    return wfstatesDto;
  }

  public async Task<IEnumerable<WfActionDto>> ActionsByStatusIdForGroup(int statusId, bool trackChanges)
  {
    IEnumerable<WfActionRepositoryDto> wfActions = await _repository.WfState.ActionsByStatusIdForGroup(statusId, trackChanges);
    IEnumerable<WfActionDto> wfActionsDto = MyMapper.JsonCloneIEnumerableToList<WfActionRepositoryDto, WfActionDto>(wfActions);
    return wfActionsDto;
  }


}
