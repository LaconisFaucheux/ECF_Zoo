namespace API_Arcadia.Models.Data
{
    public class SpeciesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ScientificName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float MaleMaxSize { get; set; }
        public float? FemaleMaxSize { get; set; }
        public float MaleMaxWeight { get; set; }
        public float? FemaleMaxWeight { get; set; }
        public int IdSizeUnit { get; set; }
        public int IdWeightUnit { get; set; }
        public byte Lifespan { get; set; }
        public int IdDiet { get; set; }

        //DTO specific prop
        public List<int> habitats { get; set; } = new();
    }
}
