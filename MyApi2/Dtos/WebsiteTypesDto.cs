namespace MyApi2.Dtos
{
    public class WebsiteTypesDto
    {
        public string Type_id { get; set; } = null!;

        public string? Name { get; set; }

        public string? Remark { get; set; }

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
