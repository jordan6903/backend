using MyApi2.Models;

namespace MyApi2.Dtos
{
    public class ExportSetOthersDtos
    {
        public int Id { get; set; }

        public int Export_batch { get; set; }

        public string? Name { get; set; }

        public bool? Use_yn { get; set; }

        public short? Sort { get; set; }

        public string? Series_data { get; set; }

        public int? snumber { get; set; }

        public int? pnumber { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }
    }
}
