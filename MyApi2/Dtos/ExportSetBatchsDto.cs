namespace MyApi2.Dtos
{
    public class ExportSetBatchsDto
    {
        public int Export_batch { get; set; }

        public string? Content { get; set; }

        public bool? Use_yn { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
