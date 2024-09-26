using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Product_Release_day
{
    public long Id { get; set; }

    public string P_id { get; set; } = null!;

    public string Sale_Date { get; set; } = null!;

    public string? Presale_Date { get; set; }

    public decimal? Price { get; set; }

    public byte? Voice_id { get; set; }

    public string? Currency_id { get; set; }

    public string? Content { get; set; }

    public string? Device_id { get; set; }

    public byte? Rating_id { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public string? Name { get; set; }

    public virtual Currency? Currency { get; set; }

    public virtual Device? Device { get; set; }

    public virtual Product P_idNavigation { get; set; } = null!;

    public virtual Rating? Rating { get; set; }

    public virtual Voice_type? Voice { get; set; }
}
