using System;
using System.Collections.Generic;

namespace demoprob.Entities2;

public partial class supplier
{
    public int id { get; set; }

    public string supplier_name { get; set; } = null!;

    public string supplier_type { get; set; } = null!;

    public string inn { get; set; } = null!;

    public int rating { get; set; }

    public DateOnly start_date { get; set; }

    public virtual ICollection<material_supplier> material_suppliers { get; set; } = new List<material_supplier>();
}
