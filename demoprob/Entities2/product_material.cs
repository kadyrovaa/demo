using System;
using System.Collections.Generic;

namespace demoprob.Entities2;

public partial class product_material
{
    public int id { get; set; }

    public string product_article { get; set; } = null!;

    public int material_id { get; set; }

    public decimal material_quantity { get; set; }

    public virtual material material { get; set; } = null!;

    public virtual product product_articleNavigation { get; set; } = null!;
}
