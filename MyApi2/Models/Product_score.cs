using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Product_score
{
    public long Id { get; set; }

    public string P_id { get; set; } = null!;

    public byte Type_id { get; set; }

    public byte Score { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Product P_idNavigation { get; set; } = null!;

    public virtual Product_score_type Type { get; set; } = null!;
}
