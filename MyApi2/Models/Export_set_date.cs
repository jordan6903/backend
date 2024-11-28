using System;
using System.Collections.Generic;

namespace MyApi2.Models;

public partial class Export_set_date
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public DateTime Date_mark { get; set; }

    public bool? Use_yn { get; set; }
}
