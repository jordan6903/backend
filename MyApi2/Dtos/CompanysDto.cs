namespace MyApi2.Dtos
{
    public class CompanysDto
    {
        public int Id { get; set; }

        public string C_id { get; set; } = null!;

        public byte C_type { get; set; }

        public string Name { get; set; } = null!;

        public string? Name_origin { get; set; }

        public string? Name_short { get; set; }

        public string? Intro { get; set; }

        public string? Remark { get; set; }

        public string? Sdate { get; set; }

        public string? Edate { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }

        public string? C_type_name { get; set; } = null!;
    }
}
