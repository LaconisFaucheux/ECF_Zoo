namespace API_Arcadia.Models
{
    public class VetVisit
    {
        public int Id { get; set; }
        public string Food { get; set; } = string.Empty;
        public float FoodWeight { get; set; }
        public int IdWeightUnit { get; set; }
        public DateTime VisitDate { get; set; }
        public string Observations { get; set; } = string.Empty;
        public int IdAnimal { get; set; }
        public int? IdVet { get; set; }

        //nav prop
        public virtual Animal animal { get; set; } = new();
        public virtual WeightUnit foodWeightUnit { get; set; } = new();
    }
}
