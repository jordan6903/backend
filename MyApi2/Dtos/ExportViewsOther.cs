namespace MyApi2.Dtos
{
    public class ExportViewsOther
    {
        public int Id { get; set; }

        public int Export_batch { get; set; }

        public string Name { get; set; } = null!;

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public bool? Count_chk { get; set; }

        public int? Count_export { get; set; }

        public int? Count_exportall { get; set; }

        public int? Count_all { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }

        public ICollection<ExportViews2Other>? Series_data { get; set; }
    }
}
