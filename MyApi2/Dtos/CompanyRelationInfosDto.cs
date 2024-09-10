namespace MyApi2.Dtos
{
    public class CompanyRelationInfosDto
    {
        public byte Relation_id { get; set; }

        public string Name { get; set; } = null!;

        public string? Content { get; set; }

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
