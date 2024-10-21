﻿namespace MyApi2.Dtos
{
    public class ExportSetProductSeriesDto
    {
        public int esc_id { get; set; }

        public long esps_Id { get; set; }

        public string Name { get; set; } = null!;

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public string? Product_data { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
