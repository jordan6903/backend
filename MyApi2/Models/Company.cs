using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Company
{
    public int Id { get; set; }

    public string C_id { get; set; } = null!;

    public byte C_type { get; set; }

    public string Name { get; set; } = null!;

    public string? Name_origin { get; set; }

    public string? Name_short { get; set; }

    public string? Intro { get; set; }

    public string? Remark { get; set; }

    public string? Sdate { get; set; }

    public string? Edate { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Company_type C_typeNavigation { get; set; } = null!;

    public virtual ICollection<Company_Pic> Company_Pic { get; set; } = new List<Company_Pic>();

    public virtual ICollection<Company_Website> Company_Website { get; set; } = new List<Company_Website>();

    public virtual ICollection<Company_relation> Company_relationC_idNavigation { get; set; } = new List<Company_relation>();

    public virtual ICollection<Company_relation> Company_relationC_id_toNavigation { get; set; } = new List<Company_relation>();

    public virtual ICollection<Export_set_Company> Export_set_Company { get; set; } = new List<Export_set_Company>();

    public virtual ICollection<Export_set_Product> Export_set_Product { get; set; } = new List<Export_set_Product>();

    public virtual ICollection<Export_set_Product_series> Export_set_Product_series { get; set; } = new List<Export_set_Product_series>();

    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}
