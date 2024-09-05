using MyApi2.Models;

namespace MyApi2.Dtos
{
    public class ViewDto1
    {
        public string C_id { get; set; } = null!;
        public string C_Name { get; set; } = null!;
        public string? Name_origin { get; set; }
        public string? Name_short { get; set; }
        public byte C_type { get; set; }
        public string C_type_name { get; set; } = null!;
        public string? Intro { get; set; }
        public string? Remark { get; set; }
        public string? Sdate { get; set; }
        public string? Edate { get; set; }
        public ICollection<ViewDto1_2>? Products { get; set; }
    }
}
