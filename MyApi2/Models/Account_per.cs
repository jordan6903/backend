using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Account_per
{
    public long Id { get; set; }

    public string Account_id { get; set; } = null!;

    public byte Permission_id { get; set; }

    public string? Password { get; set; }

    public string? Password_encrypt { get; set; }

    public string? Remark { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Account_info Account { get; set; } = null!;

    public virtual Permission_set Permission { get; set; } = null!;
}
