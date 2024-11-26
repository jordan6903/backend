namespace MyApi2.Dtos
{
    public class ProductWebsiteViews2Dto
    {
        public long Id { get; set; }

        public string Type_id { get; set; } = null!;

        public string? Type_Name { get; set; }

        public string? Name { get; set; }

        public string Url { get; set; } = null!;

        public string? Remark { get; set; }
    }
}
