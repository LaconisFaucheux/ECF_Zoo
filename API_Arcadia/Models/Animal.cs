using System.ComponentModel.DataAnnotations;

namespace API_Arcadia.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsMale { get; set; }
        public int IdSpecies { get; set; }
        public int IdHealth { get; set; }

        //nav props
        public virtual List<AnimalImage> Pics { get; set; } = new();
        public virtual Species SpeciesData { get; set; } = new();
        public virtual Health HealthData { get; set; } = new();
    }
}
