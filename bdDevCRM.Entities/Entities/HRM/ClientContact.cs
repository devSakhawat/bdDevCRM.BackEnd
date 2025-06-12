using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ClientContact
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public string PersonName { get; set; } = null!;

    public string? Designation { get; set; }

    public int? IsPrimaryContact { get; set; }

    public string? CellPhone { get; set; }

    public string? Email { get; set; }

    public string? HomeAddress { get; set; }

    public string? OfficeAddress { get; set; }

    public int? UserId { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public DateTime? EntryDate { get; set; }

    public int? IsActive { get; set; }
}
