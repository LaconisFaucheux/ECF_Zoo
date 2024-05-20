namespace API_Arcadia.Models
{
    public class Habitat
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //nav props
        public virtual List<HabitatImage> Pics { get; set; } = new();
    }
}
