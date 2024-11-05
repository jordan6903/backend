namespace MyApi2.Dtos
{
    public class ExportViewsCount
    {
        public int Id { get; set; }

        public int Export_batch { get; set; }

        public string? C_id { get; set; }

        public int? Sort { get; set; }

        public int Count_export { get; set; }

        public int Count_exportall { get; set; }

        public int Count_all { get; set; }
    }
}
