using MyApi2.Models;

namespace MyApi2.Dtos
{
    public class ProductWebsiteViewsDto
    {
        public string P_id { get; set; } = null!;

        public string? P_Name { get; set; }

        public ICollection<ProductWebsiteViews2Dto>? Url_data { get; set; }
    }
}
