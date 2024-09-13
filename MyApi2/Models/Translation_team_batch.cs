using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Translation_team_batch
{
    public long Id { get; set; }

    public long TT_id { get; set; }

    public string P_id { get; set; } = null!;

    public byte T_batch { get; set; }

    public string T_id { get; set; } = null!;

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Translation_team TT { get; set; } = null!;

    public virtual Translation_team_info T_idNavigation { get; set; } = null!;
}
