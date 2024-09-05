using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Product_type
{
    public long Id { get; set; }

    public string P_id { get; set; } = null!;

    public string P_type_id { get; set; } = null!;

    public string? Remark { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Product P_idNavigation { get; set; } = null!;

    public virtual Product_type_info P_type { get; set; } = null!;
}
