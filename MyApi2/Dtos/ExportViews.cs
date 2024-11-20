namespace MyApi2.Dtos
{
    public class ExportViews
    {
        public int Id { get; set; }

        public int Export_batch { get; set; }

        public string C_id { get; set; } = null!;

        public string C_Name { get; set; } = null!;

        public string? C_Name_origin { get; set; }

        public string? C_Name_short { get; set; }

        public string? C_Intro { get; set; }

        public string? C_Remark { get; set; }

        public string? Url { get; set; }

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public bool? Repeat_chk { get; set; }

        public bool? Count_chk { get; set; }

        public int? Count_export { get; set; }

        public int? Count_exportall { get; set; }

        public int? Count_all { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }

        public ICollection<ExportViews2>? Series_data { get; set; }
    }
}
