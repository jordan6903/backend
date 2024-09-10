﻿namespace MyApi2.Dtos
{
    public class CompanyTypesDto
    {
        public byte C_type { get; set; }

        public string C_type_name { get; set; } = null!;

        public string? Remark { get; set; }

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
