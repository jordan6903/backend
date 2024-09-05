using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Rating
{
    public byte Rating_id { get; set; }

    public byte Rating_type { get; set; }

    public string Name { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public string? Name_JP { get; set; }

    public string? Name_EN { get; set; }

    public string? Content { get; set; }

    public bool? Use_yn { get; set; }

    public short? Sort { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual ICollection<Product_Release_day> Product_Release_day { get; set; } = new List<Product_Release_day>();

    public virtual Rating_type Rating_typeNavigation { get; set; } = null!;
}
