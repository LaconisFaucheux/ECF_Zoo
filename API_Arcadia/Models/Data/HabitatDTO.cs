namespace API_Arcadia.Models.Data
{
    public class HabitatDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //DTO specific prop
        public List<IFormFile?> images { get; set; } = new();
        public List<int>? deletedImages { get; set; } = new();   
    }
}
