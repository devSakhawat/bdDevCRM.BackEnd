using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class IvmsVisitor
{
    public int VisitorId { get; set; }

    public string VisitorName { get; set; } = null!;

    public string? VisitorCompany { get; set; }

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public string? Address { get; set; }

    public string MobileNo { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public int TotalPerson { get; set; }

    public string Purpose { get; set; } = null!;

    public string? Photo { get; set; }

    public bool IsForeigner { get; set; }

    public string? PassportNo { get; set; }

    public string? VisaType { get; set; }

    public string? VisaNo { get; set; }

    public DateOnly VisaIssuedDate { get; set; }

    public DateOnly VisaExpiredDate { get; set; }

    public string? Country { get; set; }

    /// <summary>
    /// 0=Waiting, 1=Approved, 2= Deny
    /// </summary>
    public int Status { get; set; }

    public DateTime? CreateDate { get; set; }
}
