namespace MyApi2.Dtos
{
    public class ExportSetOtherSeriesDto
    {
        public int eso_id { get; set; }

        public long esos_Id { get; set; }

        public string? Name { get; set; }

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public string? Product_data { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
