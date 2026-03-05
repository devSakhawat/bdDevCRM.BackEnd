using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.AuthorizeAttribiutes;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.CRMGrid.GRID;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

/// <summary>
/// Controller for managing countries
/// All methods require authentication via [AuthorizeUser] attribute
/// </summary>
[AuthorizeUser]
public class CountryController : BaseApiController
{
	//private readonly IServiceManager _serviceManager;
	private readonly IMemoryCache _cache;

	public CountryController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager)
	{
		//_serviceManager = serviceManager;
		_cache = cache;
	}

	/// <summary>
	/// Retrieves all countries for dropdown list
	/// </summary>
	/// <returns>List of countries for dropdown</returns>
	[HttpGet(RouteConstants.CountryDDL)]
	public async Task<IActionResult> CountryDDL()
	{
		var res = await _serviceManager.CrmCountries.GetCountriesDDLAsync(trackChanges: false);

		if (res == null || !res.Any())
		{
			return Ok(ApiResponseHelper.NoContent<IEnumerable<CrmCountryDDL>>("No country found"));
		}

		return Ok(ApiResponseHelper.Success(res, "Country retrieved successfully"));
	}

	/// <summary>
	/// Retrieves paginated summary grid of countries
	/// Grid pagination handled by service layer
	/// </summary>
	/// <param name="options">Grid options for pagination, sorting, and filtering</param>
	/// <returns>Paginated grid of countries</returns>
	[HttpPost(RouteConstants.CountrySummary)]
	public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
	{
		//Get authenticated user from HttpContext
		var currentUser = HttpContext.GetCurrentUser();
		var userId = HttpContext.GetUserId();

		// Validate input parameters
		if (options == null)
			throw new NullModelBadRequestException("CRMGridOptions cannot be null");

		// Execute business logic
		var summaryGrid = await _serviceManager.CrmCountries.SummaryGrid(options);

		// Return standardized response
		if (summaryGrid == null || !summaryGrid.Items.Any())
			return Ok(ApiResponseHelper.NoContent<GridEntity<CrmCountryDto>>("No data found"));

		return Ok(ApiResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
	}

	/// <summary>
	/// Creates a new country record
	/// </summary>
	/// <param name="modelDto">Country data to create</param>
	/// <returns>Operation result message</returns>
	[HttpPost(RouteConstants.CreateCountry)]
	[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
	public async Task<IActionResult> CreateNewRcord([FromBody] CrmCountryDto modelDto)
	{
		//Get authenticated user from HttpContext
		var currentUser = HttpContext.GetCurrentUser();
		var userId = HttpContext.GetUserId();

		// Validate input parameters
		if (modelDto == null)
			throw new NullModelBadRequestException("Country data cannot be null");

		// Execute business logic
		var res = await _serviceManager.CrmCountries.CreateNewRecordAsync(modelDto);

		var links = new List<ResourceLink>
		{
				ApiResponseHelper.SelfLink($"{Request.Path}/{modelDto.CountryId}", "Created country"),
				ApiResponseHelper.RelatedLink("countries", RouteConstants.CountryDDL, "GET", "Get all countries")
		};

		return Ok(ApiResponseHelper.Created<CrmCountryDto>(
			data: modelDto,
			message: "Country created successfully",
			links: links
			)
		);
	}

	/// <summary>
	/// Updates an existing country record
	/// </summary>
	/// <param name="key">Country ID</param>
	/// <param name="modelDto">Updated country data</param>
	/// <returns>Operation result message</returns>
	[HttpPut(RouteConstants.UpdateCountry)]
	[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
	public async Task<IActionResult> UpdateCountry([FromRoute] int key, [FromBody] CrmCountryDto modelDto)
	{
		// Validate route vs body ID mismatch
		if (modelDto.CountryId != 0 && modelDto.CountryId != key)
		{
			throw new IdMismatchBadRequestException("Country", nameof(key), key.ToString());
		}
		modelDto.CountryId = key;
		//if (modelDto == null)
		//	throw new NullModelBadRequestException("Country data cannot be null");

		// Execute business logic
		var res = await _serviceManager.CrmCountries.UpdateNewRecordAsync(key, modelDto, false);
		if (res == OperationMessage.Success)
		{
			var links = new List<ResourceLink>
					{
							ApiResponseHelper.SelfLink($"{Request.Path}", "Updated country"),
							ApiResponseHelper.RelatedLink("countries", RouteConstants.CountryDDL, "GET", "Get all countries")
					};
			return Ok(ApiResponseHelper.Updated<CrmCountryDto>(data: modelDto, message: "Country updated successfully", links: links));
		}
		else
		{
			return Conflict(ApiResponseHelper.Conflict<CrmCountryDto>(res));
		}
	}

	/// <summary>
	/// Deletes a country record
	/// </summary>
	/// <param name="key">Country ID to delete</param>
	/// <param name="modelDto">Country data for validation</param>
	/// <returns>Operation result message</returns>
	[HttpDelete(RouteConstants.DeleteCountry)]
	[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
	public async Task<IActionResult> DeleteCountry([FromRoute] int key, [FromBody] CrmCountryDto modelDto)
	{
		// Execute business logic
		var res = await _serviceManager.CrmCountries.DeleteRecordAsync(key, modelDto);
		if (res == OperationMessage.Success)
		{
			var links = new List<ResourceLink>
					{
							ApiResponseHelper.SelfLink($"{Request.Path}", "Deleted country"),
							ApiResponseHelper.RelatedLink("countries", RouteConstants.CountryDDL, "GET", "Get all countries")
					};
			return Ok(ApiResponseHelper.Updated<CrmCountryDto>(data: modelDto, message: "Country deleted successfully", links: links));
		}
		else
		{
			return Conflict(ApiResponseHelper.Conflict<CrmCountryDto>(res));
		}
	}

	/// <summary>
	/// Creates or updates a country record
	/// </summary>
	/// <param name="key">Country ID (0 for create, >0 for update)</param>
	/// <param name="modelDto">Country data</param>
	/// <returns>Operation result message</returns>
	[HttpPost(RouteConstants.CreateOrUpdateCountry)]
	[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
	public async Task<IActionResult> SaveOrUpdate([FromRoute] int key, [FromBody] CrmCountryDto modelDto)
	{
		//Get authenticated user from HttpContext
		var currentUser = HttpContext.GetCurrentUser();
		var userId = HttpContext.GetUserId();

		// Validate input parameters
		if (modelDto == null)
			throw new NullModelBadRequestException("Country data cannot be null");

		// Execute business logic
		var res = await _serviceManager.CrmCountries.SaveOrUpdate(key, modelDto);

		// Return standardized response
		if (res == OperationMessage.Success)
			return Ok(ApiResponseHelper.Success(res, "Country saved successfully"));
		else
			return Conflict(ApiResponseHelper.Conflict<CrmCountryDto>(res));

		//if (res == OperationMessage.Success)
		//{
		//	var links = new List<ResourceLink>
		//			{
		//					ApiResponseHelper.SelfLink($"{Request.Path}", "Deleted country"),
		//					ApiResponseHelper.RelatedLink("countries", RouteConstants.CountryDDL, "GET", "Get all countries")
		//			};
		//	return Ok(ApiResponseHelper.Updated<CrmCountryDto>(data: modelDto, message: "Country deleted successfully", links: links));
		//}
		//else
		//{
		//	return Conflict(ApiResponseHelper.Conflict<CrmCountryDto>(res));
		//}
	}
}