namespace MyApi2.Dtos
{
    public class StaffsDto
    {
        public long Id { get; set; }

        public string P_id { get; set; } = null!;

        public string? P_Name { get; set; }

        public string Staff_id { get; set; } = null!;

        public string? Staff_Name { get; set; }

        public byte Staff_typeid { get; set; }

        public string? Staff_type_Name { get; set; }

        public string? Remark { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
