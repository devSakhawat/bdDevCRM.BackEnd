using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Presentation.Controllers.BaseController;
using bdDevCRM.Presentation.Extensions;
using bdDevCRM.RepositoryDtos.DMS;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.ApiResponse;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.DMS;

/// <summary>
/// Controller for managing DMS (Document Management System) documents
/// All methods require authentication via [AuthenticatedUser] attribute
/// </summary>
[AuthenticatedUser] // ✅ Controller-level authentication
public class DmsDocumentController : BaseApiController
{
    private readonly IMemoryCache _cache;

    public DmsDocumentController(IServiceManager serviceManager, IMemoryCache cache) 
        : base(serviceManager)
    {
        _cache = cache;
    }

    /// <summary>
    /// Retrieves all documents for dropdown list
    /// </summary>
    /// <returns>List of documents for dropdown</returns>
    [HttpGet("ddl")]
    public async Task<IActionResult> DocumentDDL()
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Execute business logic
        var list = await _serviceManager.DmsDocuments
            .GetDocumentsDDLAsync(false);

        // Return standardized response
        if (list == null || !list.Any())
            return Ok(ResponseHelper.NoContent<IEnumerable<object>>(
                "No documents found"));

        return Ok(ResponseHelper.Success(list, 
            "Documents retrieved successfully"));
    }

    /// <summary>
    /// Retrieves paginated summary grid of documents
    /// </summary>
    /// <param name="opt">Grid options for pagination, sorting, and filtering</param>
    /// <returns>Paginated grid of documents</returns>
    [HttpPost("summary")]
    public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions opt)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (opt == null)
            return BadRequest(ResponseHelper.BadRequest("Grid options cannot be null"));

        // Execute business logic
        var grid = await _serviceManager.DmsDocuments.SummaryGrid(opt);

        // Return standardized response
        if (grid == null || !grid.Items.Any())
            return Ok(ResponseHelper.NoContent<GridEntity<DmsDocumentDto>>(
                "No documents found"));

        return Ok(ResponseHelper.Success(grid, 
            "Documents grid retrieved successfully"));
    }

    /// <summary>
    /// Creates a new document record
    /// </summary>
    /// <param name="dto">Document data to create</param>
    /// <returns>Operation result message</returns>
    [HttpPost("create")]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    public async Task<IActionResult> Create([FromBody] DmsDocumentDto dto)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (dto == null)
            throw new NullModelBadRequestException("Document data cannot be null");

        // Inject uploader information
        dto.UploadedByUserId = currentUser.UserId.ToString();

        // Execute business logic
        var res = await _serviceManager.DmsDocuments.CreateNewRecordAsync(dto);

        // Return standardized response
        if (res == OperationMessage.Success)
            return Ok(ResponseHelper.Success(res, 
                "Document created successfully"));
        else
            return Conflict(ResponseHelper.Conflict(res));
    }

    /// <summary>
    /// Updates an existing document record
    /// </summary>
    /// <param name="id">Document ID</param>
    /// <param name="dto">Updated document data</param>
    /// <returns>Operation result message</returns>
    [HttpPut("update/{id:int}")]
    [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
    public async Task<IActionResult> Update(int id, [FromBody] DmsDocumentDto dto)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (id <= 0)
            throw new GenericBadRequestException(
                "Invalid document ID. ID must be greater than 0.");

        if (dto == null)
            throw new NullModelBadRequestException("Document data cannot be null");

        // Execute business logic
        var res = await _serviceManager.DmsDocuments
            .UpdateNewRecordAsync(id, dto, false);

        // Return standardized response
        if (res == OperationMessage.Success)
            return Ok(ResponseHelper.Updated(res, 
                "Document updated successfully"));
        else
            return Conflict(ResponseHelper.Conflict(res));
    }

    /// <summary>
    /// Deletes a document record
    /// </summary>
    /// <param name="id">Document ID to delete</param>
    /// <param name="dmsDocumentDto">Document data for validation</param>
    /// <returns>Operation result message</returns>
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id, DmsDocumentDto dmsDocumentDto)
    {
        // ✅ Get authenticated user from HttpContext
        var currentUser = HttpContext.GetCurrentUser();
        var userId = HttpContext.GetUserId();

        // Validate input parameters
        if (id <= 0)
            throw new GenericBadRequestException(
                "Invalid document ID. ID must be greater than 0.");

        if (dmsDocumentDto == null)
            throw new NullModelBadRequestException("Document data cannot be null");

        // Execute business logic
        var res = await _serviceManager.DmsDocuments.DeleteRecordAsync(id, dmsDocumentDto);

        // Return standardized response
        if (res == OperationMessage.Success)
            return Ok(ResponseHelper.Success(res, 
                "Document deleted successfully"));
        else
            return Conflict(ResponseHelper.Conflict(res));
    }
}
