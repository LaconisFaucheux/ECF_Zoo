namespace API_Arcadia.Models
{
    public class AnimalImage
    {
        public int Id { get; set; }
        public string Slug { get; set; } = string.Empty;
        public int IdAnimal { get; set; }
    }
}
