namespace API_Arcadia.Models.Data
{
    public class ServiceDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float? FullPrice { get; set; }
        public float? ChildPrice { get; set; }

        //DTO specific props
        public IFormFile? image { get; set; } //si POST: liste des images associées. Si PUT: liste des images ajoutées
        public string? deletedImage { get; set; } // là dedans le front doit envoyer le chemin de l'image supprimée
    }
}
