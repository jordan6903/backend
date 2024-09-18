namespace MyApi2.Dtos
{
    public class ProductScoresDto
    {
        public long Id { get; set; }

        public string P_id { get; set; } = null!;

        public string? P_Name { get; set; }

        public byte Type_id { get; set; }

        public string? Type_Name { get; set; }

        public byte Score { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
