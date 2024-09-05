﻿using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Export_set_Company
{
    public int Id { get; set; }

    public int Export_batch { get; set; }

    public string C_id { get; set; } = null!;

    public bool? Use_yn { get; set; }

    public short? Sort { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Company C_idNavigation { get; set; } = null!;

    public virtual Export_set_batch Export_batchNavigation { get; set; } = null!;
}
