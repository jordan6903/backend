namespace MyApi2.Dtos
{
    public class TranslationTeamsDto
    {
        public long Id { get; set; }

        public string P_id { get; set; } = null!;

        public string? P_Name { get; set; }

        public string? P_CName { get; set; }

        public byte T_batch { get; set; }

        public string? T_id { get; set; }

        public string? T_Name { get; set; }

        public byte Type_id { get; set; }

        public string? Type_Name { get; set; }

        public string? Remark { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }

        public ICollection<TTviewsDto1>? T_batch_data { get; set; }
    }
}
