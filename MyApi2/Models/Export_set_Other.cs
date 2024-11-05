using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Export_set_Other
{
    public int Id { get; set; }

    public int Export_batch { get; set; }

    public string Name { get; set; } = null!;

    public bool? Use_yn { get; set; }

    public short? Sort { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Export_set_batch Export_batchNavigation { get; set; } = null!;

    public virtual ICollection<Export_set_Other_series> Export_set_Other_series { get; set; } = new List<Export_set_Other_series>();
}
