using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Translation_team_info
{
    public long Id { get; set; }

    public string T_id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual ICollection<Translation_team_batch> Translation_team_batch { get; set; } = new List<Translation_team_batch>();
}
