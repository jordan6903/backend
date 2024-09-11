namespace MyApi2.Dtos
{
    public class AccountPersDto
    {
        public long Id { get; set; }

        public string Account_id { get; set; } = null!;

        public string? Account_Name { get; set; }

        public byte Permission_id { get; set; }

        public string? Permission_Name { get; set; }

        public string? Password { get; set; }

        public string? Password_encrypt { get; set; }

        public string? Remark { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
