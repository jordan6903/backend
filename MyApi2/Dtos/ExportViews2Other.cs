namespace MyApi2.Dtos
{
    public class ExportViews2Other
    {
        public long Id { get; set; }

        public int eso_id { get; set; }

        public string Name { get; set; } = null!;

        public string P_data { get; set; }

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }

        public string? Add_word { get; set; }

        public bool? Add_word_Use_yn { get; set; }
    }
}
