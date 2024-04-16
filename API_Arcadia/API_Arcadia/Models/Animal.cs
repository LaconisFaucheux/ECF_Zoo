namespace API_Arcadia.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsMale { get; set; }
        public int IdSpecies { get; set; }
        public byte IdHealth { get; set; }
    }
}
