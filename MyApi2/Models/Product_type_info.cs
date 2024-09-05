using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Product_type_info
{
    public string P_type_id { get; set; } = null!;

    public byte P_type_class { get; set; }

    public string FullName { get; set; } = null!;

    public string? ShortName { get; set; }

    public string? FullName_JP { get; set; }

    public string? FullName_EN { get; set; }

    public string? Content { get; set; }

    public bool? Use_yn { get; set; }

    public short? Sort { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Product_type_class P_type_classNavigation { get; set; } = null!;

    public virtual ICollection<Product_type> Product_type { get; set; } = new List<Product_type>();
}
