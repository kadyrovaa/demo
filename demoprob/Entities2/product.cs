using System;
using System.Collections.Generic;

namespace demoprob.Entities2;

public partial class product
{
    public string article_number { get; set; } = null!;

    public int product_type_id { get; set; }

    public string product_name { get; set; } = null!;

    public string? description { get; set; }

    public decimal min_partner_price { get; set; }

    public decimal? roll_width { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<product_material> product_materials { get; set; } = new List<product_material>();

    public virtual product_type product_type { get; set; } = null!;
}
