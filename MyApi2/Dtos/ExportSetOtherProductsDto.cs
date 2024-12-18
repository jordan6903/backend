﻿namespace MyApi2.Dtos
{
    public class ExportSetOtherProductsDto
    {
        public long? export_batch { get; set; }

        public long? eso_id { get; set; }

        public long esos_id { get; set; }

        public long? esop_id { get; set; }

        public string? P_id { get; set; }

        public string? P_Name { get; set; }

        public string? C_id { get; set; }

        public string? C_Name { get; set; }

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public bool? esp_chk { get; set; }

        public string? Sale_Date { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }

        public ICollection<ProductsViews2Dto>? TT_type { get; set; }
    }
}
