using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoryDtos.CRM;


public class CountryRepositoryDto
{
  public int CountryId { get; set; }

  public string? CountryName { get; set; }

  public string? CountryCode { get; set; }

  public int? Status { get; set; }
}
