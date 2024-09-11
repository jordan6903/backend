namespace MyApi2.Dtos
{
    public class AccountInfosDto
    {
        public string Account_id { get; set; } = null!;

        public string? Name { get; set; }

        public string? Remark { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
