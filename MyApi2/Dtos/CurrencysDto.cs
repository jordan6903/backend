﻿using MyApi2.Models;

namespace MyApi2.Dtos
{
    public class CurrencysDto
    {
        public string Currency_id { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string ShortName { get; set; } = null!;

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
