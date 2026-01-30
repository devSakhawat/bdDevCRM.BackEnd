using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.AuthorizeAttribiutes;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using bdDevCRM.Utilities.CRMGrid.GRID;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.Core.SystemAdmin;

/// <summary>
/// Controller for managing CRM courses
/// All methods require authentication via [AuthorizeUser] attribute
/// </summary>
[AuthorizeUser] //Controller-level authentication
//[ApiController]
public class CRMCourseController : BaseApiController
{
    private readonly IMemoryCache _cache;
    private readonly IWebHostEnvironment _env;

    public CRMCourseController(IServiceManager serviceManager, IMemoryCache cache, IWebHostEnvironment env) : base(serviceManager)
    {
        _cache = cache;
        _env = env;
    }

    // --------- 1. DDL --------------------------------------------------
    /// <summary>
    /// Retrieves all courses for dropdown list
    /// </summary>
    /// <returns>List of courses for dropdown</returns>
    [HttpGet(RouteConstants.CourseDDL)]
    public async Task<IActionResult> CourseDDL()
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Execute business logic
        var res = await _serviceManager.CrmCourses.GetCoursesDDLAsync(trackChanges: false);

        // Return standardized response
        if (res == null || !res.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<CrmCourseDto>>("No courses found"));

        return Ok(ResponseHelper.Success(res, "Courses retrieved successfully"));
    }

    /// <summary>
    /// Retrieves courses by institute ID for dropdown list
    /// </summary>
    /// <param name="instituteId">Institute ID</param>
    /// <returns>List of courses for the specified institute</returns>
    [HttpGet(RouteConstants.CourseByInstituteIdDDL)]
    public async Task<IActionResult> CourseByInstituteIdDDL([FromRoute] int instituteId)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (instituteId <= 0)
            throw new GenericBadRequestException("Invalid institute ID. Institute ID must be greater than 0.");

        // Execute business logic
        var res = await _serviceManager.CrmCourses.GetCourseByInstituteIdDDLAsync(instituteId, trackChanges: false);

        // Return standardized response
        if (res == null || !res.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<CRMCourseDDLDto>>("No course found"));

        return Ok(ResponseHelper.Success(res, "Courses retrieved successfully"));
    }

    // --------- 2. Summary Grid ----------------------------------------
    /// <summary>
    /// Retrieves paginated summary grid of courses
    /// </summary>
    /// <param name="options">Grid options for pagination, sorting, and filtering</param>
    /// <returns>Paginated grid of courses</returns>
    [HttpPost(RouteConstants.CourseSummary)]
    public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions options)
    {
        //Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (options == null)
            throw new NullModelBadRequestException("CRMGridOptions cannot be null");

        // Execute business logic
        var summaryGrid = await _serviceManager.CrmCourses.SummaryGrid(options);

        // Return standardized response
        if (summaryGrid == null || !summaryGrid.Items.Any())
            return Ok(ResponseHelper.NoContent<GridEntity<CrmCourseDto>>("No data found"));

        return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
    }

    // --------- 3. Create ----------------------------------------------
    /// <summary>
    /// Creates a new course record
    /// </summary>
    /// <param name="modelDto">Course data to create</param>
    /// <returns>Created course record</returns>
    [HttpPost(RouteConstants.CreateCourse)]
    [RequestSizeLimit(1_000_000)]
    public async Task<IActionResult> CreateNewRecord([FromBody] CrmCourseDto modelDto)
    {
        try
        {
            //Get authenticated user from HttpContext
            var currentUser = HttpContext.GetCurrentUser();
            var userId = HttpContext.GetUserId();

            // Validate input parameters
            if (modelDto == null)
                return BadRequest(ResponseHelper.BadRequest("Course data is required"));

            // Execute business logic
            CrmCourseDto res = await _serviceManager.CrmCourses.CreateNewRecordAsync(modelDto, currentUser);

            // Validate result
            if (res.CourseId > 0)
                return Ok(ResponseHelper.Created(res, "Course created successfully"));
            else
                return StatusCode(500, ResponseHelper.InternalServerError("Failed to create course"));
        }
        catch (System.Text.Json.JsonException)
        {
            return BadRequest(ResponseHelper.BadRequest("Invalid JSON format in course data"));
        }
    }

    // --------- 4. Update ----------------------------------------------
    /// <summary>
    /// Updates an existing course record
    /// </summary>
    /// <param name="key">Course ID</param>
    /// <param name="modelDto">Updated course data</param>
    /// <returns>Operation result message</returns>
    [HttpPut(RouteConstants.UpdateCourse)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    public async Task<IActionResult> UpdateCourse([FromRoute] int key, [FromBody] CrmCourseDto modelDto)
    {
        try
        {
            //Get authenticated user from HttpContext
            var currentUser = HttpContext.GetCurrentUser();
            var userId = HttpContext.GetUserId();

            // Validate input parameters
            if (key <= 0)
                throw new IdParametersBadRequestException();

            if (modelDto == null)
                throw new NullModelBadRequestException("Course data cannot be null");

            // Execute business logic
            var res = await _serviceManager.CrmCourses.UpdateRecordAsync(key, modelDto, false);

            // Return standardized response
            if (res == OperationMessage.Success)
                return Ok(ResponseHelper.Success(res, "Course updated successfully"));
            else
                return Conflict(ResponseHelper.Conflict(res));
        }
        catch (Exception)
        {
            throw;
        }
    }

    // --------- 5. Delete ----------------------------------------------
    /// <summary>
    /// Deletes a course record
    /// </summary>
    /// <param name="key">Course ID to delete</param>
    /// <param name="modelDto">Course data for validation</param>
    /// <returns>Operation result message</returns>
    [HttpDelete(RouteConstants.DeleteCourse)]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    public async Task<IActionResult> DeleteCourse([FromRoute] int key, [FromBody] CrmCourseDto modelDto)
    {
        try
        {
            //Get authenticated user from HttpContext
            var currentUser = HttpContext.GetCurrentUser();
            var userId = HttpContext.GetUserId();

            // Validate input parameters
            if (key <= 0)
                return BadRequest(ResponseHelper.BadRequest("Invalid course ID. Course ID must be greater than 0."));

            if (modelDto == null)
                return BadRequest(ResponseHelper.BadRequest("Course data is required"));

            // Execute business logic
            var res = await _serviceManager.CrmCourses.DeleteRecordAsync(key, modelDto);

            // Return standardized response
            if (res == OperationMessage.Success)
                return Ok(ResponseHelper.Success(res, "Course deleted successfully"));
            else
                return Conflict(ResponseHelper.Conflict(res));
        }
        catch (Exception)
        {
            throw;
        }
    }
}

