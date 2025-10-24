using System;
using System.Collections.Generic;

namespace demoprob.Entities2;

public partial class product_type
{
    public int id { get; set; }

    public string type_name { get; set; } = null!;

    public decimal production_coefficient { get; set; }

    public virtual ICollection<product> products { get; set; } = new List<product>();
}
