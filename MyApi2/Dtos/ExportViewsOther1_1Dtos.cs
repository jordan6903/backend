namespace MyApi2.Dtos
{
    public class ExportViewsOther1_1Dtos
    {
        public int Id { get; set; }

        public int Export_batch { get; set; }

        public string C_id { get; set; } = null!;

        public string? C_Name { get; set; }

        public string? C_Name_origin { get; set; }

        public short? eso_Sort { get; set; }

        public int esos_id { get; set; }

        public string esos_Name { get; set; } = null!;

        public short? esos_Sort { get; set; }

        public int esop_id { get; set; }

        public short? esop_Sort { get; set; }

        public string? P_id { get; set; }

        public string? P_Name { get; set; }

        public string? P_CName { get; set; }

        public string? Sale_Date { get; set; }

        public string T_id { get; set; } = string.Empty;

        public string? T_team { get; set; }

        public int? T_type { get; set; }

        public string? Remark { get; set; }

        public string? url1 { get; set; }

        public string? url2 { get; set; }

        public string? url3 { get; set; }

        public string? url4 { get; set; }

        public string? pic { get; set; }
    }
}
