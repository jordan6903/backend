using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Product_Pic
{
    public long Id { get; set; }

    public string P_id { get; set; } = null!;

    public string Type_id { get; set; } = null!;

    public string? Name { get; set; }

    public string Url { get; set; } = null!;

    public short width { get; set; }

    public short height { get; set; }

    public string? Remark { get; set; }

    public bool? Use_yn { get; set; }

    public short? Sort { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Product P_idNavigation { get; set; } = null!;

    public virtual Website_Type Type { get; set; } = null!;
}
