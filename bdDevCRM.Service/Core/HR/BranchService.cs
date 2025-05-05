using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.RepositoryDtos.Core.HR;
using bdDevCRM.ServiceContract.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.Core.HR;


internal sealed class BranchService : IBranchService
{
  private const string SELECT_BRANCH_BY_COMPANYID =
    "Select Branch.* from Branch inner join CompanyLocationMap on CompanyLocationMap.BranchId=Branch.BranchId where CompanyId = {0}{1} order by BranchName asc";


  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public BranchService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }



  // get Branch types with id, name and code
  public async Task<IEnumerable<BranchDto>> BranchesByCompanyIdForCombo(int companyId , UsersDto user)
  {

    if (companyId < 0 || companyId == null)
    {
      throw new IdParametersBadRequestException();
    }

    string query = string.Format(SELECT_BRANCH_BY_COMPANYID, companyId, "");

    IEnumerable<BranchRepositoryDto> queryResult = await _repository.Branches.ExecuteListQuery<BranchRepositoryDto>(query, null);
    IEnumerable<BranchDto> result = Enumerable.Empty<BranchDto>();

    if (queryResult.Count() > 0)
    {
      result = MyMapper.JsonCloneIEnumerableToList<BranchRepositoryDto, BranchDto>(queryResult);
    }

    return result;
  }


}
