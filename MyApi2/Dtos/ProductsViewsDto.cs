namespace MyApi2.Dtos
{
    public class ProductsViewsDto
    {
        public int Id { get; set; }

        public string P_id { get; set; } = null!;

        public string C_id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? C_Name { get; set; }

        public string? P_CName { get; set; }

        public string? Content { get; set; }

        public string? Content_style { get; set; }

        public string? Play_time { get; set; }

        public string? Remark { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }

        public string? Company_name { get; set; }

        public string? Sale_date { get; set; }

        public bool? eso_chk { get; set; }

        public ICollection<ProductsViews2Dto>? TT_type { get; set; }
    }
}
