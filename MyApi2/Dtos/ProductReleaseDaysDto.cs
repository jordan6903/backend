namespace MyApi2.Dtos
{
    public class ProductReleaseDaysDto
    {
        public long Id { get; set; }

        public string P_id { get; set; } = null!;

        public string? P_Name { get; set; } = null!;

        public string Sale_Date { get; set; } = null!;

        public string? Presale_Date { get; set; }

        public decimal? Price { get; set; }

        public byte? Voice_id { get; set; }

        public string? Voice_Name { get; set; }

        public string? Currency_id { get; set; }

        public string? Currency_Name { get; set; }

        public string? Content { get; set; }

        public string? Device_id { get; set; }

        public string? Device_Name { get; set; }

        public byte? Rating_id { get; set; }

        public string? Rating_Name { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
