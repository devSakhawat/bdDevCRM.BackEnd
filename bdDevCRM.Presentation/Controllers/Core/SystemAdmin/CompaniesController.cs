using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

/// <summary>
/// Controller for managing companies
/// All methods require authentication via [AuthenticatedUser] attribute
/// </summary>
[AuthenticatedUser] // ✅ Controller-level authentication
public class CompaniesController : BaseApiController
{
  //private readonly IServiceManager _serviceManager;

  public CompaniesController(IServiceManager serviceManager) : base(serviceManager)
  {
    //_serviceManager = serviceManager;
  }

  /// <summary>
  /// Retrieves all companies
  /// </summary>
  /// <returns>List of companies</returns>
  [HttpGet(RouteConstants.Companies)]
  [Produces("application/json", "text/csv")]
  public IActionResult GetCompanies()
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Execute business logic
    var companies = _serviceManager.Companies.GetAllCompanies(trackChanges: false);

    // Return standardized response
    if (companies == null || !companies.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<CompanyDto>>("No companies found"));

    return Ok(ResponseHelper.Success(companies, "Companies retrieved successfully"));
  }

  /// <summary>
  /// Retrieves a specific company by ID
  /// </summary>
  /// <param name="id">Company ID</param>
  /// <returns>Company details</returns>
  [HttpGet(RouteConstants.ReadCompany)]
  public IActionResult GetCompany([FromQuery] int id)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (id <= 0)
      throw new IdParametersBadRequestException();

    // Execute business logic
    var company = _serviceManager.Companies.GetCompany(id, trackChanges: false);

    // Return standardized response
    return Ok(ResponseHelper.Success(company, "Company retrieved successfully"));
  }

  /// <summary>
  /// Creates a new company
  /// </summary>
  /// <param name="companyDto">Company data to create</param>
  /// <returns>Created company record</returns>
  [HttpPost(RouteConstants.CreateCompany)]
  public IActionResult CreateCompany([FromBody] CompanyDto companyDto)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (companyDto is null)
      throw new NullModelBadRequestException("Company data cannot be null");

    // Execute business logic
    var createdCompany = _serviceManager.Companies.CreateCompany(companyDto);

    // Return standardized response
    return CreatedAtRoute("CompanyById", new { id = createdCompany.CompanyId }, 
      ResponseHelper.Created(createdCompany, "Company created successfully"));
  }

  /// <summary>
  /// Retrieves multiple companies by IDs
  /// </summary>
  /// <param name="ids">Collection of company IDs</param>
  /// <returns>List of companies</returns>
  [HttpGet("collection/({ids})", Name = "CompanyCollection")]
  public IActionResult GetCompanCollection(IEnumerable<int> ids)
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate input parameters
    if (ids == null || !ids.Any())
      throw new GenericBadRequestException("Company IDs are required");

    // Execute business logic
    var companyEntities = _serviceManager.Companies.GetByIds(ids, trackChanges: false);

    // Return standardized response
    if (companyEntities == null || !companyEntities.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<CompanyDto>>("No companies found"));

    return Ok(ResponseHelper.Success(companyEntities, "Companies retrieved successfully"));
  }

  /// <summary>
  /// Retrieves mother company for the current user
  /// </summary>
  /// <returns>Mother company details</returns>
  [HttpGet(RouteConstants.GetMotherCompany)]
  public async Task<IActionResult> GetMotherCompany()
  {
    // ✅ Get authenticated user from HttpContext
    var currentUser = HttpContext.GetCurrentUser();
    var userId = HttpContext.GetUserId();

    // Validate user data
    if (currentUser.HrRecordId == 0 || currentUser.HrRecordId == null)
      throw new IdParametersBadRequestException();

    // Get company ID from user
    int companyId = (int)currentUser.CompanyId;

    // Execute business logic
    IEnumerable<CompanyDto> res = await _serviceManager.Companies.GetMotherCompany(companyId, currentUser);

    // Return standardized response
    if (res == null || !res.Any())
      return Ok(ResponseHelper.NoContent<IEnumerable<CompanyDto>>("No Company found"));

    return Ok(ResponseHelper.Success(res, "Companies retrieved successfully"));
  }
}

