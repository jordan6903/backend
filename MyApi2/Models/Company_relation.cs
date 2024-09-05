using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Company_relation
{
    public long Id { get; set; }

    public string C_id { get; set; } = null!;

    public string C_id_to { get; set; } = null!;

    public byte Relation_id { get; set; }

    public string? Content { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Company C_idNavigation { get; set; } = null!;

    public virtual Company C_id_toNavigation { get; set; } = null!;

    public virtual Company_relation_info Relation { get; set; } = null!;
}
