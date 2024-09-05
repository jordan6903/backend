using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Website_Type
{
    public string Type_id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Remark { get; set; }

    public bool? Use_yn { get; set; }

    public short? Sort { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual ICollection<Company_Pic> Company_Pic { get; set; } = new List<Company_Pic>();

    public virtual ICollection<Company_Website> Company_Website { get; set; } = new List<Company_Website>();

    public virtual ICollection<Product_Pic> Product_Pic { get; set; } = new List<Product_Pic>();

    public virtual ICollection<Product_Website> Product_Website { get; set; } = new List<Product_Website>();
}
