using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Currency
{
    public string Currency_id { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public bool? Use_yn { get; set; }

    public short? Sort { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual ICollection<Product_Release_day> Product_Release_day { get; set; } = new List<Product_Release_day>();
}
