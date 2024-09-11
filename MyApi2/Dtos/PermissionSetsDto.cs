namespace MyApi2.Dtos
{
    public class PermissionSetsDto
    {
        public byte Permission_id { get; set; }

        public string Name { get; set; } = null!;

        public string? Remark { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
