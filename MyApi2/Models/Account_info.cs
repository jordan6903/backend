﻿using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Account_info
{
    public string Account_id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Remark { get; set; }

    public DateTime? Upd_date { get; set; }

    public string? Upd_user { get; set; }

    public DateTime? Create_dt { get; set; }

    public virtual Account_per? Account_per { get; set; }
}
