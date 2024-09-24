namespace MyApi2.Dtos
{
    public class TranslationTeamBatchsDto
    {
        public long Id { get; set; }

        public long TT_id { get; set; }

        public string? P_id { get; set; }

        public string? P_Name { get; set; }

        public string? P_CName { get; set; }

        public byte T_batch { get; set; }

        public string T_id { get; set; } = null!;

        public string? T_Name { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
