namespace API_Arcadia.Models
{
    public class StatsDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string AnimalsCollectionName { get; set; } = null!;
        public string HabitatsCollectionName { get; set; } = null!;
    }
}
