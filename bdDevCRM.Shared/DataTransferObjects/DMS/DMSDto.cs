using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.DataTransferObjects.DMS;


public class DMSDto
{
  // DmsdocumentTypeDto -----------------------
  //public int DocumentTypeId { get; set; }

  public string DocumentTypeName { get; set; } = null!;
  public string DocumentType { get; set; } = null!;
  public bool IsMandatory { get; set; }
  public string? AcceptedExtensions { get; set; }
  public int? MaxFileSizeMb { get; set; }

  // ---------- DmsDocumentDto--------------------
  public int DocumentId { get; set; }
  public string Title { get; set; } = null!;
  public string? Description { get; set; }
  public string FileName { get; set; } = null!;
  public string FileExtension { get; set; } = null!;
  public long FileSize { get; set; }
  public string FilePath { get; set; } = null!;
  public DateTime UploadDate { get; set; } = DateTime.UtcNow;
  public string? UploadedByUserId { get; set; }
  public int DocumentTypeId { get; set; }
  public string ReferenceEntityType { get; set; } = null!;
  public string ReferenceEntityId { get; set; } = null!;

  // DmsdocumentAccessLogDto -----------------------
  //public long LogId { get; set; }

  //public int DocumentId { get; set; }

  public string AccessedByUserId { get; set; }

  public DateTime AccessDateTime { get; set; }

  public string Action { get; set; } = null!;
  public string? IpAddress { get; set; }

  public string? DeviceInfo { get; set; } //userAgent

  public string? MacAddress { get; set; }

  public string? Notes { get; set; }

  // DmsdocumentFolderDto -----------------------
  //public int FolderId { get; set; }

  public int? ParentFolderId { get; set; }

  public string FolderName { get; set; } = null!;

  public string OwnerId { get; set; }

  //public string ReferenceEntityType { get; set; } = null!;

  //public string ReferenceEntityId { get; set; } = null!;


  // DmsdocumentTagDto-----------------------

  public int TagId { get; set; }

  public string DocumentTagName { get; set; } = null!;

  // DmsdocumentVersionDto -----------------------
  public int VersionId { get; set; }
  //public int DocumentId { get; set; }
  public int VersionNumber { get; set; }
  //public string FileName { get; set; } = null!;
  //public string FilePath { get; set; } = null!;
  public DateTime? UploadedDate { get; set; }
  public string? UploadedBy { get; set; }
}