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

    string query = string.Format(@"
select Distinct ReferenceId,ReferenceType,ParentReference,ChiledParentReference 
from AccessRestriction 
where (HrRecordId = {0} or GroupId in (
	Select GroupId 
	from GroupMember
	inner join Users on Users.UserId = GroupMember.UserId
	inner join Employment on Employment.HRRecordId = Users.EmployeeId
	where HRRecordId = {0}
	)
) and ReferenceType=2 and ParentReference = {1}" , user.HrRecordId, companyId);

    IEnumerable<BranchRepositoryDto> queryResult = await _repository.Branches.ExecuteListQuery<BranchRepositoryDto>(query, null);
    IEnumerable<BranchDto> result = Enumerable.Empty<BranchDto>();
    if (queryResult == null)
    {
      return new List<BranchDto>();
    }
    else
    {
      result = MyMapper.JsonCloneIEnumerableToList<BranchRepositoryDto, BranchDto>(queryResult);
    }
    return result;
  }


}
