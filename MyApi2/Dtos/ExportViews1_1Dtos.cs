using MyApi2.Models;

namespace MyApi2.Dtos
{
    public class ExportViews1_1Dtos
    {
        public int Id { get; set; }

        public int Export_batch { get; set; }

        public string C_id { get; set; } = null!;

        public string? C_Name { get; set; }

        public string? C_Name_origin { get; set; }

        public short? esc_Sort { get; set; }

        public int esps_id { get; set; }

        public string esps_Name { get; set; } = null!;

        public short? esps_Sort { get; set; }

        public int esp_id { get; set; }

        public short? esp_Sort { get; set; }

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
