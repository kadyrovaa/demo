using System;
using System.Collections.Generic;

namespace demoprob.Entities2;

public partial class material
{
    public int id { get; set; }

    public string material_name { get; set; } = null!;

    public int material_type_id { get; set; }

    public decimal cost_per_unit { get; set; }

    public decimal current_stock { get; set; }

    public decimal min_stock_level { get; set; }

    public decimal package_quantity { get; set; }

    public string unit_of_measure { get; set; } = null!;

    public virtual ICollection<material_supplier> material_suppliers { get; set; } = new List<material_supplier>();

    public virtual material_type material_type { get; set; } = null!;

    public virtual ICollection<product_material> product_materials { get; set; } = new List<product_material>();
}
