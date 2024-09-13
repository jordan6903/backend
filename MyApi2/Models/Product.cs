using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Product
{
    public int Id { get; set; }

    public string P_id { get; set; } = null!;

    public string C_id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? C_Name { get; set; }

    public string? Content { get; set; }

    public string? Content_style { get; set; }

    public string? Play_time { get; set; }

    public string? Remark { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Company C_idNavigation { get; set; } = null!;

    public virtual ICollection<Export_set_Product> Export_set_Product { get; set; } = new List<Export_set_Product>();

    public virtual ICollection<Export_set_Product_series> Export_set_Product_series { get; set; } = new List<Export_set_Product_series>();

    public virtual ICollection<Product_Pic> Product_Pic { get; set; } = new List<Product_Pic>();

    public virtual ICollection<Product_Release_day> Product_Release_day { get; set; } = new List<Product_Release_day>();

    public virtual ICollection<Product_Website> Product_Website { get; set; } = new List<Product_Website>();

    public virtual ICollection<Product_relation> Product_relationP_idNavigation { get; set; } = new List<Product_relation>();

    public virtual ICollection<Product_relation> Product_relationP_id_toNavigation { get; set; } = new List<Product_relation>();

    public virtual ICollection<Product_score> Product_score { get; set; } = new List<Product_score>();

    public virtual ICollection<Product_type> Product_type { get; set; } = new List<Product_type>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual ICollection<Translation_team> Translation_team { get; set; } = new List<Translation_team>();
}
