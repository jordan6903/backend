﻿namespace MyApi2.Dtos
{
    public class ExportViews2
    {
        public long Id { get; set; }

        public int ESC_id { get; set; }

        public string Name { get; set; } = null!;

        public string P_data { get; set; }

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
