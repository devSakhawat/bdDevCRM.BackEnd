using bdDevCRM.Entities.Exceptions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Region;
using System.Threading.Tasks;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

public class CompaniesController : BaseApiController
{
  private readonly IServiceManager _serviceManager;
  public CompaniesController(IServiceManager serviceManager)
  {
    _serviceManager = serviceManager;
  }

  [HttpGet(RouteConstants.Companies)]
  [Produces("application/json", "text/csv")]
  public IActionResult GetCompanies()
  {
    var companies = _serviceManager.Companies.GetAllCompanies(trackChanges: false);
    return companies.Any() ? Ok(companies) : NoContent();
  }

  [HttpGet(RouteConstants.ReadCompany)]
  public IActionResult GetCompany([FromQuery] int id)
  {
    var company = _serviceManager.Companies.GetCompany(id, trackChanges: false);
    return Ok(company);
  }

  [HttpPost(RouteConstants.CreateCompany)]
  public IActionResult CreateCompany([FromBody] CompanyDto companyDto)
  {
    if (companyDto is null)
    {
      return BadRequest("CompanyDot object is null!");
    }

    var createdCompany = _serviceManager.Companies.CreateCompany(companyDto);
    return CreatedAtRoute("CompanyById", new { id = createdCompany.CompanyId }, createdCompany);
  }

  [HttpGet("collection/({ids})", Name = "CompanyCollection")]
  public IActionResult GetCompanCollection(IEnumerable<int> ids)
  {
    var companyEntities = _serviceManager.Companies.GetByIds(ids, trackChanges: false);
    return Ok(companyEntities);
  }

  [HttpGet(RouteConstants.GetMotherCompany)]
  public async Task<IActionResult> GetMotherCompany()
  {
    // from claim.
    var userId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
    // userId : which key is reponsible to when cache was created .
    // get user from cache. if cache is not founded by key then it will thow Unauthorized exception with 401 status code.
    UsersDto user = _serviceManager.GetCache<UsersDto>(userId);
    // get hr record id from user.
    int companyId = 0;

    if (user.HrRecordId == 0 || user.HrRecordId == null) throw new IdParametersBadRequestException();
    companyId = (int)user.CompanyId;

    IEnumerable<CompanyDto> companyList = await _serviceManager.Companies.GetMotherCompany(companyId, user);


    return Ok(companyList);
  }

}

