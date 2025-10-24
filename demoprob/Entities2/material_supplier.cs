using System;
using System.Collections.Generic;

namespace demoprob.Entities2;

public partial class material_supplier
{
    public int id { get; set; }

    public int material_id { get; set; }

    public int supplier_id { get; set; }

    public virtual material material { get; set; } = null!;

    public virtual supplier supplier { get; set; } = null!;
}
