using bdDevCRM.Shared.DataTransferObjects.Conmon;

namespace bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

public class ModuleDto : CommonDto
{
  public int ModuleId { get; set; }

  public string ModuleName { get; set; } = null!;
}
