using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Staff
{
    public long Id { get; set; }

    public string P_id { get; set; } = null!;

    public string Staff_id { get; set; } = null!;

    public byte Staff_typeid { get; set; }

    public string? Remark { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Product P_idNavigation { get; set; } = null!;

    public virtual Staff_info StaffNavigation { get; set; } = null!;

    public virtual Staff_type Staff_type { get; set; } = null!;
}
