using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Export_set_Product
{
    public long Id { get; set; }

    public long ESPS_id { get; set; }

    public string P_id { get; set; } = null!;

    public bool? Use_yn { get; set; }

    public short? Sort { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Export_set_Product_series ESPS { get; set; } = null!;

    public virtual Product P_idNavigation { get; set; } = null!;
}
