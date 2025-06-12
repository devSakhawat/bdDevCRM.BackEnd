using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GeoFencingUserInfo
{
    public int GeoFencingUserInfoId { get; set; }

    public string? LoginId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Raw { get; set; }

    public string? Status { get; set; }
}
