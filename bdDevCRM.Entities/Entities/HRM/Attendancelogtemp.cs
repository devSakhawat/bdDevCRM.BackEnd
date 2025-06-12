using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Attendancelogtemp
{
    public int Userid { get; set; }

    public DateTime Attendancedate { get; set; }

    public string? Logintime { get; set; }

    public string? Logouttime { get; set; }

    public int Status { get; set; }

    public int? Isattendanceclearout { get; set; }

    public int? Islate { get; set; }

    public string? Latereason { get; set; }

    public int? Shiftid { get; set; }

    public int? Isholiday { get; set; }
}
