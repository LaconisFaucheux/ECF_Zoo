namespace API_Arcadia.Models
{
    public class Species
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ScientificName {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float MaleMaxSize { get; set; }
        public float FemaleMaxSize { get; set; }
        public float MaleMaxWeight { get; set; }
        public float FemaleMaxWeight { get; set; }
        public byte IdSizeUnit { get; set; }
        public byte IdWeightUnit { get; set; }
        public byte Lifespan {  get; set; }
        public byte IdDiet { get; set; }
    }
}
