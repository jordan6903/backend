using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Translation_team
{
    public long Id { get; set; }

    public string P_id { get; set; } = null!;

    public byte T_batch { get; set; }

    public byte Type_id { get; set; }

    public string? Remark { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Product P_idNavigation { get; set; } = null!;

    public virtual ICollection<Translation_team_batch> Translation_team_batch { get; set; } = new List<Translation_team_batch>();

    public virtual Translation_team_type Type { get; set; } = null!;
}
