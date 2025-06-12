using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmpGuranteersInfo
{
    public int GuranteerId { get; set; }

    public int? HrRecordId { get; set; }

    public string? GuranteerName { get; set; }

    public int? GhrRecordId { get; set; }

    public int? GuranteerNumber { get; set; }

    public string? GphoneNumber { get; set; }

    public string? GpresentAddress { get; set; }

    public string? GpermanentAddress { get; set; }

    public string? Gprofession { get; set; }

    public string? GstampNo { get; set; }

    public string? GofficeAdd { get; set; }

    public string? GnationalId { get; set; }

    public string? GprofilePicture { get; set; }

    public int? GrelationId { get; set; }

    public int? GprofessionTypeId { get; set; }

    public DateOnly? GdateOfBirth { get; set; }

    public int? UserId { get; set; }

    public DateTime? AddedDate { get; set; }
}
