using bdDevCRM.Entities.Entities.System;

using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.RepositoryDtos.Core.HR;
using bdDevCRM.ServiceContract.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.HR;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;

namespace bdDevCRM.Service.Core.HR;


internal sealed class DepartmentService : IDepartmentService
{
  private const string SELECT_DEPARTMENT_BY_COMPANYID =
            "Select Department.* from Department inner join CompanyDepartmentMap on CompanyDepartmentMap.DepartmentId =Department.DepartmentId  where CompanyId = {0} and IsActive=1 {1} order by DepartmentName asc";

  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public DepartmentService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }



  // get Department types with id, name and code
  public async Task<IEnumerable<DepartmentDto>> DepartmentesByCompanyIdForCombo(int companyId, UsersDto user)
  {

    if (companyId < 0 || companyId == null)
    {
      throw new IdParametersBadRequestException();
    }

    string query = string.Format(SELECT_DEPARTMENT_BY_COMPANYID, companyId, "");

    IEnumerable<DepartmentRepositoryDto> queryResult = await _repository.departments.ExecuteListQuery<DepartmentRepositoryDto>(query, null);
    IEnumerable<DepartmentDto> result = Enumerable.Empty<DepartmentDto>();

    if (queryResult.Count() > 0)
    {
      result = MyMapper.JsonCloneIEnumerableToList<DepartmentRepositoryDto, DepartmentDto>(queryResult);
    }

    return result;
  }


}
