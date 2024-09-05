using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Staff_type
{
    public byte Staff_typeid { get; set; }

    public string Name { get; set; } = null!;

    public string? Content { get; set; }

    public bool? Use_yn { get; set; }

    public short? Sort { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
