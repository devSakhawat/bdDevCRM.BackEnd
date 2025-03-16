using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SliderPicture
{
    public int SliderPictureId { get; set; }

    public string? SliderPicturePath { get; set; }

    public string? PictureTite { get; set; }

    public string? Description { get; set; }

    public int? IsActive { get; set; }
}
