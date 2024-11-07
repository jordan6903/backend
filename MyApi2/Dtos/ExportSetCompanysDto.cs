namespace MyApi2.Dtos
{
    public class ExportSetCompanysDto
    {
        public int Id { get; set; }

        public int Export_batch { get; set; }

        public string? C_id { get; set; } = null!;

        public string? C_Name { get; set; } 

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public string? Title { get; set; }

        public bool? DragShow { get; set; }

        public string? Series_data { get; set; }

        public int? snumber { get; set; }

        public int? pnumber { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
