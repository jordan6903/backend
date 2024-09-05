using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Export_set_batch
{
    public int Export_batch { get; set; }

    public string? Content { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual ICollection<Export_set_Company> Export_set_Company { get; set; } = new List<Export_set_Company>();

    public virtual ICollection<Export_set_Product> Export_set_Product { get; set; } = new List<Export_set_Product>();

    public virtual ICollection<Export_set_Product_series> Export_set_Product_series { get; set; } = new List<Export_set_Product_series>();
}
