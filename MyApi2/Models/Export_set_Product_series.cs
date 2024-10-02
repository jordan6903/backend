using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Export_set_Product_series
{
    public long Id { get; set; }

    public int ESC_id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Use_yn { get; set; }

    public short? Sort { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Export_set_Company ESC { get; set; } = null!;

    public virtual ICollection<Export_set_Product> Export_set_Product { get; set; } = new List<Export_set_Product>();
}
