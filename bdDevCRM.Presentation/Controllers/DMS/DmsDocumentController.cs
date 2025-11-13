using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.ServicesContract;
using bdDevCRM.Shared.DataTransferObjects.DMS;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bdDevCRM.Presentation.Controllers.DMS;

public class DmsDocumentController : BaseApiController
{

  // private readonly IServiceManager _serviceManager;
  private readonly IMemoryCache _cache; 

  // The constructor now only needs to pass IServiceManager to the base constructor
  public DmsDocumentController(IServiceManager serviceManager, IMemoryCache cache) : base(serviceManager) 
  {
    // _serviceManager = serviceManager; // Remove this if you're using _serviceManager from base
    _cache = cache; // Remove this if no longer needed directly in this controller
  }

  // ---------- 1. DDL ------------------------------------------------
  [HttpGet("ddl")]
  public async Task<IActionResult> DocumentDDL()
  {
    if (!TryGetLoggedInUser(out var currentUser)) return Unauthorized();

    var list = await _serviceManager.DmsDocuments.GetDocumentsDDLAsync(false);
    return Ok(list);
  }

  // ---------- 2. Grid (Summary) ------------------------------------
  [HttpPost("summary")]
  public async Task<IActionResult> SummaryGrid([FromBody] CRMGridOptions opt)
  {
    if (!TryGetLoggedInUser(out var _)) return Unauthorized();
    if (opt == null) return BadRequest("Grid options null.");

    var grid = await _serviceManager.DmsDocuments.SummaryGrid(opt);
    return grid != null ? Ok(grid) : NoContent();
  }

  // ---------- 3. Create --------------------------------------------
  [HttpPost("create")]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> Create([FromBody] DmsDocumentDto dto)
  {
    if (!TryGetLoggedInUser(out var user)) return Unauthorized();
    dto.UploadedByUserId = user.UserId.ToString();      // inject uploader

    var res = await _serviceManager.DmsDocuments.CreateNewRecordAsync(dto);
    return res == OperationMessage.Success ? Ok(res) : Conflict(res);
  }

  // ---------- 4. Update --------------------------------------------
  [HttpPut("update/{id:int}")]
  [ServiceFilter(typeof(EmptyObjectFilterAttribute))]
  public async Task<IActionResult> Update(int id, [FromBody] DmsDocumentDto dto)
  {
    if (!TryGetLoggedInUser(out var _)) return Unauthorized();
    var res = await _serviceManager.DmsDocuments.UpdateNewRecordAsync(id, dto ,false);
    return res == OperationMessage.Success ? Ok(res) : Conflict(res);
  }

  // ---------- 5. Delete --------------------------------------------
  [HttpDelete("delete/{id:int}")]
  public async Task<IActionResult> Delete(int id , DmsDocumentDto dmsDocumentDto)
  {
    if (!TryGetLoggedInUser(out var _)) return Unauthorized();
    var res = await _serviceManager.DmsDocuments.DeleteRecordAsync(id , dmsDocumentDto);
    return res == OperationMessage.Success ? Ok(res) : Conflict(res);
  }

}
