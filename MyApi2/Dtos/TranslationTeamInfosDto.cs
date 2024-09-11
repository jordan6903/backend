namespace MyApi2.Dtos
{
    public class TranslationTeamInfosDto
    {
        public long Id { get; set; }

        public string T_id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Content { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
