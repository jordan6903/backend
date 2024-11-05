namespace MyApi2.Dtos
{
    public class ExportViewsPOther
    {
        public long Export_batch { get; set; }

        public long Id { get; set; }

        public long esos_id { get; set; }

        public string? C_id { get; set; }

        public string? C_Name { get; set; }

        public string P_id { get; set; } = null!;

        public string P_Name { get; set; }

        public string P_CName { get; set; }

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
