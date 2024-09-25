namespace MyApi2.Dtos
{
    public class ProductsView1Dto
    {
        public string C_id { get; set; } = null!;

        public string? C_Name { get; set; }

        public string P_id { get; set; } = null!;

        public string? P_Name { get; set; }

        public string? P_CName { get; set; }

        public string? P_Content { get; set; }

        public string? Content_style { get; set; }

        public string? Play_time { get; set; }

        public string? P_Remark { get; set; }

        public ICollection<ProductReleaseDaysDto>? Release_data { get; set; } //發售日

        public ICollection<ProductWebsitesDto>? Website_data { get; set; } //網址

        public ICollection<ProductPicsDto>? Pic_data { get; set; } //圖片

        public ICollection<ProductTypesDto>? Type_data { get; set; } //屬性/標籤

        public ICollection<ProductRelationsDto>? Relation_data { get; set; } //關聯遊戲

        public ICollection<ProductScoresDto>? Scoren_data { get; set; } //評分

        public ICollection<StaffsDto>? Staff_data { get; set; } //製作人員
    }
}
