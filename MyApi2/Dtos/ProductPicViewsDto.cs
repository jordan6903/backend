namespace MyApi2.Dtos
{
    public class ProductPicViewsDto
    {
        public string P_id { get; set; } = null!;

        public string? P_Name { get; set; }

        public ICollection<ProductPicViews2Dto>? Pic_data { get; set; }
    }
}
