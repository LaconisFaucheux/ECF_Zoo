namespace API_Arcadia.Models
{
    public class EmployeeFeeding
    {
        public int Id { get; set; }
        public string EmployeeEmail { get; set; } = string.Empty;
        public int IdAnimal { get; set; }
        public string Food { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public double Weight { get; set; }
        public int IdWeightUnit { get; set; }

        //NavProps
        public virtual WeightUnit weightUnit { get; set; } = new();
        public virtual Animal relatedAnimal { get; set; } = new();
    }
}
