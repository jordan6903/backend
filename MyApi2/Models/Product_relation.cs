using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Product_relation
{
    public long Id { get; set; }

    public string P_id { get; set; } = null!;

    public string P_id_to { get; set; } = null!;

    public byte Relation_id { get; set; }

    public string? Content { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Product P_idNavigation { get; set; } = null!;

    public virtual Product P_id_toNavigation { get; set; } = null!;

    public virtual Product_relation_info Relation { get; set; } = null!;
}
