namespace API_Arcadia.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Pseudo { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsValidated { get; set; }
        public byte Note { get; set; }
    }
}
