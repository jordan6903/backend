namespace MyApi2.Dtos
{
    public class ProductRelationsDto
    {
        public long Id { get; set; }

        public string P_id { get; set; } = null!;

        public string? P_Name { get; set; }

        public string P_id_to { get; set; } = null!;

        public string? P_Name_to { get; set; }

        public byte Relation_id { get; set; }

        public string? Relation_Name { get; set; }

        public string? Content { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
