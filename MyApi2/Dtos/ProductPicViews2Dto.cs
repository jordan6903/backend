namespace MyApi2.Dtos
{
    public class ProductPicViews2Dto
    {
        public long Id { get; set; }

        public string Type_id { get; set; } = null!;

        public string? Type_Name { get; set; }

        public string? Name { get; set; }

        public string Url { get; set; } = null!;

        public short width { get; set; }

        public short height { get; set; }

        public string? Remark { get; set; }

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public DateTime? Upd_date { get; set; }
    }
}
