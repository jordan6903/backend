namespace MyApi2.Dtos
{
    public class ExportSetProductSeriesDto
    {
        public int Id { get; set; }

        public int Export_batch { get; set; }

        public string Name { get; set; } = null!;

        public string C_id { get; set; } = null!;

        public string P_id { get; set; } = null!;

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
