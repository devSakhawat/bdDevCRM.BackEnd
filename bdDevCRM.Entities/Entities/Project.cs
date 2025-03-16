using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Project
{
    public int Projectid { get; set; }

    public int? Companyid { get; set; }

    public string? Projectcode { get; set; }

    public string Projectname { get; set; } = null!;

    public int? Owneremployeeid { get; set; }

    public string? Projectdescription { get; set; }

    public string? Clientcode { get; set; }

    public string? Clientname { get; set; }

    public int? Status { get; set; }

    public DateTime? Createddate { get; set; }

    public int? Userid { get; set; }

    public DateTime? Lastupdatedate { get; set; }

    public string? Repositoryname { get; set; }

    public int? Projectownerdepartmentid { get; set; }
}
