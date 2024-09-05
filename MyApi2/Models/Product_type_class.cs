using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Product_type_class
{
    public byte P_type_class { get; set; }

    public string Name { get; set; } = null!;

    public string? Content { get; set; }

    public bool? Use_yn { get; set; }

    public short? Sort { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual ICollection<Product_type_info> Product_type_info { get; set; } = new List<Product_type_info>();
}
