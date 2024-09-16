namespace MyApi2.Dtos
{
    public class TTviewsDto1
    {
        public long Id { get; set; }

        public byte T_batch { get; set; }

        public byte Type_id { get; set; }

        public string? Remark { get; set; }

        public string? Type_Name { get; set; }

        public ICollection<TTviewsDto2>? TT_info { get; set; }
    }
}
