namespace MyApi2.Dtos
{
    public class View12toDto
    {
        public string P_id { get; set; } = null!;

        public ICollection<View12toDto2>? Pic_data { get; set; }
    }
}