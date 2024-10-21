namespace MyApi2.Dtos
{
    public class ExportSetProductsDto
    {
        public long? esc_id { get; set; }

        public long esps_id { get; set; }

        public long? esp_id { get; set; }

        public string? P_id { get; set; }

        public string? P_Name { get; set; }

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
