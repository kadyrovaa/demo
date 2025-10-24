using System;
using System.Collections.Generic;

namespace demoprob.Entities2;

public partial class material_type
{
    public int id { get; set; }

    public string type_name { get; set; } = null!;

    public decimal defect_percentage { get; set; }

    public virtual ICollection<material> materials { get; set; } = new List<material>();
}
