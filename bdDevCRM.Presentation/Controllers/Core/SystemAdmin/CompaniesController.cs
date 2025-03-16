using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

//[Route("api/[controller]")]
//[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CompaniesController : BaseApiController
{
  private readonly IServiceManager _serviceManager;
  public CompaniesController(IServiceManager serviceManager)
  {
    _serviceManager = serviceManager;
  }

  [HttpGet]
  [Produces("application/json" ,"text/csv")]
  public IActionResult GetCompanies()
  {
    var companies = _serviceManager.Companies.GetAllCompanies(trackChanges: false);
    return companies.Any() ? Ok(companies) : NoContent(); 
  }

  [HttpGet("{id:int}")]
  public IActionResult GetCompany(int id)
  {
    var company = _serviceManager.Companies.GetCompany(id, trackChanges: false);
    return Ok(company);
  }

  [HttpPost]
  public IActionResult CreateCompany([FromBody] CompanyDto companyDto)
  {
    if (companyDto is null)
    {
      return BadRequest("CompanyDot object is null!");
    }

    var createdCompany = _serviceManager.Companies.CreateCompany(companyDto);
    return CreatedAtRoute("CompanyById", new {id = createdCompany.CompanyId}, createdCompany);
  }

  [HttpGet("collection/({ids})", Name = "CompanyCollection")]
  public IActionResult GetCompanCollection(IEnumerable<int> ids)
  {
    var companyEntities = _serviceManager.Companies.GetByIds(ids, trackChanges: false);
    return Ok(companyEntities);
  }



}
