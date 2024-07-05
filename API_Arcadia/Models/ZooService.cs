namespace API_Arcadia.Models
{
    public class ZooService
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Pic { get; set; } = string.Empty;
        public float? FullPrice { get; set; }
        public float? ChildPrice { get; set; }
    }
}
