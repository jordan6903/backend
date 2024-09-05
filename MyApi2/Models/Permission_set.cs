using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Permission_set
{
    public byte Permission_id { get; set; }

    public string Name { get; set; } = null!;

    public string? Remark { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual ICollection<Account_per> Account_per { get; set; } = new List<Account_per>();
}
