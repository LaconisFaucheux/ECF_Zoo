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

        //public virtual byte[]? Image { get; set; } = null;
        //ajouter une prop de navigation pour envoyer les photos en byte array ou string base64
        // //  à voir si on pweut envoyer les images par ce biais en ajoutant la prop dans Blazor.Shared
    }
}
