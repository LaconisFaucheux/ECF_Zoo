namespace API_Arcadia.Models.Data
{
    public class AnimalDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsMale { get; set; }
        public int IdSpecies { get; set; }
        public int IdHealth { get; set; }

        //DTO specific props
        public List<IFormFile?> images {  get; set; } = new(); //si POST: liste des images associées. Si PUT: liste des images ajoutées
        public List<int> deletedImages { get; set; } = new(); // là dedans le front doit envoyer la liste des images supprimées par le user
    }
}
