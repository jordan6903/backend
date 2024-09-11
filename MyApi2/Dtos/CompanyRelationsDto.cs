namespace MyApi2.Dtos
{
    public class CompanyRelationsDto
    {
        public long Id { get; set; }

        public string C_id { get; set; } = null!;

        public string? C_Name { get; set; }

        public string C_id_to { get; set; } = null!;

        public string? C_Name_to { get; set; }

        public byte Relation_id { get; set; }

        public string? Relation_Name { get; set; }

        public string? Content { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
